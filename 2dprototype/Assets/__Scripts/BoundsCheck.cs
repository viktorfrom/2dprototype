using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float radius = 1f;

    [Header("Set in Inspector")]
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

        if (pos.x > camWidth - radius) 
        {
            pos.x = camWidth - radius;
        }

        if (pos.x < -camWidth + radius) 
        {
            pos.x = -camWidth + radius;
        }

        if (pos.y > camWidth - radius) 
        {
            pos.y = camWidth - radius;
        }

        if (pos.y < -camWidth + radius) 
        {
            pos.y = -camWidth + radius;
        }

        transform.position = pos;
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
