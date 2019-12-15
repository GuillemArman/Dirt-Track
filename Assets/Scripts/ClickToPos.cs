using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using UnityEngine.EventSystems;

public class ClickToPos : MonoBehaviour
{
    private CameraSwitching cameras;
    public Vector3 position;

    void Start()
    {
        cameras = GameObject.Find("_Game Manager").GetComponent<CameraSwitching>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GetComponent<Blackboard>().GetValue<bool>("selected"))
        {
            RaycastHit hit;

            if (Physics.Raycast(cameras.GetCurrCamera().ScreenPointToRay(Input.mousePosition), out hit, 200))
            {
                position = hit.point;
            }
        }
    }
}
