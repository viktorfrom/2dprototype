using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero S; // Singleton

    [Header("Set in inspector")]
    // These fields control the movement of the ship
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 2f;

    [Header("Set Dynamically")]
    [SerializeField]
    private float _shieldLevel = 1;

    private GameObject lastTriggerGo = null;

    void Awake()
    {
        if (S == null) 
        {
            S = this; // Set the Singleton

        }
        else 
        {
            Debug.Log("LogError seems to prevent the program from ever starting");
            // Debug.LogError("Hero.Awake() - Attempted to assign second Hero.S!");
        }
    }

    void Update()
    {
        // Pull in information from the Input class
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");
        
        // CHange transform.position based on the axes
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        // Rotate the ship to make it feel more dynamic
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);
    }

    void OnTriggerEnter(Collider other) 
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;

        if (go == lastTriggerGo) 
        {
            return;
        }
        lastTriggerGo = go;

        if (go.tag == "Enemy") 
        {
            shieldLevel --;
            Destroy(go);
        }
        else 
        {
            print("Triggered: " + go.name);
        }
    }

    public float shieldLevel 
    {
        get
        {
            return(_shieldLevel);
        }

        set
        {
            _shieldLevel = Mathf.Min(value, 4);
            // If the shield is going to be set less than zero
            if (value < 0) 
            {
                Destroy(this.gameObject);
                // Tell Main.S to restart the game after a delay
                Main.S.DelayedRestart(gameRestartDelay);
            }
        }
    }
}
