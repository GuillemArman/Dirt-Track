using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using UnityEngine.UI;

public class ActivateWarning : MonoBehaviour
{
    private GameObject warning_text;
    private Blackboard visitor_bb;

    // Start is called before the first frame update
    void Start()
    {
        visitor_bb = GetComponent<Blackboard>();
        warning_text = GameObject.Find("Info thief");
        warning_text.GetComponent<Text>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(visitor_bb.GetValue<bool>("stealing"))
        {
            warning_text.GetComponent<Text>().enabled = true;
        }
    }

    void OnDestroy()
    {
        warning_text.GetComponent<Text>().enabled = false;
    }
}
