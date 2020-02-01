﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public struct PanelInfo
{
    public RectTransform _transform;
    public Planet _planet;
    public TMP_InputField _planetMass;
    public TMP_InputField _planetRadius;
    public TMP_Text _gravityForceText;
}

public class SystemInputSetup : MonoBehaviour
{
    private Planet[] _planets;

    public GameObject _planetUITemplate;

    public List<PanelInfo> _panels = new List<PanelInfo>();

    public Camera godCamera;

    public float _textOffset  ;
    
    // Start is called before the first frame update
    void Start()
    {
        _planets = FindObjectsOfType<Planet>();
        for (int i = 0; i < _planets.Length; i++)
        {
            GameObject ui = Instantiate(_planetUITemplate, Vector3.zero, Quaternion.identity);
            PanelInfo panel;
            panel._planet = _planets[i];
            panel._transform = ui.GetComponent<RectTransform>();
            panel._planetMass = GetInputField(ui.transform, "MassVal");
            panel._planetRadius = GetInputField(ui.transform, "RadiusVal");
            panel._gravityForceText = GetGravityForceText(ui.transform);
            _panels.Add(panel);
            panel._transform.SetParent(transform, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        RectTransform canvasRect = GetComponent<RectTransform>();
        foreach (PanelInfo panel in _panels)
        {
            UpdatePanelPosition(panel, canvasRect);
            UpdatePlanetRadius(panel);
            ReadPanelInput(panel);
        }
    }

    private void UpdatePanelPosition(PanelInfo panel, RectTransform canvasRect)
    {
        Vector2 ViewportPosition=godCamera.WorldToViewportPoint(panel._planet.transform.position);
        Vector2 WorldObject_ScreenPosition=new Vector2(
            ((ViewportPosition.x*canvasRect.sizeDelta.x)-(canvasRect.sizeDelta.x*_textOffset)),
            ((ViewportPosition.y*canvasRect.sizeDelta.y)-(canvasRect.sizeDelta.y*_textOffset)));
        panel._transform.anchoredPosition=WorldObject_ScreenPosition;
    }

    private void ReadPanelInput(PanelInfo panel)
    {
        double gravForce = CalculateStrengthOnSurface(panel);
        
        panel._gravityForceText.text = $"{(Math.Truncate(100 * gravForce) / 100).ToString()} m/s" ;
        //Debug.Log($"GRAVITY: {gravForce}");

    }

    private void UpdatePlanetRadius(PanelInfo panel)
    {
        float planetRadius;
        if (float.TryParse(panel._planetRadius.text, out planetRadius))
        {
            panel._planet.SetPlanetRadius(planetRadius);
        }
    }

    private float CalculateStrengthOnSurface(PanelInfo panel)
    {
        Vector3 force = Vector3.one * panel._planet.GetPlanetRadius();
        float planetMass;
        
        if (float.TryParse(panel._planetMass.text, out planetMass))
        {
            if (planetMass == 0) return 0;
        
            float gravConstVal = 30;
            float distance = force.magnitude;
            float strength = (gravConstVal * planetMass * PlayerPhysics.mass) / (distance * distance);
            force.Normalize();
            force = force * strength;
            return strength;
        }
        else
        {
            return 0;
        }
    }
    
    private Vector3 CalculatePlanetStrength(PanelInfo panel)
    {
        Vector3 force = Vector3.one * panel._planet.GetPlanetRadius();
        
        float massVal = 20;
        float gravConstVal = 30;
        float distance = force.magnitude;
        float strength = (gravConstVal * massVal * PlayerPhysics.mass) / (distance * distance);
        force.Normalize();
        force = force * strength;
        return force;
    }

    private TMP_InputField GetInputField(Transform ui, string objectName)
    {
        foreach (Transform child in ui.GetComponentsInChildren<Transform>())
        {
            if (child.GetComponent<TMP_InputField>()&& child.gameObject.name == objectName)
            {
                return child.GetComponent<TMP_InputField>();
            }
        }

        return null;
    }
    
    private TMP_Text GetGravityForceText(Transform ui)
    {
        foreach (Transform child in ui.GetComponentsInChildren<Transform>())
        {
            if (child.GetComponent<TMP_Text>() && child.gameObject.name == "GravValue")
            {
                return child.GetComponent<TMP_Text>();
            }
        }
        return null;
    }
}
