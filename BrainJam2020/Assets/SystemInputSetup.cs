using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public struct PanelInfo
{
    public RectTransform _transform;
    public Planet _planet;
}

public class SystemInputSetup : MonoBehaviour
{
    private Planet[] _planets;

    public GameObject _planetUITemplate;

    public List<PanelInfo> _panels = new List<PanelInfo>();

    public Camera godCamera;
    
    
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
            
            _panels.Add(panel);
            panel._transform.SetParent(transform, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        RectTransform CanvasRect = GetComponent<RectTransform>();
        
        foreach (PanelInfo panel in _panels)
        {
            Vector2 ViewportPosition=godCamera.WorldToViewportPoint(panel._planet.transform.position);
            Vector2 WorldObject_ScreenPosition=new Vector2(
                ((ViewportPosition.x*CanvasRect.sizeDelta.x)-(CanvasRect.sizeDelta.x*0.5f)),
                ((ViewportPosition.y*CanvasRect.sizeDelta.y)-(CanvasRect.sizeDelta.y*0.5f)));
            panel._transform.anchoredPosition=WorldObject_ScreenPosition;
        }
    }
}
