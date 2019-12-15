using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class FindThieves : ActionTask
{
    private bool radius_created = false;
    private LineRenderer line;
    private int segments = 50;

    public BBParameter<bool> caught;
    public BBParameter<bool> detected;
    public BBParameter<bool> selected;

    public float alert_radius = 12.0f;
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

        // Enabling cirlce if selected
        if (selected.value)
        {
            line.enabled = true;

            int layer_id = 9;
            int layer_mask = 1 << layer_id;
            Collider[] hitColliders = Physics.OverlapSphere(agent.transform.position, alert_radius, layer_mask);

            for (int i = 0; i < hitColliders.Length; i++)
            {
                detected.value = false;

                if (hitColliders[i].gameObject != agent.gameObject && hitColliders[i].CompareTag("Visitor"))
                {
                    if (hitColliders[i].gameObject.GetComponent<Blackboard>().GetValue<bool>("stealing"))
                    {
                        detected.value = true;

                        Vector3 dist = hitColliders[i].gameObject.transform.position - agent.transform.position;

                        if (dist.magnitude <= min_dist)
                        {
                            hitColliders[i].gameObject.GetComponent<Blackboard>().SetValue("caught", true);
                            caught.value = true;
                        }
                    }

                }
            }
        }
        else
        {
            detected.value = false;
            caught.value = false;
            line.enabled = false;
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
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * alert_radius+1;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * alert_radius+1;

            line.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / segments);
        }
    }
}
