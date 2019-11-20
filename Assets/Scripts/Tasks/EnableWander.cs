using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class EnableWander : ActionTask
{
    private SteeringWander wander;
    public bool enabled;
    
    protected override void OnExecute()
    {
        wander = agent.GetComponent<SteeringWander>();

        if (enabled) wander.enabled = true;
        else wander.enabled = false;

        EndAction();
    }
}