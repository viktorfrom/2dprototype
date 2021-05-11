using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero S;  // Singleton

    [Header("Set in Inspector")]
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;

    [Header("Set in Inspector")]
    public float shieldLevel = 1;

    void Awake()
    {
        if (S == null) {
            S = this; // Set the Singleton
        } else {
            Debug.Log("Does not seem like the LogError is needed");
            // Debug.LogError("Hero.Awake() - Attempted to assign second Hero.S!");
        }
    }

    void Update() {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        // Change transformation.position based on axes
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;


        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);
    }
}
