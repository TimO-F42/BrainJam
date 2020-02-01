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

    private Material _material;
    
    // Update is called once per frame
    void Update()
    {
        _ray = _godCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                _camFollow.target = _hit.transform;
                _camHandler.target = _hit.transform;
            }

            if (_hit.transform.GetComponent<MeshRenderer>())
            {
                if (_material != _hit.transform.GetComponent<MeshRenderer>().material)
                {
                    if (_material)
                    {
                        _material.SetInt("isHighlighted",0);
                    }
                    
                    _material = _hit.transform.GetComponent<MeshRenderer>().material;
                    _hit.transform.GetComponent<MeshRenderer>().material.SetInt("isHighlighted",1);
                }
            }
            
        }
        
        
    }
}
