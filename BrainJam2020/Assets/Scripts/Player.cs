using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 _previousPosition;
    public float _maxSpeed;
    [HideInInspector]
    public float _speed;
    [HideInInspector]
    public float _normalizedSpeed;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (_speed != 0)
        {
            _normalizedSpeed = _speed / _maxSpeed;
        }
        
        Vector3 difference = _previousPosition - transform.position;
        
        _speed = difference.magnitude;

        _previousPosition = transform.position;
    }
}
