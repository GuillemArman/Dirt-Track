using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveBike : MonoBehaviour
{
    NightCycle cycle;

    // put the points from unity interface
    public GameObject[] wayPointList;

    public int currentWayPoint = 0;
    GameObject targetWayPoint;

    public float speed = 7f;

    // Use this for initialization
    void Start()
    {
        cycle = GameObject.Find("_Game Manager").GetComponent<NightCycle>();
    }

    // Update is called once per frame
    void Update()
    {       
        // check if we have somewere to walk
        if (currentWayPoint < this.wayPointList.Length)
        {
            if (targetWayPoint == null)
                targetWayPoint = wayPointList[currentWayPoint];

            walk();
        }
    }

    void walk()
    {
        float aux = (transform.position - targetWayPoint.transform.position).magnitude;
        // rotate towards the target
        transform.forward = Vector3.RotateTowards(transform.forward, targetWayPoint.transform.position - transform.position, speed * Time.deltaTime, 0.0f);

        // move towards the target
        transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.transform.position, speed * Time.deltaTime);
        if (cycle.day || cycle.noon)
        {
            if (aux <= 1)
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
        if (cycle.night)
        {
            if (aux <= 1)
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
        }
    }
}
