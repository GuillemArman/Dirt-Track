using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class InitVisitorBB : MonoBehaviour
{
    public Blackboard visitor_bb;

    public enum Action { Buying_ticket, Hungry, Go_stadium, Go_home };
    public Action visitor_action;

    // Start is called before the first frame update
    void Start()
    {
        visitor_bb = GetComponent<Blackboard>();

        // Setting triggers and targets
        visitor_bb.SetValue("ticket_1", GameObject.Find("Ticket1_Trigger"));
        visitor_bb.SetValue("ticket_2", GameObject.Find("Ticket2_Trigger"));
        visitor_bb.SetValue("ticket_3", GameObject.Find("Ticket3_Trigger"));
        visitor_bb.SetValue("ticket_4", GameObject.Find("Ticket4_Trigger"));

        visitor_bb.SetValue("food_1", GameObject.Find("Food1_Trigger"));
        visitor_bb.SetValue("food_2", GameObject.Find("Food2_Trigger"));
        visitor_bb.SetValue("food_3", GameObject.Find("Food3_Trigger"));

        visitor_bb.SetValue("stadium_1", GameObject.Find("Stadium1_Trigger"));
        visitor_bb.SetValue("stadium_2", GameObject.Find("Stadium2_Trigger"));
        visitor_bb.SetValue("stadium_3", GameObject.Find("Stadium3_Trigger"));
        visitor_bb.SetValue("stadium_4", GameObject.Find("Stadium4_Trigger"));
        visitor_bb.SetValue("stadium_5", GameObject.Find("Stadium5_Trigger"));

        visitor_bb.SetValue("entrance_1", GameObject.Find("Entrance1_Trigger"));
        visitor_bb.SetValue("entrance_2", GameObject.Find("Entrance2_Trigger"));

        // Billboarding 
        visitor_bb.SetValue("happy", transform.Find("Happy").gameObject);
        visitor_bb.SetValue("sad", transform.Find("Sad").gameObject);
        visitor_bb.SetValue("food", transform.Find("Food").gameObject);
    }
}

   