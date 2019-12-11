using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class GoToFood : ActionTask
{ 
    private MoveNavMesh nav_move;
    private Vector3 new_target;
    private int random_num = 0;
    private bool decide_path = false;

    public BBParameter<int> food_carts;
    public BBParameter<Vector3> bb_food1;
    public BBParameter<Vector3> bb_food2;
    public BBParameter<Vector3> bb_food3;

    protected override void OnExecute()
    {
        nav_move = agent.GetComponent<MoveNavMesh>();

        if (!decide_path)
        {
            random_num = Random.Range(1, food_carts.value + 1);
            decide_path = true;
        }

        switch (random_num)
        {
            case 1:
                new_target = bb_food1.value;
                break;
            case 2:
                new_target = bb_food2.value;
                break;
            case 3:
                new_target = bb_food3.value;
                break;
        }
        
        if (nav_move.final_target != new_target)
        {
            nav_move.SetDestination(new_target);
        }

        EndAction();
    }
}
