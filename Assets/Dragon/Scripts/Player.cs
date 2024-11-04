using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float hp = 4f;
    public float damage = 100f;
    public float LevelUpExp = 1000f;
    public float PlusNextLevelExp = 100f;
    public float level = 1f;
    public float amount = 10f;
    private float maxHp = 4f;
    public int killCount = 0;
    public Image hpBar;
    public GameObject shotPoint;
    public GameObject ProjectileFirePrefab;
    public float movespeed = 100f;
    private Rigidbody2D rb;

    internal float fistStopPosX = 0f;
    private float newPosX;//�������� ������ġ

    private Projectile projectile;

    private Vector2 initialDirection;
    public bool isShooting = false; //�߻� ���� 
    public float hpAmount { get { return hp / maxHp; } }


    private void Start()
    {
        GameManager.Instance.player= this;
        projectile = FindObjectOfType<Projectile>();
        StartCoroutine(Coroutine1());

    }

    private void Update()
    {

        hpBar.fillAmount = hpAmount;
        
            
           
        
    }


    public void DirPos()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //print(pos);
        initialDirection = (pos - (Vector2)shotPoint.transform.position).normalized;

    }
 

    //���� �ڷ�ƾ
  public IEnumerator first_shotCoroutine()
    {
        
        print("1.���� �ڷ�ƾ ����");

        isShooting = true;//�߻� ����
        float count = amount;
        for (int i = 0; i < count; i++)
        {
            GameObject projectile = Instantiate(ProjectileFirePrefab, shotPoint.transform.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().Initialize(initialDirection);

            yield return new WaitForSeconds(0.1f);
            amount--;
            
            //print(amount);
        }
        print(isShooting);
      
    
    }

    public IEnumerator Coroutine1() // Ȯ�ο�
    {
        while (true)
        {
            DirPos();
            yield return StartCoroutine(third_MoveCoroutine());
            if (Input.GetMouseButtonDown(0) && isShooting == false)
            {

                // ù ��° �ڷ�ƾ�� ���� ������ ���
                yield return StartCoroutine(first_shotCoroutine());

                // FistStopPosX�� ������ ������ ���
                //yield return new WaitUntil(() => projectile.fistStopPosX != 0);

                // �� ��° �ڷ�ƾ ����
            }
        }
    }

    //3. �̵�
    private IEnumerator third_MoveCoroutine()
    {

        //print("3�ڷ�ƾ ����");
        //print($"Fist : {fistStopPosX}");
        Vector2 newposition= new Vector2(fistStopPosX, -1f); 
        transform.position = newposition;
        
        yield return null;
    }
      
}
