using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Events;

public class SuccessFullLandWrongPlanet : LandOnPlanet
{
    public override void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            timer += Time.deltaTime;

            float completeNorm = timer / landDuration;

            if (completeNorm >= 1.0f)
            {
                landed = true;
            }
        }
    }

    
}


