using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodCamera : MonoBehaviour
{
    public Transform target;
    public float turnSpeed = 300f;

    private float distance;
    private float rotationX;
    private float rotationY;

    public float mouseScrollSpeed = 10f;

    

    // Start is called before the first frame update
    void Start()
    {
        rotationX = transform.rotation.eulerAngles.y;
        rotationY = transform.rotation.eulerAngles.x;

        distance = (transform.parent.position - transform.position).magnitude;
        transform.LookAt(target);
    }

    void LateUpdate()
    {
        
        
        if (Input.GetMouseButton(1))
        {
            Vector2 input = new Vector2(Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime, -Input.GetAxis("Mouse Y") * turnSpeed * Time.deltaTime);

            rotationX = (rotationX + input.x) % 360f;
            rotationY = (rotationY + input.y) % 360f;

            transform.localRotation = Quaternion.Euler(rotationY, rotationX, 0f);
        }
        
        distance -= Input.GetAxisRaw("Mouse ScrollWheel") * mouseScrollSpeed;
        transform.position = target.position - transform.forward * distance;
        
        
    }
}
