using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Exp : MonoBehaviour
{
    public float exp = 100f;
    public float nextExp = 10;

    private Transform target;
    private Player player;

    private ExpPoint expPoint;

    public float delay = 2f;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        expPoint = FindObjectOfType<ExpPoint>();
        target = expPoint.transform;
        //print(player.currentExp);
        StartCoroutine(gainExp());

    }

    private void LateUpdate()
    {

    }
    public IEnumerator gainExp()
    {
        while (true)
        {
            //print("작동함");
            yield return new WaitUntil(() => !player.isShooting);
            //Vector2 speed = Vector2.zero;
            transform.position = Vector2.MoveTowards(transform.position, target.position, 0.1f); // 스무스한 이동이라고해서 사용해봤는데 그냥 사라짐
            
            if (transform.position == target.position)
            {
                Destroy(gameObject);
                
                
                player.currentExp += exp;
            }
        }
    }
}
