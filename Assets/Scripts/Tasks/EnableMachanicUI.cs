using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class EnableMechanicUI : ActionTask
{
    public bool enable_fuel = false;
    public bool enable_tool = false;

    protected override void OnExecute()
    {
        if (enable_fuel) agent.transform.Find("Fuel").gameObject.SetActive(true);
        else agent.transform.Find("Fuel").gameObject.SetActive(false);

        if (enable_tool) agent.transform.Find("Tool").gameObject.SetActive(true);
        else agent.transform.Find("Tool").gameObject.SetActive(false);

        EndAction();
    }
}
