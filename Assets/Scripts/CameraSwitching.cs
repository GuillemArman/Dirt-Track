using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitching : MonoBehaviour
{

    public Camera[] cams;


    // Start is called before the first frame update
    void Start()
    {
        cams[0].enabled = true;
        cams[1].enabled = false;
        cams[2].enabled = false;
        cams[3].enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Camera1()
    {
        cams[0].enabled = true;
        cams[1].enabled = false;
        cams[2].enabled = false;
        cams[3].enabled = false;

    }

    public void Camera2()
    {
        cams[0].enabled = false;
        cams[1].enabled = true;
        cams[2].enabled = false;
        cams[3].enabled = false;
    }

    public void Camera3()
    {
        cams[0].enabled = false;
        cams[1].enabled = false;
        cams[2].enabled = true;
        cams[3].enabled = false;
    }

    public void Camera4()
    {
        cams[0].enabled = false;
        cams[1].enabled = false;
        cams[2].enabled = false;
        cams[3].enabled = true;
    }
}
