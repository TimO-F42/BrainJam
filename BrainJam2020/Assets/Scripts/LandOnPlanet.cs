using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LandOnPlanet : MonoBehaviour
{
    public float landDuration = 5.0f;

    protected float timer;

    public bool landed;

    public UnityEvent _event;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            other.GetComponent<Player>()._animator.enabled = false;
            other.GetComponent<Player>().ToggleRagdollDisabled(false);
        }
    }
    
    public virtual void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            timer += Time.deltaTime;

            float completeNorm = timer / landDuration;

            if (completeNorm >= 1.0f)
            {
                landed = true;
                other.GetComponent<Player>()._launched = false;
            }
        }
    }

    public virtual void OnTriggerExit(Collider other)
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