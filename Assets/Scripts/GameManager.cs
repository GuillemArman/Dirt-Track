using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NodeCanvas.Framework;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    GlobalBlackboard bb;
    CountingVisitors CV;

    public static GameManager gameManager;
    public enum gameStates { Day, Night, Noon, GameOver };
    public gameStates gameState = gameStates.Day;

    public int score = 0; //total score = money
    public int completedCycles = 0; //Day+Night cycles
    public bool winCondition = false;
    public int winCycles = 3; //number of completed cycles needed to beat the game

    public GameObject Spawner;

    public GameObject BuyPanel;

    public GameObject Mechanic;
    public GameObject Truck;
    public GameObject BoxesCar;

    public Text Currency;
    public string Currency_string;

    public Text Ticket;
    public string Ticket_string;

    public Text Currency_Inv;
    public string Currency_Inv_string;

    public Text Visitors_Text;
    public string Visitors_String;

    public int Money = 0;
    public int TicketCost = 0;
    public int Investigating_Points = 0;
    public int Visitors = 0;
    public int Days = 1;

    bool first_time;

    int tmp;

    public bool Modify;
    public bool loaded = false;


    // Start is called before the first frame update
    void Start()
    {
        LoadEverything();

        PostLoad();

    }

    // Update is called once per frame
    void Update()
    {
        if (loaded != true)
        {
            LoadEverything();
        }


        Values_To_String();
        CheckMoney();
        CycleDay();
        GetVisitorsnumber();




    }

    void LoadEverything()
    {
        if (gameManager == null)
            gameManager = gameObject.GetComponent<GameManager>();

        bb = GetComponent<GlobalBlackboard>();


        Money = 0;
        TicketCost = 10;
        Investigating_Points = 0;
        Visitors = 0;
        Days = 1;
        


        bb.SetValue("Money", Money);
        bb.SetValue("TicketPrice", TicketCost);
        bb.SetValue("Investigating_Points", Investigating_Points);
        bb.SetValue("Visitors", Visitors);
        bb.SetValue("Days", 0);

        bb.SetValue("Mechanic", Mechanic);
        bb.SetValue("Truck", Truck);
        bb.SetValue("BoxesCar", BoxesCar);


        Spawner = GameObject.Find("Spawner");

       

        Currency = GameObject.Find("Money Text").GetComponent<Text>();
        Currency.color = Color.white;

        Currency_Inv = GameObject.Find("Investigating Text").GetComponent<Text>();
        Currency_Inv.color = Color.white;

        Ticket = GameObject.Find("TicketPrice").GetComponent<Text>();
        Ticket.color = Color.white;

        Visitors_Text = GameObject.Find("Audience Text").GetComponent<Text>();
        Visitors_Text.color = Color.white;

        loaded = true;

    }

    void Values_To_String()
    {
        bb.SetValue("Days", Days);
        bb.SetValue("TicketPrice", TicketCost);
        tmp = bb.GetValue<int>("Money");


        Currency_string = bb.GetValue<int>("Money").ToString();
        Currency.text = Currency_string;

        Currency_Inv_string = bb.GetValue<int>("Investgating_Points").ToString();
        Currency_Inv.text = Currency_Inv_string;

        Ticket_string = bb.GetValue<int>("TicketPrice").ToString();
        Ticket.text = Ticket_string;

        Visitors_String = bb.GetValue<int>("Visitors").ToString();
        Visitors_Text.text = Visitors_String;

    }

    void PostLoad()
    {
        bb = GetComponent<GlobalBlackboard>();
        BuyPanel = GameObject.Find("Panel Buy");
        bb.SetValue("Buy Panel", BuyPanel);

        BuyPanel.SetActive(false);

        Mechanic = GameObject.Find("Mechanic_3");
        Truck = GameObject.Find("MonsterTruck");
        BoxesCar = GameObject.Find("Yellow Car");

        bb.SetValue("Mechanic", Mechanic);
        bb.SetValue("Truck", Truck);
        bb.SetValue("BoxesCar", BoxesCar);

        Mechanic.SetActive(false);
        Truck.SetActive(false);
        BoxesCar.SetActive(false);
    }

    void CycleDay()
    {
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

    public void ChangeTicketPrice()
    {
        TicketCost += 5;
        bb.SetValue("TicketPrice", TicketCost);
    }

    public void LessTicketPrice()
    {
        TicketCost -= 5;
        bb.SetValue("TicketPrice", TicketCost);

    }

    public void CheckMoney()
    {
        Money = bb.GetValue<int>("Money");
        TicketCost = bb.GetValue<int>("TicketPrice");

    }

    public void BuyMechanic()
    {
        Money -= 1000;
        Mechanic.SetActive(true);
        Truck.SetActive(true);
        BoxesCar.SetActive(true);

    }

    public void GetVisitorsnumber()
    {
        CV.GetComponent<CountingVisitors>();
        Visitors = CV.num_visitors;
        bb.SetValue("Visitors", Visitors);
    }
 }
