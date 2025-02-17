﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class InitMechanicBB : MonoBehaviour
{
    public enum MechAction { Look_race, Pick_fuel, Go_bike, Go_Tool, Go_Investigate };
    public MechAction mechanic_action;

    private Blackboard mechanic_bb;
    public GameObject investigate_pos;
    public GameObject tv_pos;
    public GameObject tool_pos;
    public GameObject fuel_pos;
    public GameObject bike_pos;

    // Start is called before the first frame update
    void Start()
    {
        mechanic_bb = GetComponent<Blackboard>();

        mechanic_bb.SetValue("investigate_pos", investigate_pos);
        mechanic_bb.SetValue("tv_pos", tv_pos);
        mechanic_bb.SetValue("fuel_pos", fuel_pos);
        mechanic_bb.SetValue("tool_pos", tool_pos);
        mechanic_bb.SetValue("bike_pos", bike_pos);
    }
}


