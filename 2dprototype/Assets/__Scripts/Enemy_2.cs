using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy
{

    [Header("Set in Inspector: Enemy_2")]
    // Determines how much the Sine wave will affect movement
    public float sinEccentricity = 0.6f;
    public float lifeTime = 10;

    [Header("Set Dynamically: Enemy_2")]
    // Enemy_2 uses a Sine wave to modify a 2-point linear interpolation
    public Vector3 p0; 
    public Vector3 p1; 
    public float birthTime;

    void Start()
    {
        // Pick any point on the left side of the screen
        p0 = Vector3.zero;
        p0.x = -bndCheck.camWidth - bndCheck.radius;
        p0.y = Random.Range(-bndCheck.camHeight, bndCheck.camHeight);

        // Pick any point on the right side of the screen
        p1 = Vector3.zero;
        p1.x = bndCheck.camWidth + bndCheck.radius;
        p1.y = Random.Range(-bndCheck.camHeight, bndCheck.camHeight);

        // Possibly swap sides
        if (Random.value > 0.5f) 
        {
            // Setting the .x of point to it´s negative will move it to 
            // the other side of the screen
            p0.x *= -1;
            p1.x *= -1;

        }
        birthTime = Time.time;
    }


    public override void Move() 
    {
        // Bézier curve work based on a u value between 0 & 1
        float u = (Time.time - birthTime) / lifeTime;

        // if u > 1, then it ha been longer than lifeTime since birthTime
        if (u > 1)
        {

            //  This Enemy_2 has finished it´s life
            Destroy(this.gameObject);
            return;
        }

        // Adjust u by adding U Curve based on a Sine wave
        u = u + sinEccentricity * (Mathf.Sin(u * Mathf.PI * 2));
        
        // Interpolate the two linear interpolation points
        pos = (1 - u) * p0 + u * p1;
    }

}
