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
    public GameObject shootDirPrefab;
    public float movespeed = 100f;
    private Rigidbody2D rb;
    private GameObject shootDir;
    private float angle;
    Vector2 pos;
    internal float fistStopPosX = 0f;
    private float newPosX;//다음라운드 공격위치


    private Projectile projectile;

    private Vector2 initialDirection;
    public bool isShooting = false; //발사 여부 
    public float hpAmount { get { return hp / maxHp; } }

    private float moveDir;


    private void Start()
    {

        GameManager.Instance.player = this;
        projectile = FindObjectOfType<Projectile>();
        StartCoroutine(Coroutine1());

    }

    private void Update()
    {

        hpBar.fillAmount = hpAmount;
        ShootDir();

        shotPoint.transform.position = shotPoint.transform.position;


    }


    public void DirPos()
    {
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        initialDirection = (pos - (Vector2)shotPoint.transform.position).normalized;
        angle = Mathf.Atan2(initialDirection.x, initialDirection.y) * -Mathf.Rad2Deg;

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
    private void ShootDir()
    {
        if (Input.GetMouseButtonDown(0) && isShooting == false)
        {
            DirPos();
            shootDir = Instantiate(shootDirPrefab, pos, Quaternion.identity);
            print("누름");
        }
        else if (Input.GetMouseButton(0) && shootDir != null && isShooting == false)
        {
            DirPos();
            Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            shootDir.transform.position = newPos;
            shootDir.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            Transform tail = shootDir.transform.Find("tail");
            if (tail != null)
            {

                Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
                float a = (mouseWorldPos.x + shotPoint.transform.position.x) / 2;
                float b = (mouseWorldPos.y + shotPoint.transform.position.y) / 2;
                tail.transform.position = new Vector2(a, b);
                //tail.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                tail.transform.localScale = new Vector2(1, b);
            }


        }
        if (isShooting == true)
        { Destroy(shootDir); }
    }



    public IEnumerator Coroutine1() // 확인용
    {
        while (true)
        {

            DirPos();
            yield return StartCoroutine(third_MoveCoroutine());
            if (Input.GetMouseButtonUp(0) && isShooting == false)
            {

                // 첫 번째 코루틴이 끝날 때까지 대기
                yield return StartCoroutine(first_shotCoroutine());

                // FistStopPosX가 설정될 때까지 대기
                //yield return new WaitUntil(() => projectile.fistStopPosX != 0);

                // 세 번째 코루틴 실행
                yield return new WaitUntil(() => isShooting);
                for (int i = GameManager.Instance.enemies.Count - 1; i >= 0; i--) // 역순으로 반복
                {
                    Enemy enemy = GameManager.Instance.enemies[i];
                    if (enemy != null) // null 체크 추가
                    {
                        enemy.Move(); // Enemy의 Move 메서드 호출
                    }
                    else
                    {
                        GameManager.Instance.enemies.RemoveAt(i); // null인 경우 리스트에서 제거
                    }
                }
            }
        }
    }

    //3. 이동
    private IEnumerator third_MoveCoroutine()
    {

        //print("3코루틴 시작");
        //print($"Fist : {fistStopPosX}");
        Vector2 newposition = new Vector2(fistStopPosX, -1f);
        transform.position = newposition;

        yield return null;
    }

}
