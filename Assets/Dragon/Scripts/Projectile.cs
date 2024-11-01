using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    public float damage = 200f;

    public float moveSpeed;
    public Vector2 movepoint;
    public Vector2 mousePosition;
    public Vector2 shotPos;
    private Rigidbody2D rb;
    private Transform shotPoint;
    private Vector2 moveDir;
    private Player player; // 미사일 연속 발사를 막기위해 임시로 만들어 놓음 
    public int amount = 0;// 미사일 연속 발사를 막기위해 임시로 만들어 놓음
    private EnemySpawn enemySpawn;
    private GameObject playerObject;

    public float FistStopPosX;

    private void Start()
    {
        GameManager.Instance.projectile=this;
        //player = FindObjectOfType<Player>();

    }

    public void Initialize(Vector2 direction)
    {
        moveDir = direction.normalized; // 방향 설정
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = moveDir * moveSpeed; // 초기 속도 설정
    }
    private void Update()
    {
        if (moveDir != Vector2.zero)
        {

            float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {


        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Enemy"))
            {
                Vector2 incoming = moveDir; // 물체의 입사 방향
                Vector2 normal = collision.contacts[0].normal; // 법선 벡터
                Vector2 reflected = Vector2.Reflect(incoming, normal); // 반사된 방향 계산
                moveDir = reflected;


                rb.velocity = moveDir * moveSpeed;

                if (collision.collider.CompareTag("Enemy"))
                {
                    Enemy enemy = collision.collider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.hp -= damage;

                        if (enemy.hp <= 0)
                        {
                            enemy.die();
                            enemySpawn = FindObjectOfType<EnemySpawn>();
                            List<Vector2> spawnPositions = enemySpawn.SpawnPositions;
                            spawnPositions.Remove(this.transform.position);
                        }
                    }

                }

            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {


        {



            if (collision.CompareTag("StopLine"))
                if (gameObject != null)
                {

                    Rigidbody2D rb = GetComponent<Rigidbody2D>();
                    rb.velocity = Vector2.zero;
                    rb.isKinematic = true; // 물리 엔진의 영향을 받지 않도록 설정
                                           //print(rb.velocity);
                                           // 미사일 삭제
                    Destroy(gameObject);
                    player = FindObjectOfType<Player>();

                    // Player의 발사 수 증가

                    player.amount++;
                    if (player.amount == 1)
                    {
                       
                        FistStopPosX= gameObject.transform.position.x;
                        print(FistStopPosX);
                    }
                    print(player.amount);
                    if (player.amount == 10f) // 발사 수가 10에 도달하면
                    {
                        player.isShooting = false; // 발사 상태를 false로 설정
                    }

                }
        }
    }
}