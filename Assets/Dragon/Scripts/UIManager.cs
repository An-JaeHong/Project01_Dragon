using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : SingtonManager<UIManager>
{
    public Canvas mainCanvas;
    public GameObject UpgradeBackground; //script���� ����
    public GameObject GameOver;
    public GameObject LevelUp; //�ܼ� ��������� , ���� UpgradeBackground ȣ���ؾ���
    public GameObject upCard1;
    public GameObject upCard2;
    public GameObject upCard3;


    public Button retryYes;
    public Button retryNo;


    public GameObject reset;
    public Button Reset;
    public GameObject select;
    public Button Select;

    public Button card1;
    public Button card2;
    public Button card3;
    public Button button;


    bool isPaused = false; //�ð������� ����

    public Text killcountText;
    public Text roundText;
    public Text shootAmount;
    private float powerNum;
    private float countNum;

    public Text upgradePowerNum;
    public Text upgradeCountNum;

    private Exp exp;

    private float playerHp = 4f;
    private float playerDamage = 100f;
    private float playerLevelUpExp = 1000f;
    private float playercurrentExp = 0f;
    private int playerRound = 0;
    private float playerLevel = 1f;
    private int playerAmount = 10;
    private int playerKillCount = 0;
    private float playerFistStopPosX = 0f;



    private Enemy enemy;
    public Player player;
    private EnemySpawn enemySpawn;


    private List<Enemy> allEnemies;  // ������ ������ ����Ʈ

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        exp = FindObjectOfType<Exp>();
        

        button.gameObject.SetActive(false);

        UpgradeBackground.SetActive(false);
        GameOver.SetActive(false);
        LevelUp.SetActive(false);
        select.SetActive(false);


        //if (button != null)
        //{
        //    button.onClick.AddListener(OnAbsorbButtonClick);
        //}

        isButton();

    }

    private void isButton()
    {
        
            button.GetComponent<Button>().onClick.AddListener(() => OnAbsorbButtonClick());
            upCard1.GetComponent<Button>().onClick.AddListener(() => UpgradeAttackPower());
            upCard2.GetComponent<Button>().onClick.AddListener(() => UpgradeProjectileCount());
            upCard3.GetComponent<Button>().onClick.AddListener(() => UpgradeHp());
            Reset.GetComponent<Button>().onClick.AddListener(() => ResetNum());
            retryYes.GetComponent<Button>().onClick.AddListener(() => restartGame());
            retryNo.GetComponent<Button>().onClick.AddListener(() => GameEnd());
       

    }
    public void EnableShooting()
    {
        Time.timeScale = 1f;  // ���� �簳
        GameManager.Instance.player.canShoot = true;
        UpgradeBackground.SetActive(false);
    }
    public IEnumerator LevelUpCoroutine()
    {
       

        LevelUp.SetActive(true);

        yield return new WaitForSeconds(2f);
        LevelUp.SetActive(false);
        select.SetActive(false);
        UpgradeBackground.SetActive(true);
        reset.SetActive(true);

        //StartCoroutine(exp.gainExp());

    }
    public void OnAbsorbButtonClick()
    {
        print("1");
        player.AbsorbAllProjectiles();
        button.gameObject.SetActive(false);

    }
    private void UpgradeAttackPower()
    {
        reset.SetActive(false);
        select.SetActive(true);
        upCard2.SetActive(false);
        upCard3.SetActive(false);
        Select.GetComponent<Button>().onClick.AddListener(() => ClickSelect());
        
        player.damage *= (100f + player.powerRan) / 100f; // ���ݷ� ����
        print("���ݷ��� ���� : " + player.damage);
        


    }

    // ���� ���� ���� �޼���
    private void UpgradeProjectileCount()
    {
        upCard1.SetActive(false);
        upCard3.SetActive(false);
        reset.SetActive(false);
        select.SetActive(true);
        Select.GetComponent<Button>().onClick.AddListener(() => ClickSelect());
       
        player.initialAmount += player.countRan; // ���� ���� 1 ����
        player.amount += player.countRan;
        print("���� ���� ���� : " + player.initialAmount);

    }

    private void UpgradeHp()
    {
        reset.SetActive(false);
        select.SetActive(true);
        upCard1.SetActive(false);
        upCard2.SetActive(false);


        
        if (player.hp < 4)
        {
            player.hp++;

        }
    }
    private void ResetNum()
    {
        print("���¹�ư ����");
        
        player.countRan = Random.Range(1, 6) * 10;
        player.powerRan = Random.Range(1, 4) * 10;
        upgradePowerNum.text = player.powerRan.ToString() + "%";
        upgradeCountNum.text = player.countRan.ToString();
    }
    private void ClickSelect()
    {

        
        select.SetActive(false);
        upCard1.SetActive(true);
        upCard2.SetActive(true);
        upCard3.SetActive(true);
        reset.SetActive(true);
        UpgradeBackground.SetActive(false);

        player.canShoot = true;
        player.stayLevelup = false;


    }
    private void restartGame()
    {

        // ���� �ð� �ӵ� ���� (�Ͻ� ���� ����)
        Time.timeScale = 1f;

        // ���� �񵿱������� �ε�
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Dragon");

        // �� �ε尡 �Ϸ�Ǹ� ����Ǵ� �ݹ�
        asyncOperation.completed += (operation) =>
        {
            // ���� ������ �ε�� �� �ʱ�ȭ�� �۾�
            InitializeGameState();

        };


    }


    private void InitializeGameState()
    {
        
        player.hp = playerHp;
        player.damage = playerDamage;
        player.LevelUpExp = playerLevelUpExp;
        player.currentExp = playercurrentExp;
        player.round = playerRound;
        player.level = playerLevel;
        player.amount = playerAmount;
        player.killCount = playerKillCount;
        player.fistStopPosX = playerFistStopPosX;

    }
    public void GameEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif

    }
}
