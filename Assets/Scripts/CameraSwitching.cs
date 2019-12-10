using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitching : MonoBehaviour
{
    public Camera[] cams;
    private Camera curr_camera;
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateCamera(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Camera1()
    {
        UpdateCamera(0);
    }

    public void Camera2()
    {
        UpdateCamera(1);
    }

    public void Camera3()
    {
        UpdateCamera(2);
    }

    public void Camera4()
    {
        UpdateCamera(3);
    }

    public void Camera5()
    {
        UpdateCamera(4);
    }

    public Camera GetCurrCamera()
    {
        return curr_camera;
    }

    private void UpdateCamera(int index)
    {
        for (int i = 0; i < cams.Length; i++)
        {
            if (i == index)
            {
                cams[i].enabled = true;
                cams[i].GetComponent<AudioListener>().enabled = true;
                curr_camera = cams[i];
            }
            else
            {
                cams[i].enabled = false;
                cams[i].GetComponent<AudioListener>().enabled = false;
            }
        }
    }
}
