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


    [Tooltip("茄锅俊 胶迄瞪 利狼 荐\nx : 弥家蔼, y : 弥措蔼")]
    public Vector2Int minMaxSpawnCount;//(5~10)付府 积己

    public List<Vector2> SpawnPositions = new List<Vector2>(); //积己等 利狼 困摹 历厘
    private void Start()
    {
        player = playerObject.GetComponent<Player>();
        StartCoroutine(spawnCoroutine());
    }



    //利 积己 内风凭
    private IEnumerator spawnCoroutine()
    {
        print("2. 积己");
        while (true)
        {
            int spawnNum = Random.Range(minMaxSpawnCount.x, minMaxSpawnCount.y + 1);
            int maxNum = 30 - SpawnPositions.Count;
            print (maxNum);
           
            if (maxNum < spawnNum)
            { spawnNum = maxNum; }


            Spawn(spawnNum);


            yield return new WaitUntil(() => player.isShooting);
            yield return new WaitUntil(() => !player.isShooting);
        }

    }

    //利 积己 
    private void Spawn(int count)
    {
        Vector2 spawnPos;

        for (int i = 0; i < count; i++)
        {

            do
            {
                float ranPosX = Random.Range(-4, 3) + 0.5f;
                float ranPosY = Random.Range(6, 11) + 0.5f;


                spawnPos = new Vector2(ranPosX, ranPosY);
            }
            while (SpawnPositions.Contains(spawnPos));
            SpawnPositions.Add(spawnPos);
            //print(spawnPos);

            Instantiate(enemyprefab, spawnPos, Quaternion.identity);
        }
    }
    public void RemoveSpawnPosition(Vector2 position)
    {
        SpawnPositions.Remove(position);
    }

}
