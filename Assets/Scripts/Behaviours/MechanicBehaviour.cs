using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicBehaviour : MonoBehaviour
{
    private MoveNavMesh nav_move;
    private NightCycle cycle;

    public GameObject investigate_pos;
    public GameObject check_pos;

    // Start is called before the first frame update
    void Start()
    {
        nav_move = GetComponent<MoveNavMesh>();

        nav_move.curr_target = transform.position;
        cycle = GameObject.Find("_Game Manager").GetComponent<NightCycle>();
    }

    // Update is called once per frame
    void Update()
    {
        Investigate();
        CheckVehicles();
    }

    void Investigate()
    {
        if (cycle.day || cycle.noon)
        {
            nav_move.SetDestination(investigate_pos.transform.position);
        }
    }

    void CheckVehicles()
    {
        if (cycle.night)
        {
            nav_move.SetDestination(check_pos.transform.position);
        }
    }
}


