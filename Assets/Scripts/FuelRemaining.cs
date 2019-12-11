using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NodeCanvas.Framework;

public class FuelRemaining : MonoBehaviour
{
    private Blackboard mechanic_bb;
    public GameObject mechanic;
    public Image fuel_bar;
    public float fuel_consumption = 0.005f;

    private float fuel = 6.0f;
    private NightCycle cycle;

    // Start is called before the first frame update
    void Start()
    {
        cycle = GameObject.Find("_Game Manager").GetComponent<NightCycle>();
        mechanic_bb = mechanic.GetComponent<Blackboard>();
        mechanic_bb.SetValue("fuel", fuel);
    }

    // Update is called once per frame
    void Update()
    {
        fuel = mechanic_bb.GetValue<float>("fuel");

        if (fuel > 0)
        {
            if(!cycle.night && Time.timeScale != 0) fuel -= fuel_consumption;
            fuel_bar.rectTransform.sizeDelta = new Vector2(fuel, 0.65f);
            mechanic_bb.SetValue("bike_need_fuel", false);
            mechanic_bb.SetValue("fuel", fuel);
        }
        else
        {
            mechanic_bb.SetValue("bike_need_fuel", true);
        }
    }

    public float GetFuel()
    {
        return fuel;
    }
}
