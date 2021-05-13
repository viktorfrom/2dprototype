using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float radius = 1f;
    public bool keepOnScreen = true;

    [Header("Set in Inspector")]
    public bool isOnScreen = true;
    public float camWidth;
    public float camHeight;

    void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect; 
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        isOnScreen = true;

        if (pos.x > camWidth - radius) 
        {
            pos.x = camWidth - radius;
            isOnScreen = false;
        }

        if (pos.x < -camWidth + radius) 
        {
            pos.x = -camWidth + radius;
            isOnScreen = false;
        }

        if (pos.y > camWidth - radius) 
        {
            pos.y = camWidth - radius;
            isOnScreen = false;
        }

        if (pos.y < -camWidth + radius) 
        {
            pos.y = -camWidth + radius;
            isOnScreen = false;
        }

        if (keepOnScreen && !isOnScreen) 
        {
            transform.position = pos;
            isOnScreen = true;
        }
    }

    // Draw the bounds in the Scene pane using OnDrawGizmos()
    void OnDrawGizmos() 
    {
        if (!Application.isPlaying)
        {
            Vector3 boundSize = new Vector3(camWidth * 2, camHeight * 2, 0.1f);
            Gizmos.DrawWireCube(Vector3.zero, boundSize);
        }
    }
}
