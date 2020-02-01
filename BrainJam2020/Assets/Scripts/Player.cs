using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPhysics
{
    public static float mass = 10.0f;
    public static float gravConstVal = 30;
}

public class Player : MonoBehaviour
{
    private Vector3 _previousPosition;
    public float _maxSpeed;
    [HideInInspector]
    public float _speed;
    [HideInInspector]
    public float _normalizedSpeed;

    public float mass;

    public Camera _playerCamera;

    public Rigidbody _rigidbody;
    
    private Planet[] _planets;


    private void Start()
    {
        FindObjectOfType<CameraHandler>()._launchCamera = _playerCamera;
        _rigidbody = GetComponent<Rigidbody>();
        _planets = FindObjectsOfType<Planet>();
    }
    
    

    private void FixedUpdate()
    {
        for (int i = 0; i < _planets.Length; i++)
        {
            _rigidbody.AddForce(_planets[i].PlanetForce(transform.position));
        }
        
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
