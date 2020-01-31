

using System;
using UnityEngine;
using UnityEngine.Rendering;

public class Planet : BaseObject
{
    public float _rotateSpeed;
    private Quaternion _targetQuaternion;
    protected override void Start()
    {
        base.Start();
        _direction = Vector3.back;
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