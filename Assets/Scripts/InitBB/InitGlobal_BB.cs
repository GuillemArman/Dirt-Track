﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using UnityEngine.UI;

public class InitGlobal_BB : MonoBehaviour
{
    Blackboard Global_BB;
    int Money = 0;

    public string Currency_string;
    public Text Currency;

    public Text Investigating_Point;
    public string Investigating_Point_string;

    int ValueDay, ValueNV, ValueVip;

    private void Awake()
    {
        Global_BB = GetComponent<GlobalBlackboard>();


        //currency
        Global_BB.SetValue("Money", Money);
        Currency = GameObject.Find("Money Text").GetComponent<Text>();

        Currency_string = Global_BB.GetValue<int>("Money").ToString();
        Currency.text = Currency_string;

       Investigating_Point_string = Global_BB.GetValue<int>("Investigating_Points").ToString();
       Investigating_Point.text = Investigating_Point_string;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void ResetBB()
    {
        Money = 50;
                
        //currency
        Global_BB.SetValue("currency", Money);    
    }
}


