using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class FindThieves : ActionTask
{
    private bool radius_created = false;
    private bool thief_found = false;
    private LineRenderer line;

    public BBParameter<GameObject> thief_to_follow;
    public BBParameter<bool> detected;
    public float alert_radius = 12.0f;
    public int segments = 50;
    public float min_dist = 2.0f;

    protected override void OnExecute()
    {
        // Creating radius UI
        if(!radius_created)
        {
            line = agent.gameObject.GetComponent<LineRenderer>();

            line.startWidth = 0.2f;
            line.positionCount = segments + 1;
            line.useWorldSpace = false;
            CreatePoints();
            radius_created = true;
        }

        // Stay alert (finds only agents)
        if (!thief_found)
        {
            int layer_id = 9;
            int layer_mask = 1 << layer_id;
            Collider[] hitColliders = Physics.OverlapSphere(agent.transform.position, alert_radius, layer_mask);

            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].gameObject != agent.gameObject && hitColliders[i].gameObject.GetComponent<Blackboard>().GetValue<bool>("stealing"))
                {
                    thief_to_follow.value = hitColliders[i].gameObject;
                    thief_found = true;
                }
            }
        }

        // Check if cop arrives to thief
        if (thief_to_follow.value != null)
        {
            Vector3 dist = thief_to_follow.value.transform.position - agent.transform.position;

            if (dist.magnitude <= min_dist)
            {
                // Do some stuff
                thief_to_follow.value.GetComponent<Blackboard>().SetValue("detected", true);
                detected.value = true;
            }
        }
        else
        {
            thief_to_follow.value = null;
            thief_found = false;
        }

        EndAction();
    }

    void CreatePoints()
    {
        float x = 0;
        float y = 0;
        float z = 0;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * alert_radius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * alert_radius;

            line.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / segments);
        }
    }
}
