using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float hp=1000;
    private float maxHp=1500;
    public Image hpBar;
    public float hpAmount { get { return hp / maxHp; } }
    private EnemySpawn enemySpawn;
    private Player player;
    public float exp = 100;
    public float plusExp = 10;

    public GameObject expPrefabs;
    private void Start()
    {
        enemySpawn = FindObjectOfType<EnemySpawn>();
        GameManager.Instance.enemies.Add(this);
        enemySpawn.RemoveSpawnPosition(transform.position);
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        hpBar.fillAmount = hpAmount;
        UpdateSpawnPosition();
        
    }

    public void takeDamage()
    {
        if (transform.position.y <= 1.5f)
        {
            print(player.hp);

            player.hp--;
            die();
            print(player.hp);

        }
    }

    public void die()
    {
        // ���� ���� �� SpawnPositions���� ��ġ ����
        if (enemySpawn != null)
        {
            enemySpawn.RemoveSpawnPosition(transform.position);
        }
        GameObject expPrefab = Instantiate(expPrefabs, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
      
    }


    public void Move()
    {
        if (transform.position.y <= 1.5f)
        {
            //print(player.hp);

            player.hp--;
            die();
            //print(player.hp);

        }
        transform.position -= new Vector3(0, 1, 0);

        enemySpawn.RemoveSpawnPosition(transform.position);
        //print("������ ");
        enemySpawn.AddSpawnPosition(transform.position);
        //print("�߰���");
    }
    private void UpdateSpawnPosition()
    {
        // ���� ��ġ�� SpawnPositions���� �����ϰ�, ���ο� ��ġ�� �߰�
       
    }
}


