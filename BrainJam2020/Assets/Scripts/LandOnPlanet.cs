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
        if (other.GetComponent<Player>())
        {
            Debug.Log("OTHER PLAYER");
            other.GetComponent<Player>().ToggleRagdollDisabled(false);
            if (isTarget)
            {
                other.GetComponent<Player>().target = this.transform;
                other.GetComponent<Player>().nearTarget = true;
            }
        }
        
    }
    
    public virtual void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Goal_Prefab") return;
        if (other.tag == "Player") CheckLandedOnPlanet(other);
    }

    private void CheckLandedOnPlanet(Collider player)
    {
        timer += Time.deltaTime;

        float completeNorm = timer / landDuration;

        if (completeNorm >= 1.0f)
        {
            landed = true;
            //player.GetComponent<Player>()._launched = false;
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