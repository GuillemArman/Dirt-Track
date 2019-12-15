using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class GoToClick : ActionTask
{
    private MoveNavMesh nav_move;
    private ClickToPos click_pos;
    private Vector3 new_target;

    protected override void OnExecute()
    {
        nav_move = agent.GetComponent<MoveNavMesh>();
        click_pos = agent.GetComponent<ClickToPos>();
        new_target = click_pos.position;

        if (nav_move.final_target != new_target)
        {
            nav_move.SetDestination(new_target);
        }

        EndAction();
    }
}
