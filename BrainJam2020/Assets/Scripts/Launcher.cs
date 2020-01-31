﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject _playerObject;
    public Transform _launchPosition;
    public float speed = 5.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        GameObject player = Instantiate(_playerObject, _launchPosition.position, Quaternion.identity);
        player.transform.SetParent(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(-Input.GetAxis ("Vertical") * speed, -Input.GetAxis ("Horizontal") * speed, 0.0f);
    }
}