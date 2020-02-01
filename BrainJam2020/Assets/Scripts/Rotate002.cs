using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate002: MonoBehaviour
{

    public float speed = 20.0f;

    void Start()
    {

    }


    void Update()
    {

        transform.Rotate(Vector3.forward * speed * Time.deltaTime);

    }
}