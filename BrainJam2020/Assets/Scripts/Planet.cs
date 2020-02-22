

using System;
using UnityEngine;
using UnityEngine.Rendering;

public class Planet : BaseObject
{
    public float _rotateSpeed;
    private Quaternion _targetQuaternion;
    [SerializeField]
    public float _radius;
    private float _mass;
    private Rigidbody _rigidbody;
    public float _minMass;
    public float _maxMass;
    public bool _isTargetPlanet;
    
    
    protected override void Start()
    {
        //base.Start();
        _player = FindObjectOfType<Player>();
        _direction = Vector3.back;
        _rigidbody = GetComponent<Rigidbody>();
    }

    public float GetPlanetRadius()
    {
        return _radius;
    }
    
    

    public virtual void SetPlanetRadius(float radius)
    {
        _radius = radius;
    }

    public virtual void SetPlanetMass(float mass)
    {
        _mass = mass;
        //_rigidbody.mass = mass;
    }
    public virtual Vector3 PlanetForce(Vector3 playerPosition)
    {
        Vector3 force = transform.position - playerPosition;
        float distance = force.magnitude;
        
        float strength = (PlayerPhysics.gravConstVal * _mass * PlayerPhysics.mass) / (distance * distance);
        force.Normalize();
        force = force * strength;
        return force;
    }
    
    

    public override void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * _rotateSpeed);
        if (!FindObjectOfType<Player>()) return;
        if (!_player)_player = FindObjectOfType<Player>();
        //TODO FIX: RotatePlanet();
        
    }

    protected Vector3 currentRotation;
    protected Vector3 targetRotation;
    
    public virtual void RotatePlanet()
    {
        if (Tick())
        {
            currentRotation = transform.eulerAngles;
            targetRotation = new Vector3(currentRotation.x, currentRotation.y + _rotateSpeed + Time.deltaTime, currentRotation.z);
        }
        
        if (_player._speed != 0) transform.eulerAngles = Vector3.Lerp(currentRotation, targetRotation, Time.deltaTime * _lerpSpeed);
        else transform.eulerAngles = Vector3.Lerp(currentRotation, targetRotation, Time.deltaTime);
    }
}