using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class GoToTicket : ActionTask
{
    private MoveNavMesh nav_move;
    private Vector3 new_target;
    private int random_num = 0;
    private bool decide_path = false;

    public BBParameter<int> ticket_lines;
    public BBParameter<Vector3> bb_ticket1;
    public BBParameter<Vector3> bb_ticket2;
    public BBParameter<Vector3> bb_ticket3;
    public BBParameter<Vector3> bb_ticket4;

    protected override void OnExecute()
    {
        nav_move = agent.GetComponent<MoveNavMesh>();

        if (!decide_path)
        {
            random_num = Random.Range(1, ticket_lines.value + 1);
            decide_path = true;
        }

        switch (random_num)
        {
            case 1:
                new_target = bb_ticket1.value;
                break;
            case 2:
                new_target = bb_ticket2.value;
                break;
            case 3:
                new_target = bb_ticket3.value;
                break;
            case 4:
                new_target = bb_ticket4.value;
                break;
        }

        if (nav_move.final_target != new_target)
        {
            nav_move.SetDestination(new_target);
        }

        EndAction();
    }
}
