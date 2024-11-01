using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   private static GameManager instance;
   public static GameManager Instance => instance;

    internal List<Enemy> enemies = new List<Enemy>();
    internal Player player;
    internal Projectile projectile;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { DestroyImmediate(this);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        
    }


}
