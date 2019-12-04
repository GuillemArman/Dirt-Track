using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NodeCanvas.Framework;

public class NightCycle : MonoBehaviour
{
    private Blackboard global_BB;
    private GameObject game_manager;
    private GameManager script_gamemanager;
    private MenuManager mm;

    private float time;
    private int speed;
    private bool havetofade = true;

    public bool day = false;
    public bool noon = false;
    public bool night = true;

    public int days = 0;

    public Material skybox1;
    public Material skybox2;
    public Text timetext;

    public GameObject park_light;
    public GameObject scene_light;
    public GameObject open;
    public GameObject closed;

    // Start is called before the first frame update
    void Start()
    {
        game_manager = GameObject.Find("_Game Manager");
        script_gamemanager = game_manager.GetComponent<GameManager>();
        global_BB = game_manager.GetComponent<GlobalBlackboard>();


        time = 28800; // We begin the first journey at 8:00 (3600 * 8)
        days = 0;
        day = true;
        noon = false;
        night = false;

        if(day || noon)
        {
            RenderSettings.skybox = skybox1;
        }
        if(night)
        {
            RenderSettings.skybox = skybox2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChangeTime();
        ParkLights();
        SkyboxChange();
    }

    public void ChangeTime()
    {
        time += Time.deltaTime * speed;
       
        if (time >= 86400) // 24:00
        {
            time = 0;
        }
        else if (time >= 28800 && time <= 55800) // 8:00 to 15:30
        {
            if (havetofade)
            {
                StartCoroutine("FadeIn");

                if (days > 0)
                {
                    global_BB.SetValue("Days", days);
                    script_gamemanager.EnableBalanceSheet();
                }
                days += 1;
                havetofade = false;
            }

            speed = 350;
            day = true;
            noon = false;
            night = false;

            global_BB.SetValue("Day", day);
            global_BB.SetValue("Noon", noon);
            global_BB.SetValue("Night", night);
        }
        else if (time >= 55801 && time <= 75600) // 15:30  to 21:00
        {
            speed = 350;
            day = false;
            noon = true;
            night = false;

            global_BB.SetValue("Day", day);
            global_BB.SetValue("Noon", noon);
            global_BB.SetValue("Night", night);
        }
        else if (time >= 75601 || time <= 28800) // 21:00 to 8:00
        {
            if (!havetofade)
            {
                havetofade = true;
                StartCoroutine("FadeOut");

            }
            speed = 600;
            day = false;
            noon = false;
            night = true;

            global_BB.SetValue("Day", day);
            global_BB.SetValue("Noon", noon);
            global_BB.SetValue("Night", night);
        }

        TimeSpan current_time = TimeSpan.FromSeconds(time);
        string[] temptime = current_time.ToString().Split(":"[0]);
        timetext.text = temptime[0] + ":" + temptime[1];
    }

    public void ParkLights()
    {
        if (day)
        {
            park_light.SetActive(false);
            closed.SetActive(false);
            open.SetActive(true);
        }
        if (night)
        {
            park_light.SetActive(true);
            closed.SetActive(true);
            open.SetActive(false);
        }
    }

    void SkyboxChange()
    {
        if (day)
            RenderSettings.skybox = skybox1;

        if (night)
            RenderSettings.skybox = skybox2;
    }

    IEnumerator FadeIn()
    {
        float duration = 4.0f; // Time you want it to run
        float interval = 0.05f; // Interval time between iterations of while loop

        while (scene_light.GetComponent<Light>().intensity <= 1.6f)
        {
            scene_light.GetComponent<Light>().intensity += 0.02f;
            duration -= interval;
            yield return new WaitForSeconds(interval); // The coroutine will wait for 0.1 secs
        }
    }

    IEnumerator FadeOut()
    {
        float duration = 4.0f; // Time you want it to run
        float interval = 0.05f; // Interval time between iterations of while loop

        while (scene_light.GetComponent<Light>().intensity >= 0.3f)
        {
            scene_light.GetComponent<Light>().intensity -= 0.04f;
            duration -= interval;
            yield return new WaitForSeconds(interval); // The coroutine will wait for 0.1 secs
        }
    }
}
