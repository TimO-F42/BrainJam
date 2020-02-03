using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodCamera : MonoBehaviour
{
    public Transform target;
    public float turnSpeed = 300f;
    //private Vector3 offset;

    private float distance;
    private float rotationX;
    private float rotationY;

    //private Vector3 offsetX;
    // private Vector3 offsetY;



    // Start is called before the first frame update
    void Start()
    {
        //offsetX = new Vector3 (0, height, distance);
        //offsetY = new Vector3 (0, 0, distance);

        //offset = new Vector3(target.position.x, target.position.y + 8.0f, target.position.z + 7.0f);

        rotationX = transform.rotation.eulerAngles.y;
        rotationY = transform.rotation.eulerAngles.x;

        distance = (transform.parent.position - transform.position).magnitude;
        transform.LookAt(target);
    }

    // Update is called once per frame
    void Update()
    {
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

        transform.position = target.position - transform.forward * distance;
    }
}
