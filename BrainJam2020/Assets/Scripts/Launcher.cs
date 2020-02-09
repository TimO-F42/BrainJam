using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Launcher : MonoBehaviour
{
    public GameObject _playerObject;
    public Transform _launchPosition;
    public float speed = 0.8f;
    
    public Game _game;
    public GameObject player;
    public Slider _playerVelocity;
    public float thrust;
    private float velocity;

    public bool _startTrail = false;

    public Vector3 targetLook;

    public Transform startSlingPos;

    public Animator slingAnimator;

    public GameObject world;

    public Transform slingDir;

    public Trail _trail;
    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
        
    }

    void SpawnPlayer()
    {
        //player = Instantiate(_playerObject, _launchPosition.position, Quaternion.identity, startSlingPos);
        //player.transform.SetParent(startSlingPos, false);
        
        transform.LookAt(GameObject.Find("TargetPlanet").transform);
        targetLook = transform.forward;
        maxAngle = transform.eulerAngles.x + 45.0f;
        minAngle = transform.eulerAngles.x - 45.0f;
        currentAngle = transform.eulerAngles.x;
    }

    public float X;
    public float Y;

    public float maxAngle;
    public float minAngle;
    public float currentAngle;

    public float aimWidthMin = -60;
    public float aimWidthMax = -10;

    public float aimHeightMin = -90;

    public float aimHeightMax = 40;
    // Update is called once per frame
    void Update()
    {
        //if (!_startTrail) _trail.enabled = false;
        //else _trail.enabled = true;
        
        if (_game._viewState == Game.ViewState.LAUNCH)
        {
            currentAngle = transform.eulerAngles.x;
            /*if (currentAngle <= maxAngle && currentAngle >= minAngle)
            {
                X = -Input.GetAxis("Vertical") * speed;
                transform.Rotate(X, Y, 0.0f);
            }
            else
            {
                X = 0;
                if (currentAngle > maxAngle) transform.eulerAngles = new Vector3(maxAngle, transform.eulerAngles.y, transform.eulerAngles.z);
                if (currentAngle < minAngle) transform.eulerAngles = new Vector3(minAngle, transform.eulerAngles.y, transform.eulerAngles.z);
            }
            
            Y = Input.GetAxis("Horizontal") * speed;*/
            
            X += -Input.GetAxis("Vertical") * speed * Time.deltaTime;
            X = Mathf.Clamp(X, aimWidthMin, aimWidthMax);
            
            Y += Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            Y = Mathf.Clamp(Y, aimHeightMin, aimHeightMax);
            
            transform.rotation = Quaternion.AngleAxis(X, Vector3.right) * Quaternion.AngleAxis(Y, Vector3.up);


        }
    }

    public void LaunchPlayer()
    {
        FindObjectOfType<Player>().transform.SetParent(world.transform);
        slingAnimator.SetTrigger("Sling");

        thrust = _playerVelocity.value;

        Rigidbody rb = FindObjectOfType<Player>()._rigidbody;
        rb.isKinematic = false;
        FindObjectOfType<Player>()._launched = true;
        
        Vector3 forwardThrust = slingDir.forward * thrust;
        
        rb.AddForce(forwardThrust.x,forwardThrust.y, forwardThrust.z, ForceMode.Impulse);
        
        FindObjectOfType<CameraHandler>().SwitchToPlayerView();
        
        _startTrail = true;
        
        //
    }
}
