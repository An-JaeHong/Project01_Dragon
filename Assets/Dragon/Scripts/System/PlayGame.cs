//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.SearchService;
//using UnityEngine;

//public class PlayGame : MonoBehaviour
//{
//    private Player player;
//    private Projectile projectile;
//    // Start is called before the first frame update
//    void Start()
//    {
//        player = FindObjectOfType<Player>();
//        projectile = FindObjectOfType<Projectile>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetMouseButtonDown(0) && player.isShooting == false)
//        {
//            player.DirPos();
//            StartCoroutine(player.first_shotCoroutine());
//            StartCoroutine()
//        }
//    }

//}
