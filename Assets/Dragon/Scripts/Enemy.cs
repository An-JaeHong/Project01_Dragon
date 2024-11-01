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
        GameManager.Instance.enemies.Add(this);
        //player = playerObject.GetComponent<Player>();
    }

    private void Update()
    {
        hpBar.fillAmount = hpAmount;
    }

    public void die()
    {
        // ���� ���� �� SpawnPositions���� ��ġ ����
        if (enemySpawn != null)
        {
            //enemySpawn.RemoveSpawnPosition(transform.position);
        }
        Destroy(gameObject);
      
    }

    private void Move()
    { 
    
    }
}


