using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class InitCopBB : MonoBehaviour
{
    public Blackboard cop_bb;
    public GameObject start_pos;

    // Start is called before the first frame update
    void Start()
    {
        cop_bb = GetComponent<Blackboard>();

        // Setting triggers and targets
        cop_bb.SetValue("start_pos", start_pos);
    }
}

   