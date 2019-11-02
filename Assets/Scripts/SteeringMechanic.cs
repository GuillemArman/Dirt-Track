using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringMechanic : MonoBehaviour
{

    private Move move;
    private MoveNavMesh nav_move;

    NightCycle cycle;
    public bool isDay;
    public bool isNight;

    public GameObject investigate_pos;

    public GameObject check_pos;




    // Start is called before the first frame update
    void Start()
    {
        isDay = GameObject.Find("_Game Manager").GetComponent<NightCycle>().day;
        isNight = GameObject.Find("_Game Manager").GetComponent<NightCycle>().night;

        move = GetComponent<Move>();
        nav_move = GetComponent<MoveNavMesh>();

    }

    // Update is called once per frame
    void Update()
    {
        isDay = GameObject.Find("_Game Manager").GetComponent<NightCycle>().day;
        isNight = GameObject.Find("_Game Manager").GetComponent<NightCycle>().night;

        Investigate();
        CheckVehicles();
    }

    void Investigate()
    {

        if (isDay)
        {
            nav_move.SetDestination(investigate_pos.transform.position);

            move.target = investigate_pos;
        }



    }

    void CheckVehicles()
    {
        if (isNight)
        {
            nav_move.SetDestination(check_pos.transform.position);
            move.target = check_pos;

        }

    }
}


