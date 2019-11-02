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
        }
    }

    void SetInitialTarget()
    {
        int position = Random.Range(1, 5);
        switch (position)
        {
            case 1:
                move.target = queue_1;
                break;
            case 2:
                move.target = queue_2;
                break;
            case 3:
                move.target = queue_3;
                break;
            case 4:
                move.target = queue_4;
                break;
        }

        nav_move.SetDestination(move.target.transform.position);
    }

    void OnDoingQueue()
    {
        queue.wants_to_queue = true;

        if (move.target == queue_1 && arrive.arrived && !ticket_1t.GetComponent<TicketTrigger>().IsTriggerOcuppied())
        {
            move.target = ticket_1t;
            nav_move.SetDestination(move.target.transform.position);
            visitor_action = Action.Buying_ticket;
            
        }
        else if (move.target == queue_2 && arrive.arrived && !ticket_2t.GetComponent<TicketTrigger>().IsTriggerOcuppied())
        {
            move.target = ticket_2t;
            nav_move.SetDestination(move.target.transform.position);
            visitor_action = Action.Buying_ticket;
        }
        else if (move.target == queue_3 && arrive.arrived && !ticket_3t.GetComponent<TicketTrigger>().IsTriggerOcuppied())
        {
            move.target = ticket_3t;
            nav_move.SetDestination(move.target.transform.position);
            visitor_action = Action.Buying_ticket;
        }
        else if (move.target == queue_4 && arrive.arrived && !ticket_4t.GetComponent<TicketTrigger>().IsTriggerOcuppied())
        {
            move.target = ticket_4t;
            nav_move.SetDestination(move.target.transform.position);
            visitor_action = Action.Buying_ticket;
        }
    }

    void OnBuyingTicket()
    {
        if (arrive.arrived)
        {
            time_waiting -= Time.deltaTime;

            if (time_waiting <= 0)
            {
                int position = Random.Range(1, 6);
                switch (position)
                {
                    case 1:
                        move.target = stadium_1t;
                        nav_move.SetDestination(move.target.transform.position);
                        queue.wants_to_queue = false;
                        break;
                    case 2:
                        move.target = stadium_2t;
                        nav_move.SetDestination(move.target.transform.position);
                        queue.wants_to_queue = false;
                        break;
                    case 3:
                        move.target = stadium_3t;
                        nav_move.SetDestination(move.target.transform.position);
                        queue.wants_to_queue = false;
                        break;
                    case 4:
                        move.target = stadium_4t;
                        nav_move.SetDestination(move.target.transform.position);
                        queue.wants_to_queue = false;
                        break;
                    case 5:
                        move.target = stadium_5t;
                        nav_move.SetDestination(move.target.transform.position);
                        queue.wants_to_queue = false;
                        break;
                }

                visitor_action = Action.Going_stadium;
                time_waiting = 3.0f;
            }
        }       
    }
}
