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
        if (!player.stayLevelup && !player.isShooting) // 필요한 경우에만 실행
        {
            gainExp();
        }


        //gainExp();
    }
    public void gainExp()
    {
       
            //print("작동함");
           
                //Vector2 speed = Vector2.zero;
                if (!player.stayLevelup&& !player.isShooting)
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, 0.1f); // 스무스한 이동이라고해서 사용해봤는데 그냥 사라짐
                }
                else if (player.stayLevelup)
                {
                    print("작동중");
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
