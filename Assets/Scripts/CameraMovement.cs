using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{


    public float speed = 10f;
    public float BorderThickness = 10f;

    public float scrollSpeed = 20f;
    public float minY = 20f;
    public float maxY = 100f;

    public Vector2 limit;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();
     
    }

    void CameraMovement()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - BorderThickness)
        {
            pos.z += speed * Time.deltaTime;
        }

        if (Input.GetKey("s") || Input.mousePosition.y <= BorderThickness)
        {
            pos.z -= speed * Time.deltaTime;
        }

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - BorderThickness)
        {
            pos.x += speed * Time.deltaTime;
        }

        if (Input.GetKey("a") || Input.mousePosition.x <= BorderThickness)
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

