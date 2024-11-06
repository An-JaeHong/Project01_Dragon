using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPoint : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();

    }
    private void Update()
    {
        Vector2 newPos = new Vector2(-5f+player.ExpAmount*10, 11.2f);
        transform.position = newPos; 
    }
}
