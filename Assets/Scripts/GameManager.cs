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

    private int Money = 0;
    private int TicketCost = 10;
    private int num_mechanics = 2;
    private int Visitors = 0;
    private int mechanic_cost = 250;

    private bool first_time;
    private bool Modify;
    private bool loaded = false;

    public GameObject yellow_team;
    public GameObject red_team;
    public GameObject cantbuymechanic;
    public Text Money_Text;
    public Text uiTicketPrice_Text;
    public Text TicketPrice_Text;
    public Text num_mechanics_text;
    public Text Visitors_Text;

    // Start is called before the first frame update
    void Start()
    {
        LoadEverything();
    }

    // Update is called once per frame
    void Update()
    {
        GetVisitorsnumber();
        Values_To_String();
        CheckMoney();

        if (loaded == false)
            LoadEverything();
    }

    void LoadEverything()
    {
        bb = GetComponent<GlobalBlackboard>();
        count_visitors = GameObject.Find("Stadium").GetComponent<CountingVisitors>();

        Money = 0;
        Visitors = 0;
        TicketCost = 10;
        mechanic_cost = 250;
        num_mechanics = 2;
       
        bb.SetValue("Money", Money);
        bb.SetValue("TicketPrice", TicketCost);
        bb.SetValue("Mechanics", num_mechanics);
        bb.SetValue("Visitors", Visitors);
        bb.SetValue("Days", 0);

        loaded = true;
    }

    void Values_To_String()
    {
        bb.SetValue("TicketPrice", TicketCost);

        Money_Text.text = Money.ToString();
        num_mechanics_text.text = num_mechanics.ToString();    
        uiTicketPrice_Text.text = TicketCost.ToString();
        TicketPrice_Text.text = TicketCost.ToString();
        TicketPrice_Text.color = Color.black;
        Visitors_Text.text = Visitors.ToString();
    }

    public void ChangeTicketPrice()
    {
        TicketCost += 5;
        bb.SetValue("TicketPrice", TicketCost);
    }

    public void LessTicketPrice()
    {
        if (TicketCost != 0)
        {
            TicketCost -= 5;
            bb.SetValue("TicketPrice", TicketCost);
        }
    }

    public void PayFixedCosts()
    {
        Money -= 300;
        bb.SetValue("Money", Money);
    }

    public void CheckMoney()
    {
        Money = bb.GetValue<int>("Money");

        if (Money < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (Money > mechanic_cost && red_team.activeSelf == false)
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
        Money -= mechanic_cost;
        num_mechanics++;
        bb.SetValue("Money", Money);
        bb.SetValue("Mechanics", num_mechanics);

        if (yellow_team.activeSelf == false) yellow_team.SetActive(true);
        else if (yellow_team.activeSelf == true)
        {
            red_team.SetActive(true);
            cantbuymechanic.SetActive(true);
            cantbuymechanic.GetComponentInChildren<Text>().text = "".ToString();
        }
    }

    public void GetVisitorsnumber()
    {
        Visitors = count_visitors.num_visitors;
        bb.SetValue("Visitors", Visitors);
    }
}
