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

    [Tooltip("�ѹ��� ������ ���� ��\nx : �ּҰ�, y : �ִ밪")]
    public Vector2Int minMaxSpawnCount;//(5~10)���� ����

    public List<Vector2> SpawnPositions = new List<Vector2>(); //������ ���� ��ġ ����
    private void Start()
    {
        
        player = playerObject.GetComponent<Player>();
        

        StartCoroutine(spawnCoroutine());
    
    }
    private void Update()
    {
        
    }


    //�� ���� �ڷ�ƾ
    private IEnumerator spawnCoroutine()
    {
        print("2. ����");
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
   
    //�� ���� 
    private void Spawn(int count)
    {
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
