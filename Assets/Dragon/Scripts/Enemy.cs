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
        //Move() 턴마다 이동예정
    }

    public void die()
    {
        // 적이 죽을 때 SpawnPositions에서 위치 제거
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
        // 현재 위치를 SpawnPositions에서 제거하고, 새로운 위치를 추가
        enemySpawn.RemoveSpawnPosition(transform.position);
        //enemySpawn.AddSpawnPosition(transform.position); // 새로운 위치 추가
    }
}


