using System;
using UnityEngine;

public class BaseObject : MonoBehaviour, IDilatable
{
    [SerializeField]
    protected float _speed = 1;
    protected float _nextActionTime = 0.0f;
    protected Vector3 _direction;
    
    private Vector3 _targetTransform;
    [SerializeField]
    public float _maxPeriod = 0.1f;
    public float _period = 0.1f;
    public float _lerpSpeed = 0.2f;

    protected Player _player;

    protected virtual void Start()
    {
        InitPlayer();
    }

    private void InitPlayer()
    {
        _player = FindObjectOfType<Player>();
        
    } 
    
    public virtual void Update()
    {
        //_period = _maxPeriod - _player._speed;
        _lerpSpeed = _player._normalizedSpeed;
        _period = 1 - _player._normalizedSpeed;
        
        if (Tick())
        {
            _targetTransform = transform.position + _direction * _speed;
        }
        if (_player._speed != 0) transform.position = Vector3.Lerp(transform.position, _targetTransform, Time.deltaTime * _lerpSpeed);
        else transform.position = Vector3.Lerp(transform.position, _targetTransform, Time.deltaTime);
    }

    public bool Tick()
    {
        if (Time.time > _nextActionTime)
        {
            _nextActionTime = Time.time + _period;

            return true;
        }

        return false;
    }
}