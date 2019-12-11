using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class GoToStadium : ActionTask
{
    private MoveNavMesh nav_move;
    private Vector3 new_target;
    private int random_num = 0;
    private bool decide_path = false;

    public BBParameter<Vector3> bb_stadium1;
    public BBParameter<Vector3> bb_stadium2;
    public BBParameter<Vector3> bb_stadium3;
    public BBParameter<Vector3> bb_stadium4;
    public BBParameter<Vector3> bb_stadium5;

    protected override void OnExecute()
    {
        nav_move = agent.GetComponent<MoveNavMesh>();

        if (!decide_path)
        {
            random_num = Random.Range(1, 6);
            decide_path = true;
        }

        switch (random_num)
        {
            case 1:
                new_target = bb_stadium1.value;
                break;
            case 2:
                new_target = bb_stadium2.value;
                break;
            case 3:
                new_target = bb_stadium3.value;
                break;
            case 4:
                new_target = bb_stadium4.value;
                break;
            case 5:
                new_target = bb_stadium5.value;
                break;
        }
        

        if (nav_move.final_target != new_target)
        {
            nav_move.SetDestination(new_target);
        }

        EndAction();
    }
}
