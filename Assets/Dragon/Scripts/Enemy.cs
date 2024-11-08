using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float hp=1000;
    public float maxHp=1000;
    public Image hpBar;
    public float hpAmount { get { return hp / maxHp; } }
    private EnemySpawn enemySpawn;
    private Player player;
    private Exp exp;
    public float expAmount=100f;
    public GameObject expPrefabs;

    public List<Exp> expPrefabsList = new List<Exp>();
    private void Start()
    {
        exp = FindObjectOfType<Exp>();
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
       
        if (enemySpawn != null)
        {
            enemySpawn.RemoveSpawnPosition(transform.position);
        }
        Destroy(gameObject);

        GameObject expPrefab = Instantiate(expPrefabs, transform.position, Quaternion.identity);
       Exp expComponent = expPrefab.GetComponent<Exp>();
        if (expComponent != null)
        {
            expComponent.exp = expAmount;  // 증가된 경험치 설정
            expPrefabsList.Add(expComponent);
        }
        //print(expPrefabsList.Count);
       
        
      
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
       
        enemySpawn.AddSpawnPosition(transform.position);
   
    }
    private void UpdateSpawnPosition()
    {
     
       
    }
}


