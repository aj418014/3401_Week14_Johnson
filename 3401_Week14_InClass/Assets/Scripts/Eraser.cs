using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : MonoBehaviour
{
    //public LineRenderer line;
    //public EdgeCollider2D edge;
    public GameObject LinePrefab;
    private LineRenderer _currentLine;
    public float minimumSegmentLength = .5f;
    private Vector3 _lastMousePosition;
    private List<GameObject> _undoBuffer;
    // Start is called before the first frame update

    void Start()
    {
        _undoBuffer = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //Spawn Line Prefab
        if (Input.GetMouseButtonDown(0))
        {
            _currentLine = Instantiate(LinePrefab).GetComponent<LineRenderer>();

            _lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (_currentLine != null)
        {
            Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentMousePosition.z = 0;
            if (Vector3.Distance(_lastMousePosition, currentMousePosition) > minimumSegmentLength)
            {
                _currentLine.positionCount += 1;
                _currentLine.SetPosition(_currentLine.positionCount - 1, currentMousePosition);
                _lastMousePosition = currentMousePosition;
                EdgeCollider2D currentEdge = _currentLine.GetComponent<EdgeCollider2D>();
                Vector2[] edgePoints = new Vector2[_currentLine.positionCount];
                for (int i = 0; i < edgePoints.Length; i++)
                {
                    edgePoints[i] = _currentLine.GetPosition(i);
                }
                //Assign Collider points
                currentEdge.points = edgePoints;
                _lastMousePosition = currentMousePosition;
            }
        }


        if (Input.GetMouseButtonUp(0))
        {
            //if (_currentLine.positionCount < 2)
            Destroy(_currentLine.gameObject);
            //    else
            //    {
            //        _undoBuffer.Add(_currentLine.gameObject);
            //    }
               _currentLine = null;
            //}

            //if (Input.GetKeyDown(KeyCode.U))
            //{
            //    Destroy(_undoBuffer[_undoBuffer.Count - 1]);
            //    _undoBuffer.RemoveAt(_undoBuffer.Count - 1);
        }

        /*if (Input.GetMouseButtonDown (0))
           {
               //Get World Position of mouse cursor and make sure z = 0
               Vector3 clickpoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
               clickpoint.z = 0;
               // Add a new point ot our line
               line.positionCount += 1;
               // Set the last position (the one we just added) to our mouse
               line.SetPosition(line.positionCount - 1, clickpoint);
               //
               Vector2[] edgePoints = new Vector2[line.positionCount];
               for(int i = 0; i < edgePoints.Length; i++)
               {
                   edgePoints[i] = line.GetPosition(i);
               }

               //sets collider points
               edge.points = edgePoints;

           }*/
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }

    
    
}
