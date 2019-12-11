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

    public bool quest_completed = false;

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
        switch(days_quest)
        {
            case 1:
                if (days_quest == 1 && visitors_quest < 25)
                {
                    quest_completed = false;
                }
                else
                {
                    quest_completed = true;
                    ButtonQuest.SetActive(true);

                }
                break;
            case 2:
                break;


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
