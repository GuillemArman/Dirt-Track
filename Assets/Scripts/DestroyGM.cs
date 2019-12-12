using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGM : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Destroy(GameObject.Find("_Game Manager"));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
