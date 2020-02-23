using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingleton<GameManager>
{
    public List<string> levels = new List<string>();

    public int _currentLevel = 0;
<<<<<<< HEAD
=======

    public float _playerVelocity;
    public Vector2 _launcherRotation;

    public List<float> _panelInfoMasses = new List<float>();
    public List<GameObject> _lineAttempts = new List<GameObject>();
    
>>>>>>> parent of 8b81cfa... only last 3 line attemps now show
    // Start is called before the first frame update
    void Start()
    {
        levels.Add("ArtScene");
        levels.Add("level1");
        levels.Add("level2");
        levels.Add("level3");
        levels.Add("level4");
        levels.Add("level5");
        SceneManager.LoadScene(levels[_currentLevel], LoadSceneMode.Additive);
    }

    public void MoveToNextLevel()
    {
        SceneManager.UnloadSceneAsync(levels[_currentLevel]);
        _currentLevel++;
        SceneManager.LoadScene(levels[_currentLevel], LoadSceneMode.Additive);
    }

    public void RestartLevel()
    {
        SceneManager.UnloadSceneAsync(levels[_currentLevel]);
        SceneManager.LoadScene(levels[_currentLevel], LoadSceneMode.Additive);
    }

}
