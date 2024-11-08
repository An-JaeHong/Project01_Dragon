using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class ExpPoint : MonoBehaviour
{
    public Player player;

    private void Start()
    {
       
    }
    // Update is called once per frame

    private void Update()
    {
        Vector2 newPos = new Vector2(-5f + player.ExpAmount * 10, 11.2f);
        transform.position = newPos;

    }

}

