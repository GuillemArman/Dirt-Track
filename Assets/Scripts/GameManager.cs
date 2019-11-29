using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NodeCanvas.Framework;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GlobalBlackboard bb;
    private CountingVisitors count_visitors;

    public GameObject BuyPanel;
    public GameObject Mechanic;
    public GameObject Truck;
    public GameObject BoxesCar;
    public GameObject cantbuymechanic;

    public Text Money_Text;
    public Text TicketPrice_Text;
    //public Text Currency_Inv;
    public Text Visitors_Text;

    private int Money = 0;
    private int TicketCost = 10;
    //private int Investigating_Points = 0;
    private int Visitors = 0;

    private bool first_time;
    private bool Modify;
    private bool loaded = false;

    // Start is called before the first frame update
    void Start()
    {
        LoadEverything();
        PostLoad();
    }

    // Update is called once per frame
    void Update()
    {
        GetVisitorsnumber();
        Values_To_String();
        CheckMoney();

        if (loaded == false)
        {
            LoadEverything();
        }
    }

    void LoadEverything()
    {
        bb = GetComponent<GlobalBlackboard>();
        count_visitors = GameObject.Find("Stadium").GetComponent<CountingVisitors>();

        Money = 0;
        Visitors = 0;
        TicketCost = 10;
        //Investigating_Points = 0;
       
        bb.SetValue("Money", Money);
        bb.SetValue("TicketPrice", TicketCost);
        //bb.SetValue("Investigating_Points", Investigating_Points);
        bb.SetValue("Visitors", Visitors);
        bb.SetValue("Days", 0);

        bb.SetValue("Mechanic", Mechanic);
        bb.SetValue("Truck", Truck);
        bb.SetValue("BoxesCar", BoxesCar);

        Money_Text.color = Color.white;
        //Currency_Inv.color = Color.white;
        TicketPrice_Text.color = Color.white;
        Visitors_Text.color = Color.white;

        loaded = true;
    }

    void Values_To_String()
    {
        bb.SetValue("TicketPrice", TicketCost);

        Money_Text.text = Money.ToString();
        //Currency_Inv.text = bb.GetValue<int>("Investgating_Points").ToString();      
        TicketPrice_Text.text = TicketCost.ToString();   
        Visitors_Text.text = Visitors.ToString();
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

        if (Money < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (Money > 1000)
        {
            cantbuymechanic.SetActive(false);
        }
        else
        {
            cantbuymechanic.SetActive(true);
        }
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
        Visitors = count_visitors.num_visitors;
        bb.SetValue("Visitors", Visitors);
    }
}
