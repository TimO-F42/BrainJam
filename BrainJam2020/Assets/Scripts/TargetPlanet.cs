using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlanet : Planet
{
    
    // Start is called before the first frame update
    void Start()
    {
        _isTargetPlanet = true;
    }
    
    public override Vector3 PlanetForce(Vector3 playerPosition)
    {
        Vector3 force = transform.position - playerPosition;
        float distance = force.magnitude;
        
        float strength = (PlayerPhysics.gravConstVal * 30.0f * PlayerPhysics.mass) / (distance * distance);
        force.Normalize();
        force = force * strength;
        return force;
    }
    
    public override void Update()
    {
        
        //DAY/NIGHT for planets
        /*if (Tick())
        {
            Quaternion myRotation = transform.localRotation;
            _targetQuaternion = new Quaternion(myRotation.x, myRotation.y + _rotateSpeed + Time.deltaTime, myRotation.z, myRotation.w);
        }

        transform.localRotation = Quaternion.Lerp(transform.localRotation, _targetQuaternion, Time.deltaTime * _period);*/
    }
}
