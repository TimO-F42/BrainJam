

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
        base.Start();
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
        _rigidbody.mass = mass;
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
        base.Update();
        
        //DAY/NIGHT for planets
        /*if (Tick())
        {
            Quaternion myRotation = transform.localRotation;
            _targetQuaternion = new Quaternion(myRotation.x, myRotation.y + _rotateSpeed + Time.deltaTime, myRotation.z, myRotation.w);
        }

        transform.localRotation = Quaternion.Lerp(transform.localRotation, _targetQuaternion, Time.deltaTime * _period);*/
    }
}