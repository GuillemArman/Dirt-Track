using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class EnableMechanicUI : ActionTask
{
    public bool enable_fuel = false;

    protected override void OnExecute()
    {
        if (enable_fuel) agent.transform.Find("Fuel").gameObject.SetActive(true);
        else agent.transform.Find("Fuel").gameObject.SetActive(false);

        EndAction();
    }
}
