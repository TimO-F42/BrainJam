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

    private Game _game;

    void Start()
    {
        _game = FindObjectOfType<Game>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_game._viewState == Game.ViewState.GOD)
        {
            _ray = _godCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _camFollow.target = _hit.transform;
                    _camHandler.target = _hit.transform;
                }

                if (_hit.transform.GetComponentInChildren<MeshRenderer>())
                {
                    if (_material != _hit.transform.GetComponentInChildren<MeshRenderer>().material)
                    {
                        if (_material)
                        {
                            _material.SetInt("isHighlighted", 0);
                        }

                        _material = _hit.transform.GetComponentInChildren<MeshRenderer>().material;
                        _hit.transform.GetComponentInChildren<MeshRenderer>().material.SetInt("isHighlighted", 1);
                    }
                }

            }
            else if (_material)
            {
                _material.SetInt("isHighlighted", 0);
                _material = null;
            }
        }
    }
}
