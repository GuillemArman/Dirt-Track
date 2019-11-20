using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class GoTo : ActionTask
{
    private MoveNavMesh nav_move;

    private Vector3 new_target;
    public BBParameter<Vector3> bb_target;

    protected override void OnExecute()
    {   
        nav_move = agent.GetComponent<MoveNavMesh>();

        new_target = bb_target.value;
        nav_move.SetDestination(new_target);
        EndAction();
    }
}
