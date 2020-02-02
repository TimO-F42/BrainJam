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

    public Animator _animator;
    public bool nearTarget;

    public float lerpSpeed;
    
    
    private void Start()
    {
        FindObjectOfType<CameraHandler>()._launchCamera = _playerCamera;
        _planets = FindObjectsOfType<Planet>();
        ToggleRagdollDisabled(true);
        _animator = GetComponent<Animator>();
    }
    
    

    private void FixedUpdate()
    {
        if (_launched && !nearTarget)
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

    public Transform target;

    public void LerpToTransform()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * lerpSpeed);
    }

    private void Update()
    {
        if (nearTarget)
        {
            _rigidbody.detectCollisions = false;
            LerpToTransform();
        }
        if (_speed != 0)
        {
            _normalizedSpeed = _speed / _maxSpeed;
        }
        
        Vector3 difference = _previousPosition - transform.position;
        
        _speed = difference.magnitude;

        _previousPosition = transform.position;

        if (_launched)
        {
            if (!nearTarget) failTimer += Time.deltaTime;
            _animator.SetTrigger("BallToIdle");
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
