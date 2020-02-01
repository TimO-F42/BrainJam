using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Game _game;
    public Camera _godCamera;
    public GameObject _godCanvas;
    
    public Camera _launchCamera;
    public GameObject _launchCanvas;
    
    void Start()
    {
        _godCamera.gameObject.SetActive(true);
        _godCanvas.SetActive(true);
        _game = FindObjectOfType<Game>();
        _launchCanvas.SetActive(false);
    }

    
    void Update()
    {
    }

    public void SwitchToLaunchView()
    {
        _launchCamera.gameObject.SetActive(true);
        _godCamera.gameObject.SetActive(false);
        _launchCanvas.SetActive(true);
        _godCanvas.SetActive(false);
        _game._viewState = Game.ViewState.LAUNCH;
    }

    public void SwitchToGodView()
    {
        _launchCamera.gameObject.SetActive(false);
        _godCamera.gameObject.SetActive(true);
        _godCanvas.SetActive(true);
        _launchCanvas.SetActive(false);
        _game._viewState = Game.ViewState.GOD;
    }
}
