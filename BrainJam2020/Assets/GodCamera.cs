using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodCamera : MonoBehaviour
{
    public Transform target;
    public float turnSpeed;
    //private Vector3 offset;
    
    public float height = 1f;
    public float distance = 2f;
    
   //private Vector3 offsetX;
   // private Vector3 offsetY;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        //offsetX = new Vector3 (0, height, distance);
        //offsetY = new Vector3 (0, 0, distance);
        
        //offset = new Vector3(target.position.x, target.position.y + 8.0f, target.position.z + 7.0f);
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    void LateUpdate()
    {
        if(Input.GetKey (KeyCode.A))
        {
            transform.RotateAround(target.position, Vector3.up, turnSpeed * Time.deltaTime);
        }
        
        if(Input.GetKey (KeyCode.D))
        {
            transform.RotateAround(target.position, -Vector3.up, turnSpeed * Time.deltaTime);
        }

        if(Input.GetKey (KeyCode.W))
        {
            transform.RotateAround(target.position, -Vector3.right, turnSpeed * Time.deltaTime);
        }
        
        if(Input.GetKey (KeyCode.S))
        {
            transform.RotateAround(target.position, Vector3.right, turnSpeed * Time.deltaTime);
        }
        
        transform.LookAt(target);
    }
}
