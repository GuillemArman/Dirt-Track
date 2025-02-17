﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour 
{
    private Animator animator;

    [Header("------ Read Only -------")]
    public Vector3 velocity = Vector3.zero;
    public float rotation = 0.0f;

    [Header("------ Set Values ------")]
    public float max_velocity = 3.0f;
    public float max_rotation = 2.0f;
    public float max_acceleration = 3.0f;

    public void AddVelocity(Vector3 steering_force)
    {
        velocity += steering_force;
    }

    public void SetVelocity(Vector3 new_velocity)
    {
        velocity = new_velocity;
    }

    public void AddRotation(float new_rotation)
    {
        rotation += new_rotation;
    }

    public void SetRotation(float new_rotation)
    {
        rotation = new_rotation;
    }

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update()
    {
        // Cap velocity
        if (velocity.magnitude > max_velocity)
        {
            velocity = velocity.normalized * max_velocity;
        }

        // Animation parameters
        animator.SetFloat("Velocity", velocity.magnitude);

        // Move
        velocity.y = 0.0f;
        transform.position += velocity * Time.deltaTime;

        // Rotate
        transform.rotation *= Quaternion.AngleAxis(Mathf.Clamp(rotation * Time.deltaTime, -max_rotation, max_rotation), Vector3.up);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + (velocity));
    }
}
