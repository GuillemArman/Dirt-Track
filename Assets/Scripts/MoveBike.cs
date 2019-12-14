using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveBike : MonoBehaviour
{
    private NightCycle cycle;
    private NavMeshAgent nav_agent;
    private FuelRemaining fuel_remaining;
    private GameObject targetWayPoint;

    public bool colliding = false;
    public GameObject[] wayPointList; // put the points from unity interface
    public int currentWayPoint = 0;
    public float speed = 7f;

    // Use this for initialization
    void Start()
    {
        cycle = GameObject.Find("_Game Manager").GetComponent<NightCycle>();
        nav_agent = GetComponent<NavMeshAgent>();
        fuel_remaining = GetComponent<FuelRemaining>();
    }

    // Update is called once per frame
    void Update()
    {       
        // check if we have somewere to walk
        if (currentWayPoint < this.wayPointList.Length)
        {
            if (targetWayPoint == null)
                targetWayPoint = wayPointList[currentWayPoint];

            Walk();
        }
    }

    void Walk()
    {
        float aux = (transform.position - targetWayPoint.transform.position).magnitude;

        if (cycle.day || cycle.noon)
        {
            if (aux <= 0.3f)
            {
                if (currentWayPoint == 10)
                {
                    currentWayPoint = 0;
                    targetWayPoint = wayPointList[currentWayPoint];
                }
                else
                {
                    currentWayPoint++;
                    targetWayPoint = wayPointList[currentWayPoint];
                }
            }

            // rotate towards the target
            transform.forward = Vector3.RotateTowards(transform.forward, targetWayPoint.transform.position - transform.position, speed * Time.deltaTime, 0.0f);
            if (fuel_remaining.GetFuel() > 0)
            {
                // move towards the target
                transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.transform.position, speed * Time.deltaTime);
            }
        }
        if (cycle.night)
        {
            if (aux <= 0.3f)
            {
                if (currentWayPoint != 0)
                {
                    if (currentWayPoint == 10)
                    {
                        currentWayPoint = 0;
                        targetWayPoint = wayPointList[currentWayPoint];
                    }
                    else
                    {
                        currentWayPoint++;
                        targetWayPoint = wayPointList[currentWayPoint];
                    }
                }
            }

            if (fuel_remaining.GetFuel() > 0)
            {
                // rotate towards the target
                transform.forward = Vector3.RotateTowards(transform.forward, targetWayPoint.transform.position - transform.position, speed * Time.deltaTime, 0.0f);

                if (currentWayPoint != 0)
                {
                    // move towards the target
                    transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.transform.position, speed * Time.deltaTime);
                }
                else if (currentWayPoint == 0 && aux > 2 && !colliding)
                {
                    // move towards the target
                    transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.transform.position, speed * Time.deltaTime);
                }
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Bike"))
        {
            colliding = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        colliding = false;
    }
}
