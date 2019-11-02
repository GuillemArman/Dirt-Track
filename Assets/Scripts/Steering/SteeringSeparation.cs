using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringSeparation : MonoBehaviour
{
    public float search_radius = 1.0f;
    public float max_repulsion = 0.07f;

    private Move move;
    private SteeringQueue queue;

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Move>();
        queue = GetComponent<SteeringQueue>();
    }

    // Update is called once per frame
    void Update()
    {
        int layer_id = 9;
        int layer_mask = 1 << layer_id;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, search_radius, layer_mask);

        if (!queue.is_in_queue)
        {
            Vector3 steering_force = Vector3.zero;
            for (int i = 0; i < hitColliders.Length; i++)
            {
                Vector3 force = hitColliders[i].transform.position - transform.position;
                force = force * -max_repulsion;
                steering_force += force;
            }

            move.AddVelocity(steering_force);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, search_radius);
    }
}
