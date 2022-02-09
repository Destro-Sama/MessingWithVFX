using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    public LineRenderer line;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool makeConnection;

    public void MakeConnection(Vector3 startPos, Vector3 endPos)
    {
        startPosition = startPos;
        endPosition = endPos;

        makeConnection = true;
    }
    private void Update()
    {
        if (makeConnection)
        {
            line.SetPosition(0, startPosition);
            line.SetPosition(1, endPosition);
        }
    }
}
