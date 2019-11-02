using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopBehaviour : MonoBehaviour
{
    private Move move;
    private MoveNavMesh nav_move;
    private SteeringWander wander;
    private SteeringArrive arrive;
    private NightCycle cycle;
    private Vector3 start_pos;

    private bool go_start = false;

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Move>();
        wander = GetComponent<SteeringWander>();
        nav_move = GetComponent<MoveNavMesh>();
        arrive = GetComponent<SteeringArrive>();
        cycle = GameObject.Find("_Game Manager").GetComponent<NightCycle>();

        start_pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (cycle.night)
        {
            wander.enabled = true;
            go_start = false;
        }
        else
        {
            wander.enabled = false;
            GameObject target = new GameObject();
            target.transform.position = start_pos;

            if (!go_start)
            {
                Destroy(move.final_target);
                nav_move.SetDestination(target);
            }
            else Destroy(target);

            go_start = true;
        }
    }
}
