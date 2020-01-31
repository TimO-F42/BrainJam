

using System;
using UnityEngine;

public class Planet : BaseObject
{
    private void Start()
    {
        speed = 2.0f;
        _direction = Vector3.back;
    }

    public override void Update()
    {
        base.Update();
        
    }
}