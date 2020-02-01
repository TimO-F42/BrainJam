using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Events;

public class SuccessFulLand : MonoBehaviour
{
    public float landDuration = 5.0f;

    private float timer;

    public bool landed;

    public UnityEvent _event;

    private void OnTriggerStay(Collider other)
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

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            timer = 0.0f;
        }
    }

    private void Update()
    {
        if (landed)
        {
            _event.Invoke();
        }
    }
}
