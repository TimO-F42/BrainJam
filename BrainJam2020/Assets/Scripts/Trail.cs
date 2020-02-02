using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    public Launcher _launcher;
    private Player _squirrel;
    private Game _game;
    public Material LineMaterial;
    public List<Vector3> CurrentTrail;

    public int maxPositions = 200;
    private int currentPos = 0;

    private bool _saveTrail = false;

    public float _timeBetweenPositions;
    private float _time;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentTrail = new List<Vector3>();
        _game = FindObjectOfType<Game>();
    }

    // Update is called once per frame
    void Update()
    {
        //Save less positions in RAM
        _time += Time.deltaTime;
        if(_time >= _timeBetweenPositions)
        {
            _time = 0.0f;

            if (_launcher._startTrail)
            {
                CurrentTrail.Clear();
                _launcher._startTrail = false;
                _saveTrail = true;
            }

            if (_saveTrail && currentPos < maxPositions)
            {
                if (!_squirrel)
                    _squirrel = (Player)FindObjectOfType(typeof(Player));
                CurrentTrail.Add(_squirrel.transform.position);
                currentPos++;
            }

            if(_game._stopTrail)
            {
                _game._stopTrail = false;
                _saveTrail = false;
                currentPos = 0;
            }
        }

        DrawTrail();
    }

    void DrawTrail()
    {
        for (int i = 0; i < CurrentTrail.Count - 1; i++)
        {
            //Debug.DrawLine(CurrentTrail[i], CurrentTrail[i + 1], new Color(1.0f, 0.0f, 1.0f));
            DrawLine(CurrentTrail[i], CurrentTrail[i + 1], new Color(1.0f, 0.0f, 1.0f));
        }
    }
    
    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = LineMaterial;
        lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }
    
    
}
