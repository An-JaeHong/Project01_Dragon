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
    public GameObject playerObject;
    private void Start()
    {
        enemySpawn = FindObjectOfType<EnemySpawn>();
        GameManager.Instance.enemies.Add(this);
        enemySpawn.RemoveSpawnPosition(transform.position);
        //player = playerObject.GetComponent<Player>();
    }

    private void Update()
    {
        hpBar.fillAmount = hpAmount;
        UpdateSpawnPosition();
        //Move() �ϸ��� �̵�����
    }

    public void die()
    {
        // ���� ���� �� SpawnPositions���� ��ġ ����
        if (enemySpawn != null)
        {
            enemySpawn.RemoveSpawnPosition(transform.position);
        }
        Destroy(gameObject);
        
      
    }


    private void Move()
    { 
    
    }
    private void UpdateSpawnPosition()
    {
        // ���� ��ġ�� SpawnPositions���� �����ϰ�, ���ο� ��ġ�� �߰�
        enemySpawn.RemoveSpawnPosition(transform.position);
        //enemySpawn.AddSpawnPosition(transform.position); // ���ο� ��ġ �߰�
    }
}

