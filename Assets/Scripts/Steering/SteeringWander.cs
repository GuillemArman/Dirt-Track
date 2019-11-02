using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class SteeringWander : MonoBehaviour
{
    private Move move;
    private MoveNavMesh nav_move;
    private SteeringArrive arrive;

    public float walk_radius = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Move>();
        nav_move = GetComponent<MoveNavMesh>();
        arrive = GetComponent<SteeringArrive>();

        move.SetCurrTarget(transform.position);
        Wander();
    }

    // Update is called once per frame
    void Update()
    {
        if (arrive.arrived)
        {
            // Deleting prev GO target
            Destroy(move.final_target);
            Wander();
        }
    }

    void Wander()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walk_radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walk_radius, 1);

        // Creating new GO target
        GameObject final_target = new GameObject();
        final_target.transform.position = hit.position;
        nav_move.SetDestination(final_target);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, walk_radius);
    }
}
