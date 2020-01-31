using UnityEngine;

public class BaseObject : MonoBehaviour, IDilatable
{
    [Range(1,20)]
    [SerializeField]
    protected float speed = 1;
    protected float nextActionTime = 0.0f;
    protected Vector3 _direction;
    
    private Vector3 targetTransform;
    
    public float period = 0.1f;
    
    public virtual void Update()
    {
        if (Tick())
        {
            targetTransform = transform.position + _direction * speed;
        }
        
        transform.position = Vector3.Lerp(transform.position, targetTransform, Time.deltaTime * period);
    }

    public bool Tick()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime = Time.time + period;

            return true;
        }

        return false;
    }
}