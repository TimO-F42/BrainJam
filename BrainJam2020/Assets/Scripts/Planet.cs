

using System;
using UnityEngine;
using UnityEngine.Rendering;

public class Planet : BaseObject
{
    public float _rotateSpeed;
    private Quaternion _targetQuaternion;
    [SerializeField]
    [Range(1,40)]
    private float _radius;
    protected override void Start()
    {
        base.Start();
        _direction = Vector3.back;
    }

    public float GetPlanetRadius()
    {
        return _radius;
    }

    public void SetPlanetRadius(float radius)
    {
        _radius = radius;
    }
    
    

    public override void Update()
    {
        base.Update();
        
        transform.localScale = Vector3.one * _radius;
        
        
        
        //DAY/NIGHT for planets
        /*if (Tick())
        {
            Quaternion myRotation = transform.localRotation;
            _targetQuaternion = new Quaternion(myRotation.x, myRotation.y + _rotateSpeed + Time.deltaTime, myRotation.z, myRotation.w);
        }

        transform.localRotation = Quaternion.Lerp(transform.localRotation, _targetQuaternion, Time.deltaTime * _period);*/
    }
}