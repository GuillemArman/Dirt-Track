using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceTrigger : MonoBehaviour
{
    public GameObject visitor_1;
    public float duration_spawn = 10;

    private float next_spawn;
    private NightCycle cycle;

    // Use this for initialization
    void Start()
    {
        next_spawn = duration_spawn;
        cycle = GameObject.Find("_Game Manager").GetComponent<NightCycle>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Agent") && cycle.night)
        {
            Destroy(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        next_spawn -= Time.deltaTime;

        if (next_spawn <= 0 && cycle.day)
        {
            // Clone enemy
            GameObject prefab = Instantiate(visitor_1, transform.position, Quaternion.identity);
            next_spawn = duration_spawn;
        }
    }
}
