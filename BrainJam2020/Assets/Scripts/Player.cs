using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public bool _launched;

    private float failTimer;
    private float TimeToFail = 15.0f;
    
    
    
    private void Start()
    {
        FindObjectOfType<CameraHandler>()._launchCamera = _playerCamera;
        _planets = FindObjectsOfType<Planet>();
        ToggleRagdollDisabled(true);
    }
    
    

    private void FixedUpdate()
    {
        if (_launched)
        {
            for (int i = 0; i < _planets.Length; i++)
            {
                _rigidbody.AddForce(_planets[i].PlanetForce(transform.position));
            }
        }
    }
    
    public void ToggleRagdollDisabled(bool isActive) 
    {
        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child.GetComponent<Rigidbody>())
            {
                if (child.gameObject.name == "Player(Clone)")
                {
                    child.GetComponent<Rigidbody>().isKinematic = false;
                }
                else
                {
                    child.GetComponent<Rigidbody>().isKinematic = isActive;
                }
            }
            
            if (child.GetComponent<Collider>())
            {
                if (child.gameObject.name == "Player(Clone)")
                {
                    child.GetComponent<Collider>().enabled = true;
                }
                else
                {
                    child.GetComponent<Collider>().enabled = !isActive;
                }
            }
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

        if (_launched)
        {
            failTimer += Time.deltaTime;

            float norm = failTimer / TimeToFail;

            if (norm >= 1.0f)
            {
                FindObjectOfType<CameraHandler>().LevelFailedView();
            }
        }
        else
        {
            failTimer = 0.0f;
        }
    }
}
