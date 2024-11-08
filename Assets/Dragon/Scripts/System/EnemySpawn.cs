using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UniRan = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyprefab;
    public GameObject playerObject;
    private Player player;
    private Enemy enemy;
    private float hpIncrease = 0f;
    private float expIncrease = 0f;

    [Tooltip("한번에 스폰될 적의 수\nx : 최소값, y : 최대값")]
    public Vector2Int minMaxSpawnCount;//(5~10)마리 생성

    public List<Vector2> SpawnPositions = new List<Vector2>(); //생성된 적의 위치 저장
    private void Start()
    {
        
        player = playerObject.GetComponent<Player>();
        

        StartCoroutine(spawnCoroutine());
    
    }
    private void Update()
    {
        
    }


    //적 생성 코루틴
    private IEnumerator spawnCoroutine()
    {
        print("2. 생성");
        while (true)
        {
            int spawnNum = Random.Range(minMaxSpawnCount.x, minMaxSpawnCount.y + 1);
            //print(spawnNum);
            //int maxNum = 40 - SpawnPositions.Count;


            //if (maxNum < spawnNum)
            //{ spawnNum = maxNum; }

            Spawn(spawnNum);


            yield return new WaitUntil(() => player.isShooting);
            yield return new WaitUntil(() => !player.isShooting);
            //print(SpawnPositions.Count);
        }

        


    }
   

    //적 생성 
    private void Spawn(int count)
    {
        for (int i = GameManager.Instance.enemies.Count - 1; i >= 0; i--)
        {
            print("작동하니");
            Enemy enemy = GameManager.Instance.enemies[i];
            if (enemy != null)
            {
                enemy.Move();
            }
            else
            {
                GameManager.Instance.enemies.RemoveAt(i);
            }
        }

        Vector2 spawnPos;
        player.round++;

        float currentMaxHp = 1000f + hpIncrease;
        float currentExpAmount = 100f + expIncrease;

        for (int i = 0; i < count; i++)
        {
            
            do
            {
                float ranPosX = Random.Range(-4, 4) + 0.5f;
                float ranPosY = Random.Range(6, 11) + 0.5f;


                spawnPos = new Vector2(ranPosX, ranPosY);
            }
            while (SpawnPositions.Contains(spawnPos));
            SpawnPositions.Add(spawnPos);
            //print(spawnPos);
            GameObject enemyObj = Instantiate(enemyprefab, spawnPos, Quaternion.identity);
            Enemy enemyComponent = enemyObj.GetComponent<Enemy>();
            //Instantiate(enemyprefab, spawnPos, Quaternion.identity);
            enemyComponent.maxHp = currentMaxHp;
            enemyComponent.hp = currentMaxHp;
            enemyComponent.expAmount = currentExpAmount;

        }
        hpIncrease += 100f;
        expIncrease += 10f;

    }
    public void RemoveSpawnPosition(Vector2 position)
    {
        SpawnPositions.Remove(position);
    }

    public void AddSpawnPosition(Vector2 position)
    {
        if (!SpawnPositions.Contains(position))
        {
            SpawnPositions.Add(position);
        }
    }

}
