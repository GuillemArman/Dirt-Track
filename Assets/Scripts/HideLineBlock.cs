using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class HideLineBlock : MonoBehaviour
{
    private Blackboard gamemanager_bb;
    private bool up = true;
    private bool down = false;

    public float velocity = 1.0f;
    public int ticket_line;

    // Start is called before the first frame update
    void Start()
    {
        gamemanager_bb = GameObject.Find("_Game Manager").GetComponent<Blackboard>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gamemanager_bb.GetValue<bool>("Night") && gamemanager_bb.GetValue<int>("TicketLines") < ticket_line)
        {
            if (transform.position.y < 120.15f && !up)
                transform.position += new Vector3(0, velocity * Time.deltaTime, 0);
            else up = true;

            down = false;
        }
        else
        {     
            if (transform.position.y > 115.0f && !down)
                transform.position -= new Vector3(0, velocity * Time.deltaTime, 0);
            else down = true;

            up = false;
        }
    }
}
