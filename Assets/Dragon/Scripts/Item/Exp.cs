using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    public float exp = 100f;
    public float nextExp = 10;

    private Transform target;
    private Rigidbody2D rb;
    private Player player;

    private ExpPoint expPoint;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        expPoint = FindObjectOfType<ExpPoint>();
        target = expPoint.transform; 
    }
    
    public void gainExp()
    {
        print("¿€µø«‘");
        player.currentExp += exp;
        Destroy(gameObject);
    }
}
