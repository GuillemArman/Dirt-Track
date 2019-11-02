using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketTrigger : MonoBehaviour
{
    private bool is_occupided = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        is_occupided = true;
    }

    void OnTriggerExit(Collider collider)
    {
        is_occupided = false;
    }

    public bool IsTriggerOcuppied()
    {
        return is_occupided;
    }
}
