using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class EnableBillboarding : ActionTask
{
    public bool enable_happy = false;
    public bool enable_sad = false;
    public bool enable_food = false;

    protected override void OnExecute()
    {
        if(enable_happy) agent.transform.Find("Happy").gameObject.SetActive(true);
        else agent.transform.Find("Happy").gameObject.SetActive(false);

        if (enable_sad) agent.transform.Find("Sad").gameObject.SetActive(true);
        else agent.transform.Find("Sad").gameObject.SetActive(false);

        if (enable_food) agent.transform.Find("Food").gameObject.SetActive(true);
        else agent.transform.Find("Food").gameObject.SetActive(false);

        EndAction();
    }
}