using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject _playerObject;
    public Transform _launchPosition;
    public float speed = 5.0f;
    
    public Game _game;
    private GameObject player;

    public float thrust;
    
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        player = Instantiate(_playerObject, _launchPosition.position, Quaternion.identity);
        player.transform.SetParent(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (_game._viewState == Game.ViewState.LAUNCH)
        {
            transform.Rotate(-Input.GetAxis ("Vertical") * speed, -Input.GetAxis ("Horizontal") * speed, 0.0f);
        }
        
    }

    public void LaunchPlayer()
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        player.GetComponent<Player>()._launched = true;

        Vector3 forwardThrust = player.transform.forward * thrust;
        
        rb.AddForce(forwardThrust.x,forwardThrust.y, forwardThrust.z, ForceMode.Impulse);
        
        FindObjectOfType<CameraHandler>().SwitchToPlayerView();
    }
}
