using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorBehaviour : MonoBehaviour
{
    public enum Action { To_queue, Doing_queue, Buying_ticket, Hungry, Go_stadium, Go_home, Just_walking };
    public Action visitor_action;

    private Move move;
    private MoveNavMesh nav_move;
    private SteeringQueue queue;
    private SteeringArrive arrive;
    private float time_waiting = 3.0f;
    private NightCycle cycle;

    private GameObject queue_1;
    private GameObject queue_2;
    private GameObject queue_3;
    private GameObject queue_4;

    private GameObject ticket_1t;
    private GameObject ticket_2t;
    private GameObject ticket_3t;
    private GameObject ticket_4t;

    private GameObject food_1t;
    private GameObject food_2t;
    private GameObject food_3t;

    private GameObject stadium_1t;
    private GameObject stadium_2t;
    private GameObject stadium_3t;
    private GameObject stadium_4t;
    private GameObject stadium_5t;

    private GameObject entrance_1t;
    private GameObject entrance_2t;

    private GameObject sad;
    private GameObject happy;
    private GameObject food;

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Move>();
        nav_move = GetComponent<MoveNavMesh>();
        queue = GetComponent<SteeringQueue>();
        arrive = GetComponent<SteeringArrive>();
        cycle = GameObject.Find("_Game Manager").GetComponent<NightCycle>();

        // Setting triggers and targets
        queue_1 = GameObject.Find("Queue1_Target"); 
        queue_2 = GameObject.Find("Queue2_Target"); 
        queue_3 = GameObject.Find("Queue3_Target"); 
        queue_4 = GameObject.Find("Queue4_Target"); 
        
        ticket_1t = GameObject.Find("Ticket1_Trigger"); 
        ticket_2t = GameObject.Find("Ticket2_Trigger"); 
        ticket_3t = GameObject.Find("Ticket3_Trigger"); 
        ticket_4t = GameObject.Find("Ticket4_Trigger"); 
        
        food_1t = GameObject.Find("Food1_Trigger"); 
        food_2t = GameObject.Find("Food2_Trigger"); 
        food_3t = GameObject.Find("Food3_Trigger"); 
        
        stadium_1t = GameObject.Find("Stadium1_Trigger"); 
        stadium_2t = GameObject.Find("Stadium2_Trigger"); 
        stadium_3t = GameObject.Find("Stadium3_Trigger"); 
        stadium_4t = GameObject.Find("Stadium4_Trigger"); 
        stadium_5t = GameObject.Find("Stadium5_Trigger");

        entrance_1t = GameObject.Find("Entrance1_Trigger");
        entrance_2t = GameObject.Find("Entrance2_Trigger");

        // Billboarding 
        happy = transform.Find("Happy").gameObject; happy.SetActive(false);
        sad = transform.Find("Sad").gameObject; sad.SetActive(false);
        food = transform.Find("Food").gameObject; food.SetActive(false);

        // Setting first action
        if (cycle.day || cycle.noon) visitor_action = Action.To_queue;
        else visitor_action = Action.Go_home;
    }

    // Update is called once per frame
    void Update()
    {
        switch(visitor_action)
        {
            case Action.To_queue:
                OnToQueue();
                break;

            case Action.Doing_queue:
                OnDoingQueue();
                break;

            case Action.Buying_ticket:
                OnBuyingTicket();
                break;

            case Action.Hungry:
                OnHungry();
                break;

            case Action.Go_home:
                OnGoHome();
                break;
        }
    }

    private void OnToQueue()
    {
        int position = Random.Range(1, 5);
        switch (position)
        {
            case 1:
                ToQueue(queue_1);
                break;
            case 2:
                ToQueue(queue_2);
                break;
            case 3:
                ToQueue(queue_3);
                break;
            case 4:
                ToQueue(queue_4);
                break;
        }
    }

    private void OnDoingQueue()
    {
        queue.wants_to_queue = true;

        if (move.final_target == queue_1 && arrive.arrived && !ticket_1t.GetComponent<TicketTrigger>().IsTriggerOcuppied())
        {
            ToTicket(ticket_1t);            
        }
        else if (move.final_target == queue_2 && arrive.arrived && !ticket_2t.GetComponent<TicketTrigger>().IsTriggerOcuppied())
        {
            ToTicket(ticket_2t);
        }
        else if (move.final_target == queue_3 && arrive.arrived && !ticket_3t.GetComponent<TicketTrigger>().IsTriggerOcuppied())
        {
            ToTicket(ticket_3t);
        }
        else if (move.final_target == queue_4 && arrive.arrived && !ticket_4t.GetComponent<TicketTrigger>().IsTriggerOcuppied())
        {
            ToTicket(ticket_4t);
        }
    }

    private void OnBuyingTicket()
    {
        if (arrive.arrived)
        {
            time_waiting -= Time.deltaTime;
            int is_hungry = Random.Range(1, 6);

            // Going to stadium
            if (time_waiting <= 0 && is_hungry != 1)
            {
                int position = Random.Range(1, 6);
                switch (position)
                {
                    case 1:
                        ToStadium(stadium_1t);
                        break;
                    case 2:
                        ToStadium(stadium_2t);
                        break;
                    case 3:
                        ToStadium(stadium_3t);
                        break;
                    case 4:
                        ToStadium(stadium_4t);
                        break;
                    case 5:
                        ToStadium(stadium_5t);
                        break;
                }
            }

            // Going to buy food
            if (time_waiting <= 0 && is_hungry == 1)
            {
                int position = Random.Range(1, 4);
                switch (position)
                {
                    case 1:
                        ToFood(food_1t);
                        break;
                    case 2:
                        ToFood(food_2t);
                        break;
                    case 3:
                        ToFood(food_3t);
                        break;
                }
            }
        }
    }

    private void OnHungry()
    {
        food.SetActive(true);

        if (arrive.arrived)
        {
            time_waiting -= Time.deltaTime;

            // Going to stadium
            if (time_waiting <= 0)
            {              
                GameObject[] targets = { stadium_2t, stadium_3t, stadium_4t, stadium_5t };

                GameObject final_target = stadium_1t;
                float smaller_distance = Vector3.Distance(transform.position, stadium_1t.transform.position);

                for (int i = 0; i < targets.Length; i++)
                {
                    float distance = Vector3.Distance(transform.position, targets[i].transform.position);

                    if (smaller_distance > distance)
                    {
                        smaller_distance = distance;
                        final_target = targets[i];
                    }
                }
                food.SetActive(false);
                ToStadium(final_target);
            }
        }
    }

    private void OnGoHome()
    {
        int position = Random.Range(1, 3);
        switch (position)
        {
            case 1:
                nav_move.SetDestination(entrance_1t);
                visitor_action = Action.Just_walking;
                break;
            case 2:
                nav_move.SetDestination(entrance_2t);
                visitor_action = Action.Just_walking;
                break;
        }

        // Deciding randomly visitor's mood
        position = Random.Range(1, 10);
        switch (position)
        {
            case 1:
                happy.SetActive(true);
                break;
            case 2:
                sad.SetActive(true);
                break;
        }
    }

    private void ToQueue(GameObject target)
    {
        nav_move.SetDestination(target);
        visitor_action = Action.Doing_queue;
    }

    private void ToTicket(GameObject target)
    {
        nav_move.SetDestination(target);
        visitor_action = Action.Buying_ticket;
    }

    private void ToStadium(GameObject target)
    {
        nav_move.SetDestination(target);
        queue.wants_to_queue = false;

        visitor_action = Action.Go_stadium;
        time_waiting = 3.0f;
    }

    private void ToFood(GameObject target)
    {
        nav_move.SetDestination(target);

        visitor_action = Action.Hungry;
        time_waiting = 3.0f;
    }
}

