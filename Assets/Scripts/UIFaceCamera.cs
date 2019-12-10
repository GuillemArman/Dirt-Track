using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFaceCamera : MonoBehaviour
{
    private CameraSwitching cameras;
    private Quaternion initial_rot;

    // Start is called before the first frame update
    void Start()
    {
        cameras = GameObject.Find("_Game Manager").GetComponent<CameraSwitching>();
    }

    void Update()
    {
        Vector3 look_pos = cameras.GetCurrCamera().gameObject.transform.position - transform.position;
        look_pos.y = 0;

        Quaternion rotation = Quaternion.LookRotation(look_pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
    }
}
