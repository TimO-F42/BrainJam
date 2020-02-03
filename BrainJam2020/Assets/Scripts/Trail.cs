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
    GameObject myLine;
    public List<Vector3> CurrentTrail;
    public List<List<Vector3>> trailAttempts;

    public int maxPositions = 200;
    private int currentPos = 0;

    private bool _saveTrail = false;

    public float _timeBetweenPositions;
    private float _time;
    public bool forceStopDraw;
    private Vector3 previousPosition;
    
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void ClearLines()
    {
        Destroy(myLine);
        trailAttempts.Clear();
        CurrentTrail.Clear();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentTrail = new List<Vector3>();
        trailAttempts = new List<List<Vector3>>();
        myLine = new GameObject();
        _game = FindObjectOfType<Game>();
        if (!_squirrel)
            _squirrel = (Player)FindObjectOfType(typeof(Player));
        previousPosition = _squirrel.transform.position;
        LineRenderer lr = myLine.AddComponent<LineRenderer>();
        lr.material = LineMaterial;
        lr.SetWidth(0.1f, 0.1f);
        
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
                if (CurrentTrail != null) trailAttempts.Add(CurrentTrail);
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
                
                MyDraw(currentPos, previousPosition, _squirrel.transform.position);
                
                previousPosition = _squirrel.transform.position;
            }

            if(_game._stopTrail)
            {
                _game._stopTrail = false;
                _saveTrail = false;
                currentPos = 0;
            }
        }
    }
    
    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = LineMaterial;
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }

    void MyDraw(int newPositionIndex, Vector3 start, Vector3 end)
    {
        myLine.GetComponent<LineRenderer>().SetVertexCount(newPositionIndex + 1);
        myLine.GetComponent<LineRenderer>().SetPosition(newPositionIndex - 1, start);
        myLine.GetComponent<LineRenderer>().SetPosition(newPositionIndex, end);
    }
    
    
}
