using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceTrigger : MonoBehaviour
{
    private CountingVisitors stadium_visitors;

    void Start()
    {
        stadium_visitors = GameObject.Find("Stadium").GetComponent<CountingVisitors>();
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Agent"))
        {
            Destroy(other.gameObject);
            stadium_visitors.AddVisitor();
        }
    }
}
