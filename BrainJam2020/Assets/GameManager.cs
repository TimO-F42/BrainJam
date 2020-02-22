using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingleton<GameManager>
{
    public List<string> levels = new List<string>();

    public int _currentLevel = 0;

    public float _playerVelocity;
    public Vector2 _launcherRotation;

    public List<float> _panelInfoMasses = new List<float>();
    // Start is called before the first frame update
    void Start()
    {
        levels.Add("ArtScene");
        levels.Add("level1");
        levels.Add("level3");
        levels.Add("level4");
        levels.Add("level5");
        levels.Add("level2");
        SceneManager.LoadScene(levels[_currentLevel], LoadSceneMode.Additive);
    }

    public void MoveToNextLevel()
    {
        ResetPreconfigureNumbers();
        
        SceneManager.UnloadSceneAsync(levels[_currentLevel]);
        _currentLevel++;
        SceneManager.LoadScene(levels[_currentLevel], LoadSceneMode.Additive);
        
    }

    private void ResetPreconfigureNumbers()
    {
        _panelInfoMasses.Clear();
        _playerVelocity = 0;
    }

    public void RestartLevel()
    {
        SceneManager.UnloadSceneAsync(levels[_currentLevel]);
        SceneManager.LoadScene(levels[_currentLevel], LoadSceneMode.Additive);
    }

}
