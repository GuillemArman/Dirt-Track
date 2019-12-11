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
    
    private int ticket_cost = 10;
    private int num_mechanics = 2;
    private int mechanic_cost = 250;
    private int foodcart_cost = 50;
    private int taxes_day = 250;
    private int income_day = 0; // per day without taking into account expenses
    private int expenses_day = 0; 
    private int savings_day = 0;
    private int visitors = 0;
    private int food_carts = 1;
    private int ticket_lines = 2;

    public int money = 0; // total
    public float popularity = 1;

    [Header("------ Progression System ------")]
    public GameObject yellow_team;
    public GameObject red_team;
    public GameObject cant_buy_mechanic;

    public GameObject food_cart1;
    public GameObject food_cart2;
    public GameObject cant_buy_foodcart;

    [Header("------ Ui texts ------")]
    public Text money_text;
    public Text ui_ticketprice_text;
    public Text ticketprice_text;
    public Text visitors_text;
    public Text taxes_text;
    public Text popularity_text;

    [Header("------ Balance sheet ------")]
    public GameObject balance_sheet;
    public Text day;
    public Text savings;
    public Text income;
    public Text taxes;
    public Text expenses;
    public Text final_money;

    // Start is called before the first frame update
    void Start()
    {
        LoadEverything();
    }

    // Update is called once per frame
    void Update()
    {
        CheckVisitorsNumber();
        CheckMoneyMechanic();
        CheckMoneyFoodCart();
        UpdateDataUI();
    }

    void LoadEverything()
    {
        bb = GetComponent<GlobalBlackboard>();
        count_visitors = GameObject.Find("Stadium").GetComponent<CountingVisitors>();

        money = 0;
        visitors = 0;
        ticket_cost = 10;
        mechanic_cost = 1; //250
        foodcart_cost = 1; //50
        num_mechanics = 2;
        taxes_day = 250;
        savings_day = 0;
        popularity = 1;
        food_carts = 1;
        ticket_lines = 2;

        bb.SetValue("Money", money);
        bb.SetValue("Income", income_day);
        bb.SetValue("TicketPrice", ticket_cost);
        bb.SetValue("Days", 0);
        bb.SetValue("Popularity", popularity);
        bb.SetValue("FoodCarts", food_carts);
        bb.SetValue("TicketLines", ticket_lines);
    }

    void UpdateDataUI()
    {
        money_text.text = money.ToString();  
        ui_ticketprice_text.text = ticket_cost.ToString();
        ticketprice_text.text = ticket_cost.ToString();
        ticketprice_text.color = Color.black;
        visitors_text.text = visitors.ToString();
        taxes_text.text = taxes_day.ToString();
        popularity_text.text = popularity.ToString();
    }

    public void ChangeTicketPrice()
    {
        ticket_cost += 5;
        bb.SetValue("TicketPrice", ticket_cost);
    }

    public void LessTicketPrice()
    {
        if (ticket_cost != 0)
        {
            ticket_cost -= 5;
            bb.SetValue("TicketPrice", ticket_cost);
        }
    }

    public void CheckMoneyMechanic()
    {
        money = bb.GetValue<int>("Money");
        income_day = bb.GetValue<int>("Income");
        if (money >= mechanic_cost && red_team.activeSelf == false)
        {
            cant_buy_mechanic.SetActive(false);
        }
        else
        {
            cant_buy_mechanic.SetActive(true);
        }
    }

    public void BuyMechanic()
    {
        money -= mechanic_cost;
        expenses_day += mechanic_cost;
        popularity += 0.5f;
        taxes_day += 50;
        num_mechanics++;
        bb.SetValue("Money", money);

        if (yellow_team.activeSelf == false)
        {
            yellow_team.SetActive(true);
        }
        else
        {
            red_team.SetActive(true);
            cant_buy_mechanic.SetActive(true);
            cant_buy_mechanic.GetComponentInChildren<Text>().text = "SOLD OUT".ToString();
        }
    }

    public void CheckMoneyFoodCart()
    {
        money = bb.GetValue<int>("Money");
        income_day = bb.GetValue<int>("Income");
        if (money >= foodcart_cost && food_carts < 3)
        {
            cant_buy_foodcart.SetActive(false);
        }
        else
        {
            cant_buy_foodcart.SetActive(true);
        }
    }

    public void BuyFoodCart()
    {    
        money -= foodcart_cost;
        expenses_day += foodcart_cost;
        popularity += 0.5f;
        taxes_day += 15;
        bb.SetValue("Money", money);

        if (food_cart1.activeSelf == false && food_carts == 1)
        {           
            food_cart1.SetActive(true);
        }
        else
        {
            food_cart2.SetActive(true);
            cant_buy_foodcart.SetActive(true);
            cant_buy_foodcart.GetComponentInChildren<Text>().text = "SOLD OUT".ToString();
        }

        food_carts++;
        bb.SetValue("FoodCarts", food_carts);
    }

    public void Quest1Completed()
    {
        money += 50;
        bb.SetValue("Money", money);      
    }

    public void CheckVisitorsNumber()
    {
        visitors = count_visitors.num_visitors;       
    }

    // This fncs only has to be called once
    public void EnableBalanceSheet()
    {
        // Change camera
        CameraSwitching camera_switch = GetComponent<CameraSwitching>();
        camera_switch.Camera5();

        // Set active balance sheet
        balance_sheet.SetActive(true);

        // Set values to text
        day.GetComponent<Text>().text = bb.GetValue<int>("Days").ToString();
        income.GetComponent<Text>().text = income_day.ToString();
        expenses.GetComponent<Text>().text = (-expenses_day).ToString();
        taxes.GetComponent<Text>().text = (-taxes_day).ToString();
        savings.GetComponent<Text>().text = savings_day.ToString();
        int p = savings_day + income_day - expenses_day - taxes_day;
        final_money.GetComponent<Text>().text = p.ToString();

        // This pauses the game
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        // Continue game
        Time.timeScale = 1;

        // Change camera
        CameraSwitching camera_switch = GetComponent<CameraSwitching>();
        camera_switch.Camera1();

        // Disable balance sheet
        balance_sheet.SetActive(false);

        // Setting values
        money -= taxes_day;
        bb.SetValue("Money", money);
        income_day = 0;
        bb.SetValue("Income", income_day);
        savings_day = bb.GetValue<int>("Money");
        expenses_day = 0;

        // Check money to see if we lose
        if (bb.GetValue<int>("Money") < 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public float GetPopularity()
    {
        return popularity;
    }

    public float GetVisitors()
    {
        return visitors;
    }
}
