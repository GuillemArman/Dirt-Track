using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringQueue : MonoBehaviour
{
    [Header("------ Read Only -------")]
    public bool is_in_queue = false;

    [Header("------ Set Values -------")]
    public float max_queue_ahead;
    public float queue_radius;
    public float max_brake_force;
    public my_ray[] rays;

    private Move move; 

    // Use this for initialization
    void Start()
    {
        move = GetComponent<Move>();
    }

    // Update is called once per frame
    void Update()
    {      
        Vector3 ahead = transform.position + (move.velocity.normalized * max_queue_ahead);

        RaycastHit hit;
        float angle = Mathf.Atan2(transform.forward.x, transform.forward.z);
        Quaternion q = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

        foreach (my_ray ray in rays)
        {
            Vector3 direction = Vector3.forward;
            direction.x += ray.direction_offset;

            if (Physics.Raycast(transform.position, q * direction.normalized, out hit, ray.length)
                && !hit.collider.CompareTag("Obstacle"))
            {
                is_in_queue = true;
                break;
            }
            else is_in_queue = false;
        }

        if (is_in_queue)
        {          
            move.SetVelocity(Vector3.zero);       
        }
    }
            
    void OnDrawGizmos()
    {
        float angle = Mathf.Atan2(transform.forward.x, transform.forward.z);
        Quaternion q = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

        foreach (my_ray ray in rays)
        {
            Vector3 direction = Vector3.forward;
            direction.x += ray.direction_offset;

            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, (q * direction.normalized) * ray.length);
        }
    }
}