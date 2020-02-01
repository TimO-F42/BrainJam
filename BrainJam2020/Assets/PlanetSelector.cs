using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSelector : MonoBehaviour
{
    private Ray _ray;
    public Camera _godCamera;
    private RaycastHit _hit;

    public GodCameraFollow _camFollow;
    public GodCamera _camHandler;
    // Update is called once per frame
    void Update()
    {
        _ray = _godCamera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(_ray, out _hit))
            {
                _camFollow.target = _hit.transform;
                _camHandler.target = _hit.transform;
            }
        }
    }
}
