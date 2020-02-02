using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviour
{
    public GameObject _playerObject;
    public Transform _launchPosition;
    public float speed = 0.8f;
    
    public Game _game;
    private GameObject player;
    public Slider _playerVelocity;
    public float thrust;
    private float velocity;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        player = Instantiate(_playerObject, _launchPosition.position, Quaternion.identity);
        player.transform.SetParent(this.transform);
        transform.LookAt(GameObject.Find("TargetPlanet").transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (_game._viewState == Game.ViewState.LAUNCH)
        {
            transform.Rotate(-Input.GetAxis ("Vertical") * speed, Input.GetAxis ("Horizontal") * speed, 0.0f);
        }
        
    }

    public void LaunchPlayer()
    {
        thrust = _playerVelocity.value;
        
        Rigidbody rb = FindObjectOfType<Player>()._rigidbody;
        FindObjectOfType<Player>()._launched = true;
        
        Vector3 forwardThrust = player.transform.forward * thrust;
        
        rb.AddForce(forwardThrust.x,forwardThrust.y, forwardThrust.z, ForceMode.Impulse);
        
        FindObjectOfType<CameraHandler>().SwitchToPlayerView();
    }
}
