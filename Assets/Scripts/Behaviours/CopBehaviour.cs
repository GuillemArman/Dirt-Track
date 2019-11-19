using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopBehaviour : MonoBehaviour
{
    private MoveNavMesh nav_move;
    private SteeringWander wander;
    private NightCycle cycle;

    private Vector3 start_pos;
    private bool go_start_pos = false;

    // Start is called before the first frame update
    void Start()
    {
        wander = GetComponent<SteeringWander>();
        nav_move = GetComponent<MoveNavMesh>();
        cycle = GameObject.Find("_Game Manager").GetComponent<NightCycle>();

        start_pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (cycle.night)
        {
            wander.enabled = true;
            go_start_pos = false;
        }
        else
        {
            wander.enabled = false;

            if (!go_start_pos)
            {
                nav_move.SetDestination(start_pos);
                go_start_pos = true;
            }
        }
    }
}
