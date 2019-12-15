using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private GameManager GM;
    private CountingVisitors count_visitors;
    private NightCycle nightcycle;

    public int visitors_quest;
    public int days_quest;
    public int money_quest;

    public GameObject ButtonQuest;
    public GameObject ButtonQuest2;
    public GameObject ButtonQuest3;

    public bool quest1_completed = false;
    public bool quest2_completed = false;
    public bool quest3_completed = false;

    // Start is called before the first frame update
    void Start()
    {
        count_visitors = GameObject.Find("Stadium").GetComponent<CountingVisitors>();
        GM = GameObject.Find("_Game Manager").GetComponent<GameManager>();
        nightcycle = GameObject.Find("_Game Manager").GetComponent<NightCycle>();
    }

    // Update is called once per frame
    void Update()
    {
        GetVisitors();
        GetDays();
        GetMoney();

        Money_Quest();
    }

    

    public void Money_Quest()
    {
         if (visitors_quest > 24 && quest2_completed == false && quest3_completed == false)
         {

            quest1_completed = true;
            ButtonQuest.SetActive(true);
         }
         if (visitors_quest > 34 && quest1_completed == true && quest3_completed == false)
        {
            quest2_completed = true;
            ButtonQuest2.SetActive(true);
        }
         if (visitors_quest > 49 && quest1_completed == true && quest2_completed == true)
        {
            quest3_completed = true;
            ButtonQuest3.SetActive(true);
        }

    }

    public void GetVisitors()
    {
        visitors_quest = count_visitors.num_visitors;

    }

    public void GetDays()
    {
        days_quest = nightcycle.days;

    }

    public void GetMoney()
    {
        money_quest = GM.money;
    }

   
}
