using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_4 : Enemy
{
    private Vector3 p0, p1;
    private float timeStart;
    private float duration = 4;

    void Start()
    {
        p0 = p1 = pos;

        InitMovement();
    }

    void InitMovement()
    {
        p0 = p1;
        
        float widMinRad = bndCheck.camWidth - bndCheck.radius;
        float hgtMinRad = bndCheck.camHeight - bndCheck.radius;
        p1.x = Random.Range(-widMinRad, widMinRad);
        p1.y = Random.Range(-hgtMinRad, hgtMinRad);

        timeStart = Time.time;
    }

    public override void Move()
    {
        float u = (Time.time - timeStart) / duration;
        if (u >= 1)
        {
            InitMovement();
            u = 0;
        }

        u = 1 - Mathf.Pow(1 - u, 2);
        pos = (1 - u) * p0 + u * p1;
    }
}
