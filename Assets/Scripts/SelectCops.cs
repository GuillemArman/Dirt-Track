using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class SelectCops : MonoBehaviour
{
    public GameObject cop_1;
    public GameObject cop_2;
    public GameObject cop_3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSelected(GameObject cop_selected)
    {
        cop_1.GetComponent<Blackboard>().SetValue("selected", false);
        cop_2.GetComponent<Blackboard>().SetValue("selected", false);
        cop_3.GetComponent<Blackboard>().SetValue("selected", false);

        if (cop_selected == cop_1) cop_1.GetComponent<Blackboard>().SetValue("selected", true);
        if (cop_selected == cop_2) cop_2.GetComponent<Blackboard>().SetValue("selected", true);
        if (cop_selected == cop_3) cop_3.GetComponent<Blackboard>().SetValue("selected", true);
    }
}
