using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3 : Enemy
{
    // Enemy_3 will move following a Bézier curve, which is a linear
    // interpolation between more than two points

    [Header("Set in Inspector: Enemy_3")]
    public float lifeTime = 5;

    [Header("Set Dynamically: Enemy_3")]
    public Vector3[] points;
    public float birthTime;

    void Start()
    {
        
        points = new Vector3[3];
        points[0] = pos;

        // set xMin and xMax the same way that Main.SpawnEnemy() does 
        float xMin = -bndCheck.camWidth + bndCheck.radius;
        float xMax = bndCheck.camWidth - bndCheck.radius;

        Vector3 v;
        // Pick a random middle position in the bottom half of the screen
        v = Vector3.zero;
        v.x = Random.Range(xMin, xMax);
        v.y = -bndCheck.camHeight * Random.Range(2.75f, 2);
        points[1] = v;

        // Pick a random final position above the top of the screen
        v = Vector3.zero;
        v.y = pos.y;
        v.x = Random.Range(xMin, xMax);
        points[2] = v;

        // Set the birthTime to the current time 
        birthTime = Time.time;
    }

    public override void Move() 
    {
        // Bézier curves work based on a u value between 0 & 1
        float u = (Time.time - birthTime) / lifeTime;

        if (u > 1)
        {
            // This Enemy_3 has finished it's life
            Destroy(this.gameObject);
            return;

        }
        // Interpolate the three Bézier curve points
        Vector3 p01, p12;
        u = u - 0.2f * Mathf.Sin(u * Mathf.PI * 2);
        p01 = (1 - u) * points[0] + u * points[1];
        p12 = (1 - u) * points[1] + u * points[2];
        pos = (1 - u) * p01 + u * p12;
    }
}
