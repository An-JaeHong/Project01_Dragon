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
    private Player player; // �̻��� ���� �߻縦 �������� �ӽ÷� ����� ���� 
    public int amount = 0;// �̻��� ���� �߻縦 �������� �ӽ÷� ����� ����
    private EnemySpawn enemySpawn;
    private GameObject playerObject;
    private Exp exp;

    private bool isBeingAbsorbed = false;
    private Vector2 targetPosition;
    public float absorbSpeed = 10f; // ����Ǵ� �ӵ�


    public void StartAbsorption(Vector2 playerPosition)
    {
        isBeingAbsorbed = true;
        targetPosition = playerPosition;

        //�������۸���
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }


    private void Start()
    {
        exp = FindObjectOfType<Exp>();    
        GameManager.Instance.projectile=this;
        player = FindObjectOfType<Player>();
        //print(fistStopPosX);
    }

    public void Initialize(Vector2 direction)
    {
        moveDir = direction.normalized; // ���� ����
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = moveDir * moveSpeed; // �ʱ� �ӵ� ����
    }
    private void Update()
    {//m
        if (isBeingAbsorbed)
        {
            // �÷��̾� �������� �̵�
            transform.position = Vector2.MoveTowards(
                transform.position,
                targetPosition,
                absorbSpeed * Time.deltaTime
            );

            // �÷��̾ �����ϸ� ����
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                Destroy(gameObject);
            }
            return; // ��� ���� ���� ���� Update ���� �������� ����
        }

        // 1
        
        if (moveDir != Vector2.zero)
        {

            float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        }
       
       //m 
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
                Vector2 incoming = moveDir; // ��ü�� �Ի� ����
                Vector2 normal = collision.contacts[0].normal; // ���� ����
                Vector2 reflected = Vector2.Reflect(incoming, normal); // �ݻ�� ���� ���
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
                            //player.currentExp += 100f; //�ٸ������� �Ծ����
                            //print(player.currentExp);
                            player.killCount++;
                            
                        }
                    }

                }

            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {


        {

            if (isBeingAbsorbed) return;

            if (collision.CompareTag("StopLine"))
                if (gameObject != null)
                {

                    Rigidbody2D rb = GetComponent<Rigidbody2D>();
                    rb.velocity = Vector2.zero;
                    rb.isKinematic = true; // ���� ������ ������ ���� �ʵ��� ����
                                           //print(rb.velocity);
                                           // �̻��� ����
                    Destroy(gameObject); //������ ���� ���� ������ �Űܾ���
                    player = FindObjectOfType<Player>();

                    // Player�� �߻� �� ����

                    player.amount++;
                    if (player.amount == 1)
                    {

                        player.fistStopPosX= gameObject.transform.position.x;
                        //print(player.fistStopPosX);
                    }
                   
                    
                    if (player.amount == player.initialAmount) // �߻� �� �����ϸ�
                    {
                        //print(player.isShooting);
                        player.isShooting = false; // �߻� ���¸� false�� ����
                        //print(player.isShooting);
                    }

                }
        }
    }
    

}