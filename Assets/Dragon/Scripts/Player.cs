using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public float hp = 4f;
    public float damage = 100f;
    public float LevelUpExp = 1000f;
    public float currentExp = 0f;
    public float PlusNextLevelExp = 100f;
    
    public int round=0;
    
    public float level = 1f;
    public int amount = 10;
    public float initialAmount;

    private float maxHp = 4f;
    public int killCount = 0;
    public Image hpBar;
    public Image expBar;
    public GameObject shotPoint;
    public GameObject ProjectileFirePrefab;
    public GameObject shootDirPrefab;
    public float movespeed = 100f;
    private Rigidbody2D rb;
    private GameObject shootDir;
    private float angle;
    Vector2 pos;
    internal float fistStopPosX = 0f;
    private float newPosX;
    private Exp exp;

    private Projectile projectile;

    private Vector2 initialDirection;
    public bool isShooting = false;
    public float hpAmount { get { return hp / maxHp; } }


    public float ExpAmount { get { return currentExp / LevelUpExp; } }

    private float moveDir;



    private void Start()
    {
        initialAmount = amount;
        exp = FindObjectOfType<Exp>();
        GameManager.Instance.player = this;
        projectile = FindObjectOfType<Projectile>();
        StartCoroutine(Coroutine1());

    }

    private void Update()
    {
        upgradeAmount();
        hpBar.fillAmount = hpAmount;
        expBar.fillAmount = ExpAmount;
        ShootDir();
        LevelUp();
        
        UIManager.Instance.killcountText.text = killCount.ToString();
        UIManager.Instance.roundText.text = round.ToString();
      UIManager.Instance.shootAmount.text = amount.ToString();

        shotPoint.transform.position = shotPoint.transform.position;


    }


    public void DirPos()
    {
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        initialDirection = (pos - (Vector2)shotPoint.transform.position).normalized;
        angle = Mathf.Atan2(initialDirection.x, initialDirection.y) * -Mathf.Rad2Deg;

    }



    public IEnumerator first_shotCoroutine()
    {


        isShooting = true;

        for (int i = 0; i < initialAmount; i++)
        {
            GameObject projectile = Instantiate(ProjectileFirePrefab, shotPoint.transform.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().Initialize(initialDirection);

            yield return new WaitForSeconds(0.1f);
            amount--;

            //print(amount);
        }
        //print(isShooting);


    }
    private void ShootDir()
    {
        if (Input.GetMouseButtonDown(0) && isShooting == false)
        {
            DirPos();
            shootDir = Instantiate(shootDirPrefab, pos, Quaternion.identity);

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



    public IEnumerator Coroutine1()
    {
        while (true)
        {

            DirPos();

            if (Input.GetMouseButtonUp(0) && isShooting == false)
            {


                yield return StartCoroutine(first_shotCoroutine());


                //yield return new WaitUntil(() => projectile.fistStopPosX != 0);


                yield return new WaitUntil(() => isShooting==false);
                for (int i = GameManager.Instance.enemies.Count - 1; i >= 0; i--)
                {
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
                //yield return new WaitUntil(() => isShooting == false);
            }

            
            yield return StartCoroutine(third_MoveCoroutine());


            //print("삭제전");
        
            //print("삭제후");

        }
    }

    //3. 
    private IEnumerator third_MoveCoroutine()
    {


        //print($"Fist : {fistStopPosX}");
        Vector2 newposition = new Vector2(fistStopPosX, -1f);
        transform.position = newposition;

        yield return null;
    }

    //public void TakeExp()
    //{
    //    exp.gainExp();
    //}

    public void LevelUp()
    {
        if (LevelUpExp <= currentExp)
        {
            print("LevelUp");
            level++;
            print($"현재 레벨 : {level}");
            currentExp -= LevelUpExp;
            LevelUpExp += PlusNextLevelExp;

            

            //print(projectile.damage);
            //projectile.damage += 100f;

            amount += 10;
            //print(amount);
            initialAmount += 10;
        StartCoroutine(UIManager.Instance.LevelUpCoroutine()); 
           }

    }
    private void upgradeAmount()
    { 
    if(amount>=100)
        {
            print(amount);
            print(initialAmount);
            print(projectile.damage);
            amount /= 10;
            projectile.damage *= 10f;
            initialAmount = amount;
            print(amount);
            print(initialAmount);
            print(projectile.damage);

        }
    }
}
