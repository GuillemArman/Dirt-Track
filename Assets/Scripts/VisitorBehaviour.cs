using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorBehaviour : MonoBehaviour
{
    private enum Action { Doing_queue, Buying_ticket, Hungry, Going_stadium };
    private Move move;
    private MoveNavMesh nav_move;
    private SteeringQueue queue;
    private SteeringArrive arrive;
    private Action visitor_action;
    private float time_waiting = 3.0f;

    public GameObject queue_1;
    public GameObject queue_2;
    public GameObject queue_3;
    public GameObject queue_4;

    public GameObject ticket_1t;
    public GameObject ticket_2t;
    public GameObject ticket_3t;
    public GameObject ticket_4t;

    public GameObject food_1t;
    public GameObject food_2t;
    public GameObject food_3t;

    public GameObject stadium_1t;
    public GameObject stadium_2t;
    public GameObject stadium_3t;
    public GameObject stadium_4t;
    public GameObject stadium_5t;

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Move>();
        nav_move = GetComponent<MoveNavMesh>();
        queue = GetComponent<SteeringQueue>();
        arrive = GetComponent<SteeringArrive>();
        visitor_action = Action.Doing_queue;

        // Setting triggers
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
        
        stadium_1t = GameObject.Find("Entrance1_Trigger"); 
        stadium_2t = GameObject.Find("Entrance2_Trigger"); 
        stadium_3t = GameObject.Find("Entrance3_Trigger"); 
        stadium_4t = GameObject.Find("Entrance4_Trigger"); 
        stadium_5t = GameObject.Find("Entrance5_Trigger"); 

        SetInitialTarget();
    }

    // Update is called once per frame
    void Update()
    {
        switch(visitor_action)
        {
            case Action.Doing_queue:
                OnDoingQueue();
                break;

            case Action.Buying_ticket:
                OnBuyingTicket();
                break;

            case Action.Hungry:
                OnHungry();
                break;
        }
    }

    void SetInitialTarget()
    {
        int position = Random.Range(1, 5);
        switch (position)
        {
            case 1:
                move.final_target = queue_1;
                break;
            case 2:
                move.final_target = queue_2;
                break;
            case 3:
                move.final_target = queue_3;
                break;
            case 4:
                move.final_target = queue_4;
                break;
        }

        nav_move.SetDestination(move.final_target);
    }

    void OnDoingQueue()
    {
        queue.wants_to_queue = true;

        if (move.final_target == queue_1 && arrive.arrived && !ticket_1t.GetComponent<TicketTrigger>().IsTriggerOcuppied())
        {
            QueueToTicket(ticket_1t);            
        }
        else if (move.final_target == queue_2 && arrive.arrived && !ticket_2t.GetComponent<TicketTrigger>().IsTriggerOcuppied())
        {
            QueueToTicket(ticket_2t);
        }
        else if (move.final_target == queue_3 && arrive.arrived && !ticket_3t.GetComponent<TicketTrigger>().IsTriggerOcuppied())
        {
            QueueToTicket(ticket_3t);
        }
        else if (move.final_target == queue_4 && arrive.arrived && !ticket_4t.GetComponent<TicketTrigger>().IsTriggerOcuppied())
        {
            QueueToTicket(ticket_4t);
        }
    }

    void QueueToTicket(GameObject target)
    {
        nav_move.SetDestination(target);
        visitor_action = Action.Buying_ticket;
    }

    void OnBuyingTicket()
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

    void OnHungry()
    {
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

                ToStadium(final_target);
            }
        }
    }

    void ToStadium(GameObject target)
    {
        nav_move.SetDestination(target);
        queue.wants_to_queue = false;

        visitor_action = Action.Going_stadium;
        time_waiting = 3.0f;
    }

    void ToFood(GameObject target)
    {
        nav_move.SetDestination(target);

        visitor_action = Action.Hungry;
        time_waiting = 3.0f;
    }
}

