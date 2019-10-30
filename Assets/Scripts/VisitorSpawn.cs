using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorSpawn : MonoBehaviour
{
    public GameObject visitor_1;
    public GameObject first_target;
    public float duration_spawn;

    private float next_spawn;
    private Move move;

    // Use this for initialization
    void Start()
    {
        next_spawn = duration_spawn;

        move = visitor_1.GetComponent<Move>();
        move.SetTarget(first_target);
    }

    // Update is called once per frame
    void Update()
    {
        next_spawn -= Time.deltaTime;

        if (next_spawn <= 0)
        {
            // Clone enemy
            GameObject objectInstance = Instantiate(visitor_1, transform.position, Quaternion.identity);      
            next_spawn = duration_spawn;
        }
    }
}
