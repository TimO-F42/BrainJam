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

    public bool isTarget;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!isTarget)
        {
            if (other.GetComponent<Player>())
            {
                other.GetComponent<Player>().ToggleRagdollDisabled(false);
            }
        }
        else
        {
            
        }
        
    }
    
    public virtual void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Goal_Prefab") return;
        if (other.GetComponent<Player>())
        {
            if (isTarget)
            {
                other.GetComponent<Player>().nearTarget = true;
                other.GetComponent<Player>().transform.position =
                    Vector3.Lerp(other.GetComponent<Player>().transform.position, transform.position, Time.deltaTime * 0.2f);
            }
            
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