using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   private static GameManager instance;
   public static GameManager Instance => instance;

    public List<Exp> exps = new List<Exp>();
    internal List<Enemy> enemies = new List<Enemy>();
    internal Player player;
    internal Projectile projectile;
    internal Enemy enemy;
    internal bool expsnum = true;
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
        checkExpsList();
    }

    public void checkExpsList()
    {
        if (exps.Count <= 0)
        {
            expsnum = true;
        }
        else expsnum = false;


    }
    }
