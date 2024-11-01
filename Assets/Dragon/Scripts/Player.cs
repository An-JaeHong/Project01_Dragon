using System;
using System.Collections;
using System.Collections.Generic;
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

    public float newPosX;//�������� ������ġ

    private Vector2 initialDirection;
    public bool isShooting = false; //�߻� ���� 
    public float hpAmount { get { return hp / maxHp; } }


    private void Start()
    {
        GameManager.Instance.player= this;
    }

    private void Update()
    {

        hpBar.fillAmount = hpAmount;
        if (Input.GetMouseButtonDown(0) && isShooting == false)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //print(pos);
            initialDirection = (pos - (Vector2)shotPoint.transform.position).normalized;
            StartCoroutine(first_shotCoroutine());

            StartCoroutine(third_MoveCoroutine());

        }
    }

    //���� �ڷ�ƾ
    private IEnumerator first_shotCoroutine()
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
            print(amount);
        }
        

    }
    //3. �̵�
    private IEnumerator third_MoveCoroutine()
    {
        newPosX = GameManager.Instance.projectile.FistStopPosX;
        Vector2 newposition= new Vector2(newPosX, transform.position.y);
        transform.position = newposition;
        yield return null;
    }
}
