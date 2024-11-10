using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;


//using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public bool canShoot = true;
    public float hp = 4f;
    public float damage = 100f;
    public float LevelUpExp = 1000f;
    public float currentExp = 0f;
    public float PlusNextLevelExp = 100f;
    
    public int round=0;
    
    public float level = 1f;
    public int amount = 10;
    public int initialAmount;

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

    private Coroutine shootingCoroutine;

    private void Start()
    {
        GameManager.Instance.player = this;
        UIManager.Instance.player = this;
        initialAmount = amount;
        exp = FindObjectOfType<Exp>();
        projectile = FindObjectOfType<Projectile>();
        stopcoroutine = true;
        projectileBack = true;
        onButton = false;

        StartCoroutine(Coroutine1());

    }

    private void Update()
    {
       
        upgradeAmount();
        hpBar.fillAmount = hpAmount;
        expBar.fillAmount = ExpAmount;
        ShootDir();
        LevelUp();
        PlayerDie();

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

    private void PlayerDie()
    {
        if (hp <= 0)
        {
            print("플레이어사망");
            UIManager.Instance.GameOver.SetActive(true);
            isShooting = false;


        }
    }
    public IEnumerator first_shotCoroutine()
    {

        
        if (GameManager.Instance.expsnum)
        {
            UIManager.Instance.button.gameObject.SetActive(true);
            isShooting = true;
            for (int i = 0; i < initialAmount; i++)
            {
                
                if (!isShooting) yield break;

                
                GameObject projectileObj = Instantiate(ProjectileFirePrefab, shotPoint.transform.position, Quaternion.identity);
                Projectile projectileComponent = projectileObj.GetComponent<Projectile>();
                projectileComponent.damage = damage; // 데미지 값을 설정
                projectileComponent.Initialize(initialDirection);

                yield return new WaitForSeconds(0.1f);
                amount--;

            }
        }
    }
    
    public bool checkMousePos = false;
    private void ShootDir()
    {
        if (!canShoot||!GameManager.Instance.expsnum) return;
        
        
        // 마우스 월드좌표로
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 마우스범위
        bool isInBounds = mouseWorldPos.x >= -4f && mouseWorldPos.x <= 4f &&
                         mouseWorldPos.y >= 1f && mouseWorldPos.y <= 11f;

        if (Input.GetMouseButtonDown(0) && isShooting == false && isInBounds)
        {
            checkMousePos = true;
            DirPos();
            shootDir = Instantiate(shootDirPrefab, pos, Quaternion.identity);

        }
        else if (Input.GetMouseButton(0) && shootDir != null&& isInBounds)
        {
            DirPos();
            Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            shootDir.transform.position = newPos;
            shootDir.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            Transform tail = shootDir.transform.Find("tail");
            if (tail != null)
            {
                float a = (mouseWorldPos.x + shotPoint.transform.position.x) / 2;
                float b = (mouseWorldPos.y + shotPoint.transform.position.y) / 2;
                tail.transform.position = new Vector2(a, b);
                tail.transform.localScale = new Vector2(1, b);
            }



        }

        if (isShooting == true || !isInBounds)
        { Destroy(shootDir);
            if (!isInBounds) { checkMousePos = false; }        
        }

        if (isShooting == true || !isInBounds)
        {
            if (shootDir != null)
            {
                Destroy(shootDir);
            }
            if (!isInBounds)
            {
                checkMousePos = false;
            }
        }
    }



    public IEnumerator Coroutine1()
    {   
        while (true)
        {
            if(!stopcoroutine) yield break;
            yield return StartCoroutine(third_MoveCoroutine());
            // 마우스 월드좌표로
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 마우스범위
            bool isInBounds = mouseWorldPos.x >= -4f && mouseWorldPos.x <= 4f &&
                             mouseWorldPos.y >= 1f && mouseWorldPos.y <= 11f;
            DirPos();
            //print("이거 실행안되는거같은데");
           
            if (Input.GetMouseButtonUp(0) && isShooting == false && canShoot && isInBounds && checkMousePos)
            {
                //print("여기를 못들어와");
                shootingCoroutine = StartCoroutine(first_shotCoroutine());
                yield return shootingCoroutine;
                //yield return StartCoroutine(first_shotCoroutine());


                //yield return new WaitUntil(() => projectile.fistStopPosX != 0);


                yield return new WaitUntil(() => isShooting==false);
               
                   
                
            }
          
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

    public bool stayLevelup = false;
    public int countRan = -1;
    public int powerRan = -1;
    public void LevelUp()
    { 
        
        if (LevelUpExp <= currentExp)
        {

            stayLevelup = true;
            canShoot = false;
            print("LevelUp");
            level++;
          
            currentExp -= LevelUpExp;
            LevelUpExp += PlusNextLevelExp;
           
            print($"현재 레벨 : {level}");
            
            
                powerRan = Random.Range(1, 4) * 10;
                countRan = Random.Range(1, 6) * 10;
                UIManager.Instance.upgradePowerNum.text = powerRan.ToString() + "%";
                UIManager.Instance.upgradeCountNum.text = countRan.ToString();

            //StopCoroutine(exp.gainExp());
            StartCoroutine(UIManager.Instance.LevelUpCoroutine());
            
            
    
        }
        
    
    }
  
        private void upgradeAmount()
        {

            if (amount >= 100)
            {
                amount /= 10;
                damage *= 10f; // projectile.damage 대신 Player의 damage 값을 수정
                initialAmount = amount;
            }
        }
    public bool onButton=false ; 
   public bool projectileBack = true;
    public bool stopcoroutine = true;
    public void AbsorbAllProjectiles()
    {
        onButton = true;
        projectileBack = false;
        print("2번");
        //if (shootingCoroutine != null)
        //{
        //    StopCoroutine(shootingCoroutine);
        //    shootingCoroutine = null;
        //}

        
        stopcoroutine = false;

        Projectile[] projectiles = FindObjectsOfType<Projectile>();
        foreach (Projectile proj in projectiles)
        {
            print("3번");
            proj.StartAbsorption(transform.position);

        }

        print("4번");
        // 모든 상태 완전 초기화

        isShooting = false;
        canShoot = true;
        amount = (int)initialAmount;
        shootingCoroutine = null;
        stopcoroutine = true;

    }
   
   

}
