using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class EnableCopUI : ActionTask
{
    public bool enable_detected = false;

    protected override void OnExecute()
    {
        if (enable_detected) agent.transform.Find("Detected").gameObject.SetActive(true);
        else agent.transform.Find("Detected").gameObject.SetActive(false);

        EndAction();
    }
}