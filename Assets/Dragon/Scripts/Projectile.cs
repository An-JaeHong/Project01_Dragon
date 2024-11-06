using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    public float damage = 100f;

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
    private Exp exp;

    
 
    private void Start()
    {
        exp = FindObjectOfType<Exp>();    
        GameManager.Instance.projectile=this;
        player = FindObjectOfType<Player>();
        //print(fistStopPosX);
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

                rb.velocity = moveDir* moveSpeed;
               
                if (collision.collider.CompareTag("Enemy"))
                {
                    Enemy enemy = collision.collider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.hp -= damage;

                        if (enemy.hp <= 0f)
                        {
                            //print(player.currentExp);
                            enemy.die();
                            enemySpawn = FindObjectOfType<EnemySpawn>();
                            List<Vector2> spawnPositions = enemySpawn.SpawnPositions;
                            spawnPositions.Remove(this.transform.position);
                            //print(player.currentExp);
                            player.currentExp += 100f; //다른곳에서 먹어야해
                            //print(player.currentExp);
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
                    Destroy(gameObject); //지금은 삭제 나중 지워짐 옮겨야해
                    player = FindObjectOfType<Player>();

                    // Player의 발사 수 증가

                    player.amount++;
                    if (player.amount == 1)
                    {

                        player.fistStopPosX= gameObject.transform.position.x;
                        //print(player.fistStopPosX);
                    }
                   
                    
                    if (player.amount == player.initialAmount) // 발사 수가 10에 도달하면
                    {
                        //print(player.isShooting);
                        player.isShooting = false; // 발사 상태를 false로 설정
                        //print(player.isShooting);
                    }

                }
        }
    }
}