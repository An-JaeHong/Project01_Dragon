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
    private float newPosX;//다음라운드 공격위치

    private Projectile projectile;

    private Vector2 initialDirection;
    public bool isShooting = false; //발사 여부 
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
 

    //공격 코루틴
  public IEnumerator first_shotCoroutine()
    {
        
        print("1.공격 코루틴 시작");

        isShooting = true;//발사 시작
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

    public IEnumerator Coroutine1() // 확인용
    {
        while (true)
        {
            DirPos();
            yield return StartCoroutine(third_MoveCoroutine());
            if (Input.GetMouseButtonDown(0) && isShooting == false)
            {

                // 첫 번째 코루틴이 끝날 때까지 대기
                yield return StartCoroutine(first_shotCoroutine());

                // FistStopPosX가 설정될 때까지 대기
                //yield return new WaitUntil(() => projectile.fistStopPosX != 0);

                // 세 번째 코루틴 실행
            }
        }
    }

    //3. 이동
    private IEnumerator third_MoveCoroutine()
    {

        //print("3코루틴 시작");
        //print($"Fist : {fistStopPosX}");
        Vector2 newposition= new Vector2(fistStopPosX, -1f); 
        transform.position = newposition;
        
        yield return null;
    }
      
}
