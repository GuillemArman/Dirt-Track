using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float speed = 10f;

    public float scrollSpeed = 20f;
    public float minY = 20f;
    public float maxY = 100f;

    public Vector2 limit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraMove();
    }

    void CameraMove()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("w"))
        {
            pos.z += speed * Time.deltaTime;
        }

        if (Input.GetKey("s") )
        {
            pos.z -= speed * Time.deltaTime;
        }

        if (Input.GetKey("d"))
        {
            pos.x += speed * Time.deltaTime;
        }

        if (Input.GetKey("a") )
        {
            pos.x -= speed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 30f * Time.deltaTime;

       pos.x = Mathf.Clamp(pos.x, -limit.x, limit.x);
       pos.z = Mathf.Clamp(pos.z, -limit.y, limit.y);
       pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;

    }
}
