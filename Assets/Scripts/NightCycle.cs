using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class NightCycle : MonoBehaviour
{
    

    public float time;
    public TimeSpan current_time;
    public Text timetext;
    public int days;
    public bool day;
    public bool night;

    public GameObject Park_Light;
    public GameObject light_Getout;

    public GameObject Open;
    public GameObject Closed;

    public GameObject Spawner;


    public int speed;

    public bool havetofade = true;
    // Use this for initialization
    void Start()
    {
        time = 18000; // We begin the first journey at 16:00 (3600 * 16)
        days = 1;
        day = true;
        night = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        ChangeTime();
        ParkLights();

    }

    public void ChangeTime()
    {

        time += Time.deltaTime * speed;
        if (time > 86400)
        {
         
            days += 1;
            time = 0;

        }

        else if (time > 28800 && time < 75600) // 8:00  to 21:00
        {
            if (havetofade)
            {
                havetofade = false;
                StartCoroutine("FadeIn");

            }
            speed = 1500;
            day = true;
            night = false;
           
        }
        else if (time > 75600 || time < 28800) //21:00 to 8:00
        {
            if (!havetofade)
            {
                havetofade = true;
                StartCoroutine("FadeOut");

            }
            speed = 800;
            night = true;
            day = false;
          
        }


        else if (time > 86350) // 23:59 AM
        {
            days += 1;
        }

        current_time = TimeSpan.FromSeconds(time);
        string[] temptime = current_time.ToString().Split(":"[0]);
        timetext.text = temptime[0] + ":" + temptime[1];



    }

    public void ParkLights()
    {
        if (day)
        {

            Park_Light.SetActive(false);
            Open.SetActive(false);
            Closed.SetActive(true);
            Spawner.SetActive(true);
        }
        if (night)
        {
            Park_Light.SetActive(true);
            Open.SetActive(true);
            Closed.SetActive(false);
            Spawner.SetActive(false);
        }

    }







    IEnumerator FadeIn()
    {
        float duration = 4.0f;//time you want it to run
        float interval = 0.05f;//interval time between iterations of while loop

        while (light_Getout.GetComponent<Light>().intensity <= 1.6f)
        {
            light_Getout.GetComponent<Light>().intensity += 0.02f;
            duration -= interval;
            yield return new WaitForSeconds(interval);//the coroutine will wait for 0.1 secs
        }
    }

    IEnumerator FadeOut()
    {
        float duration = 4.0f;//time you want it to run
        float interval = 0.05f;//interval time between iterations of while loop

        while (light_Getout.GetComponent<Light>().intensity >= 0.3f)
        {
            light_Getout.GetComponent<Light>().intensity -= 0.02f;
            duration -= interval;
            yield return new WaitForSeconds(interval);//the coroutine will wait for 0.1 secs
        }
    }

}
