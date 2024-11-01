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


    [Tooltip("�ѹ��� ������ ���� ��\nx : �ּҰ�, y : �ִ밪")]
    public Vector2Int minMaxSpawnCount;//(5~10)���� ����

    public List<Vector2> SpawnPositions = new List<Vector2>(); //������ ���� ��ġ ����
    private void Start()
    {
        player = playerObject.GetComponent<Player>();
        StartCoroutine(spawnCoroutine());
    }



    //�� ���� �ڷ�ƾ
    private IEnumerator spawnCoroutine()
    {
        print("2. ����");
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

    //�� ���� 
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
