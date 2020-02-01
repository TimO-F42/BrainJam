using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public float completeDuration;
    private float completeTimer;
    private bool readyForCompletion;

    public enum ViewState
    {
        GOD,
        LAUNCH
    }

    public ViewState _viewState;

    public void LoadNextLevel()
    {
        readyForCompletion = true;
        GameManager.Instance.MoveToNextLevel();
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
