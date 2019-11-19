using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class SteeringWander : MonoBehaviour
{
    private MoveNavMesh nav_move;
    private SteeringArrive arrive;

    public float walk_radius = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        nav_move = GetComponent<MoveNavMesh>();
        arrive = GetComponent<SteeringArrive>();

        nav_move.curr_target = transform.position;
        if (arrive.arrived)
            Wander();
    }

    // Update is called once per frame
    void Update()
    {
        if (arrive.arrived)
            Wander();
    }

    void Wander()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walk_radius;
        randomDirection += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walk_radius, 8);
        nav_move.SetDestination(hit.position);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, walk_radius);
    }
}
