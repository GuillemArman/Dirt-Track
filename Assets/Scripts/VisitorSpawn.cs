using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorSpawn : MonoBehaviour
{
    public GameObject visitor_1;
    public GameObject queue_1;
    public GameObject queue_2;
    public GameObject queue_3;
    public GameObject queue_4;
    public float duration_spawn;

    private float next_spawn;
    private Move move;
    private MoveNavMesh nav_move;

    // Use this for initialization
    void Start()
    {
        next_spawn = duration_spawn;

        move = visitor_1.GetComponent<Move>();
        nav_move = visitor_1.GetComponent<MoveNavMesh>();       
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

            int position = Random.Range(1, 4);
            switch (position)
            {
                case 1:
                    move.target = queue_1;
                    break;
                case 2:
                    move.target = queue_2;
                    break;
                case 3:
                    move.target = queue_3;
                    break;
                case 4:
                    move.target = queue_4;
                    break;
            }

            nav_move.SetDestination(move.target.transform.position);
        }
    }
}
