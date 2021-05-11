using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float cameraDistance = 30.0f;
    public Vector3 offset;

    void Awake()
    {
        GetComponent<UnityEngine.Camera>().orthographicSize = ((Screen.height / 2) / cameraDistance);
    }


    void FixedUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, target.position.z) + offset;

    }
}
