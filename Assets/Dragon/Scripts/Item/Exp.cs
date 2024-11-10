using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Exp : MonoBehaviour
{
    public float exp = 100f;
    public float nextExp = 10;

    private ExpPoint expPoint;
    private Transform target;
    private Player player;

    
    public float delay = 2f;
    private void Start()
    {

        player = FindObjectOfType<Player>();
        expPoint = FindObjectOfType<ExpPoint>();
        target = expPoint.transform;
        //print(player.currentExp);
        //StartCoroutine(gainExp());

        GameManager.Instance.exps.Add(this);

    }

    private void LateUpdate()
    {
        if (!player.stayLevelup && !player.isShooting) // �ʿ��� ��쿡�� ����
        {
            gainExp();
        }


        //gainExp();
    }
    public void gainExp()
    {
       
            //print("�۵���");
           
                //Vector2 speed = Vector2.zero;
                if (!player.stayLevelup&& !player.isShooting)
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, 0.1f); // �������� �̵��̶���ؼ� ����غôµ� �׳� �����
                }
                else if (player.stayLevelup)
                {
                    print("�۵���");
                    transform.position = Vector2.MoveTowards(transform.position, transform.position, 0f);
                }

                if (transform.position == target.position)
                {
                    GameManager.Instance.exps.Remove(this);
                    Destroy(gameObject);
                   

                    player.currentExp += exp;
                }
            
        
    }
}
