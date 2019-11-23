using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NodeCanvas.Framework;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    GlobalBlackboard bb;


    public static GameManager gameManager;
    public enum gameStates { Day, Night, Noon, GameOver };
    public gameStates gameState = gameStates.Day;

    public int score = 0; //total score = money
    public int completedCycles = 0; //Day+Night cycles
    public bool winCondition = false;
    public int winCycles = 3; //number of completed cycles needed to beat the game

    public GameObject Spawner;

    public Text Currency;
    public string Currency_string;

    public int Money = 0;
    public int Investigating_Points = 0;
    public int Days = 1;

    bool first_time;

    int tmp;
    public bool loaded = false;

    // Start is called before the first frame update
    void Start()
    {
        LoadEverything();
    }

    // Update is called once per frame
    void Update()
    {
        if (loaded != true)
        {
            LoadEverything();
        }

        bb.SetValue("Days", Days);

        switch (gameState)
        {
            case gameStates.Day:

                if (first_time == true)
                {
                    
                   
                    Days += 1;
                   
                    first_time = false;

                }
               


                Spawner.SetActive(false);
                break;
            case gameStates.Night:

                Spawner.SetActive(true);

                first_time = true;

           
                break;
            case gameStates.GameOver:
                break;

        }

    

    }

    void LoadEverything()
    {
        if (gameManager == null)
            gameManager = gameObject.GetComponent<GameManager>();

        bb = GetComponent<GlobalBlackboard>();


        Money = 20;
        Investigating_Points = 0;
        Days = 1;


        bb.SetValue("Money", Money);
        bb.SetValue("Investigating_Points", Investigating_Points);
        bb.SetValue("Days", 0);

        Spawner = GameObject.Find("Spawner");

        loaded = true;

    }

 }
