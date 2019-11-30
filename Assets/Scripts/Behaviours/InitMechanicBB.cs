using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class InitMechanicBB : MonoBehaviour
{
    private Blackboard mechanic_bb;
    public GameObject investigate_pos;
    public GameObject check_pos;

    // Start is called before the first frame update
    void Start()
    {
        mechanic_bb = GetComponent<Blackboard>();

        mechanic_bb.SetValue("investigate_pos", investigate_pos);
        mechanic_bb.SetValue("check_pos", check_pos);
    }
}


