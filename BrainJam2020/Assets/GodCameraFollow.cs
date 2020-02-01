﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodCameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    
    void LateUpdate () 
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref velocity, smoothSpeed * Time.deltaTime);
    }
}
