using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public float completeDuration;
    private float completeTimer;
    private bool readyForCompletion;

    public bool _stopTrail = false;

    public enum ViewState
    {
        GOD,
        LAUNCH
    }

    public ViewState _viewState;

    public void LoadNextLevel()
    {
        _stopTrail = true;

        readyForCompletion = true;
        GameManager.Instance.MoveToNextLevel();
    }

    public void RestartLevel()
    {
        _stopTrail = true;

        readyForCompletion = false;
        GameManager.Instance.RestartLevel();
    }

    private void Update()
    {
        if (readyForCompletion)
        {
            completeTimer += Time.deltaTime;

            float norm = completeTimer / completeDuration;

            if (norm >= 1.0f)
            {
                LoadNextLevel();
            }
        }
    }
}
