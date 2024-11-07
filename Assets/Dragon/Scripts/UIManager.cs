using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingtonManager<UIManager>
{
    public Canvas mainCanvas;
    public GameObject UpgradeBackground; //script만들 예정
    public GameObject GameOver;//script만들 예정
    public GameObject LevelUp; //단순 출력을위해 , 이후 UpgradeBackground 호출해야해
    
    bool isPaused = false; //시간정지를 위해

    public Text killcountText;
    public Text roundText;
    public Text shootAmount;

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        UpgradeBackground.SetActive(false);
        GameOver.SetActive(false);//script로 만들 예정
        LevelUp.SetActive(false); //단순 출력만 이후 skill upgrade 출력

    }
    private void Update()
    {
        if(isPaused==true)
        {
            Time.timeScale = isPaused? 0f : 1f;// isPaused==true 일떈 0_멈춤..;
        }
    }
    public IEnumerator LevelUpCoroutine()
    {
        print("정지");
        //Time.timeScale = 0f;
        print("1");
        LevelUp.SetActive(true);
        print("2");
        yield return new WaitForSeconds(2f);
        print("3");
        LevelUp.SetActive(false) ;
        print("4");
        Time.timeScale = 1f;
        print("5");
    }
}
