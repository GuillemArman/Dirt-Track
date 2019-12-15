using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class InitCopBB : MonoBehaviour
{
    private Blackboard cop_bb;
    public GameObject start_pos;
    public GameObject cop_manager;

    // Start is called before the first frame update
    void Start()
    {
        cop_bb = GetComponent<Blackboard>();
        cop_bb.SetValue("start_pos", start_pos);
        cop_bb.SetValue("start_velocity", GetComponent<Move>().max_velocity);
    }

    void OnMouseUp()
    {
        cop_manager.GetComponent<SelectCops>().ChangeSelected(gameObject);
        cop_bb.SetValue("selected", true);
    }
}

   