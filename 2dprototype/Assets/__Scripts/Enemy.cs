using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Set in Inspector: Enemy")]
    public float speed = 10f;
    public float fireRate = 0.3f;
    public float health = 10;
    public int score = 100;

    private BoundsCheck bndCheck;

    void Awake() 
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    // This is a proptery: A method that acts like a field 
    public Vector3 pos 
    {
        get 
        { 
            return this.transform.position; 
        }

        set 
        { 
            this.transform.position = value;
        }
    }

    void Update()
    {
        Move();

        if (bndCheck != null && !bndCheck.isOnScreen) {

            // Check to  make sure it's gone of the bottom of the screen
            if (pos.y < bndCheck.camHeight - bndCheck.radius) 
            {
                // We're off the bottom, so destroy this GameObject
                Destroy(gameObject);
            }
        }
    }

    public virtual void Move() 
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }
}
