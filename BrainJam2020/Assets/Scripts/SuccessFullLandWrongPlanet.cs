using UnityEngine;

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


