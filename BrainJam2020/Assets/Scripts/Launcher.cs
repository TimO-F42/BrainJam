using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        //SetupVelocitySlider();
        SpawnPlayer();
        
    }

    private void SetupVelocitySlider()
    {
        Slider[] sliders = FindObjectsOfType<Slider>();

        for (int i = 0; i < sliders.Length; i++)
        {
            if (sliders[i].gameObject.name == "VelocitySlider")
            {
                _playerVelocity = sliders[i];
            }
        }
    }

    void SpawnPlayer()
    {
        //player = Instantiate(_playerObject, _launchPosition.position, Quaternion.identity, startSlingPos);
        //player.transform.SetParent(startSlingPos, false);
        //if (GameManager.Instance._playerVelocity != 0) _playerVelocity.value = GameManager.Instance._playerVelocity;

        if (GameManager.Instance._launcherRotation == Vector2.zero)
        {
            transform.LookAt(GameObject.Find("TargetPlanet").transform);
            targetLook = transform.forward;
            maxAngle = transform.eulerAngles.x + 45.0f;
            minAngle = transform.eulerAngles.x - 45.0f;
            currentAngle = transform.eulerAngles.x;
        }
        else
        {
            X = GameManager.Instance._launcherRotation.x;
            Y = GameManager.Instance._launcherRotation.y;
        }
        
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
        if (_game._viewState == Game.ViewState.LAUNCH)
        {
            currentAngle = transform.eulerAngles.x;
            
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

        thrust = 20.0f;//_playerVelocity.value;
        //GameManager.Instance._playerVelocity = _playerVelocity.value;
        GameManager.Instance._launcherRotation = new Vector2(X,Y); 
        

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
