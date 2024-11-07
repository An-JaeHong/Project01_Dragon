using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingtonManager<UIManager>
{
    public Canvas mainCanvas;
    public GameObject UpgradeBackground; //script���� ����
    public GameObject GameOver;//script���� ����
    public GameObject LevelUp; //�ܼ� ��������� , ���� UpgradeBackground ȣ���ؾ���
    
    bool isPaused = false; //�ð������� ����

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
        GameOver.SetActive(false);//script�� ���� ����
        LevelUp.SetActive(false); //�ܼ� ��¸� ���� skill upgrade ���

    }
    private void Update()
    {
        if(isPaused==true)
        {
            Time.timeScale = isPaused? 0f : 1f;// isPaused==true �ϋ� 0_����..;
        }
    }
    public IEnumerator LevelUpCoroutine()
    {
        print("����");
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
