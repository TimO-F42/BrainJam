using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public struct PanelInfo
{
    public RectTransform _transform;
    public Planet _planet;
    public Slider _planetMass;
    public TMP_Text _planetMassText;
    public TMP_Text _gravityForceText;

    public void Finalise()
    {
        _planet.SetPlanetMass(_planetMass.value);
    }
}

public class SystemInputSetup : MonoBehaviour
{
    private Planet[] _planets;

    public GameObject _planetUITemplate;

    public List<PanelInfo> _panels = new List<PanelInfo>();

    public Camera godCamera;

    public float _textOffset;

    public GameObject _launchButton;

    public Vector2 panelOffset;
    
    // Start is called before the first frame update
    void Start()
    {
        _planets = FindObjectsOfType<Planet>();
        for (int i = 0; i < _planets.Length; i++)
        {
            if (_planets[i]._isTargetPlanet) continue;
            
            GameObject ui = Instantiate(_planetUITemplate, Vector3.zero, Quaternion.identity);
            PanelInfo panel;
            panel._planet = _planets[i];
            panel._transform = ui.GetComponent<RectTransform>();
            panel._planetMass = GetSlider(ui.transform, "MassVal");
            panel._planetMass.minValue = panel._planet._minMass;
            panel._planetMass.maxValue = panel._planet._maxMass;
            panel._planetMassText = GetMassText(ui.transform);
            panel._planetMass.wholeNumbers = true;
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
            ReadPanelInput(panel);
            PrintMassText(panel);
        }
        
    }

    private void UpdatePanelPosition(PanelInfo panel, RectTransform canvasRect)
    {
        //Vector2 ViewportPosition=godCamera.WorldToViewportPoint(panel._planet.transform.position);
        //Vector2 WorldObject_ScreenPosition=new Vector2(
        //    ((ViewportPosition.x*canvasRect.sizeDelta.x)-(canvasRect.sizeDelta.x*_textOffset)),
        //    ((ViewportPosition.y*canvasRect.sizeDelta.y)-(canvasRect.sizeDelta.y*_textOffset)));
        //panel._transform.anchoredPosition=WorldObject_ScreenPosition;

        Vector2 ViewportPosition = godCamera.WorldToViewportPoint(panel._planet.transform.position);

        panel._transform.anchorMax = ViewportPosition + panelOffset;
        panel._transform.anchorMin = ViewportPosition + panelOffset;
    }

    private void ReadPanelInput(PanelInfo panel)
    {
        double gravForce = CalculateStrengthOnSurface(panel);
        
        panel._gravityForceText.text = $"{(Math.Truncate(100 * gravForce) / 100).ToString()}";
    }

    private void PrintMassText(PanelInfo panel)
    {
        panel._planetMassText.text = $"{panel._planetMass.value * PlayerPhysics.mass}"; // <-- I'm guessing! lol!
    }

    private float CalculateStrengthOnSurface(PanelInfo panel)
    {
        Vector3 force = Vector3.one * panel._planet.GetPlanetRadius();
        
        float distance = force.magnitude;
        float strength = (PlayerPhysics.gravConstVal * panel._planetMass.value * PlayerPhysics.mass) / (distance * distance);
        force.Normalize();
        force = force * strength;
        return strength;
    }

    public void SetFinalPlanetValues()
    {
        foreach (PanelInfo panel in _panels)
        {
            panel.Finalise();
        }
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

    private Slider GetSlider(Transform ui, string objectName)
    {
        foreach (Transform child in ui.GetComponentsInChildren<Transform>())
        {
            if (child.GetComponent<Slider>()&& child.gameObject.name == objectName)
            {
                return child.GetComponent<Slider>();
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

    private TMP_Text GetMassText(Transform ui)
    {
        foreach (TMP_Text textMeshPro in ui.GetComponentsInChildren<TMP_Text>())
        {
            if (textMeshPro.gameObject.name == "MassValText")
            {
                return textMeshPro;
            }
        }
        return null;
    }
}
