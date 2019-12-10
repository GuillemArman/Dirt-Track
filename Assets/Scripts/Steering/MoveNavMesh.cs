using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class MoveNavMesh : MonoBehaviour
{
    private SteeringArrive arrive = null;
    private SteeringQueue queue = null;
    private NavMeshAgent nav_agent = null;

    private Vector3[] path;

    [Header("------ Read Only -------")]
    public Vector3 final_target = Vector3.zero;
    public Vector3 curr_target = Vector3.zero;
    public int progress = 0;

    [Header("------ Set Values ------")]
    public float path_point_distance = 1.5f;

    // Use this for initialization
    void Start()
    {
        arrive = GetComponent<SteeringArrive>();
        queue = GetComponent<SteeringQueue>();
        nav_agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        nav_agent.isStopped = true;
        nav_agent.updateRotation = false;

        if (nav_agent != null && nav_agent.hasPath && !queue.is_in_queue)
        {
            if (!nav_agent.pathEndPosition.Equals(path[path.Length - 1]))
                path = nav_agent.path.corners;

            if (progress < path.Length)
            {
                if (Vector3.Distance(transform.position, path[progress]) < path_point_distance)
                {
                    progress++;
                }
                if (progress < path.Length)
                {
                    curr_target = path[progress];
                }
            }
        }
    }

    public bool IsPathFinishing()
    {
        return (progress >= path.Length - 1);
    }

    public void SetDestination(Vector3 dest)
    {
        progress = 0;
        final_target = dest;
        curr_target = dest;
        arrive.arrived = false;

        NavMeshPath nav_path = new NavMeshPath();
        nav_agent.SetDestination(dest);
        nav_agent.CalculatePath(dest, nav_path);
        path = nav_agent.path.corners;
    }

    public void OnDrawGizmos()
    {
        if (nav_agent != null && nav_agent.hasPath)
        {
            for (int i = 0; i < nav_agent.path.corners.Length - 1; i++)
                Debug.DrawLine(nav_agent.path.corners[i], nav_agent.path.corners[i + 1], Color.red);
        }
    }
}
