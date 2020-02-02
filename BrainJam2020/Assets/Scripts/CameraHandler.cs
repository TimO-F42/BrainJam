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

    public GameObject _inGameCanvas;

    public GameObject _levelCompleteCanvas;
    public GameObject _levelFailedCanvas;
    
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
        _inGameCanvas.gameObject.SetActive(false);
        _launchCamera.gameObject.SetActive(true);
        _godCamera.gameObject.SetActive(false);
        _launchCanvas.SetActive(true);
        _godCanvas.SetActive(false);
        _levelCompleteCanvas.SetActive(false);
        _game._viewState = Game.ViewState.LAUNCH;
    }

    public void SwitchToGodView()
    {
        _inGameCanvas.gameObject.SetActive(false);
        _launchCamera.gameObject.SetActive(false);
        _godCamera.gameObject.SetActive(true);
        _godCanvas.SetActive(true);
        _launchCanvas.SetActive(false);
        _levelCompleteCanvas.SetActive(false);
        _game._viewState = Game.ViewState.GOD;
    }

    public void SwitchToPlayerView()
    {
        _inGameCanvas.gameObject.SetActive(true);
        _launchCamera.gameObject.SetActive(false);
        _godCamera.gameObject.SetActive(true);
        _godCanvas.SetActive(false);
        _launchCanvas.SetActive(false);
        _levelCompleteCanvas.SetActive(false);
        _game._viewState = Game.ViewState.GOD;
        FindObjectOfType<GodCamera>().target = FindObjectOfType<Player>().transform;
        FindObjectOfType<GodCameraFollow>().offset = new Vector3(35, 88, -50);
        FindObjectOfType<GodCameraFollow>().target = FindObjectOfType<Player>().transform;
    }

    public void LevelCompleteView()
    {
        _inGameCanvas.gameObject.SetActive(false);
        _levelCompleteCanvas.SetActive(true);
        _launchCanvas.SetActive(false);
        _godCanvas.SetActive(false);
        FindObjectOfType<Game>().readyForCompletion = true;
    }

    public void LevelFailedView()
    {
        _inGameCanvas.gameObject.SetActive(false);
        _levelFailedCanvas.SetActive(true);
        _launchCanvas.SetActive(false);
        _godCanvas.SetActive(false);
    }
}
