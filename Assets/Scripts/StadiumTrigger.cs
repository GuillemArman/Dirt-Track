using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StadiumTrigger : MonoBehaviour
{
    public GameObject visitor_1;
    public float duration_spawn = 1.5f;

    private CountingVisitors stadium_visitors;
    private NightCycle cycle;
    private float next_spawn;

    void Start()
    {
        next_spawn = duration_spawn;

        stadium_visitors = GameObject.Find("Stadium").GetComponent<CountingVisitors>();
        cycle = GameObject.Find("_Game Manager").GetComponent<NightCycle>();
    }

    void Update()
    {
        if (cycle.night) VisitorsGoHome();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Agent") && !cycle.night)
        {
            Destroy(other.gameObject);
            stadium_visitors.AddVisitor();
        }
    }

    void VisitorsGoHome()
    {
        next_spawn -= Time.deltaTime;

        if (next_spawn <= 0 && cycle.night && stadium_visitors.GetNumVisitors() > 0)
        {
            // Clone enemy
            GameObject objectInstance = Instantiate(visitor_1, transform.position, Quaternion.identity);
            visitor_1.GetComponent<VisitorBehaviour>().visitor_action = VisitorBehaviour.Action.Go_home;
            next_spawn = duration_spawn;
            stadium_visitors.VisitorLeft();
        }
    }
}
