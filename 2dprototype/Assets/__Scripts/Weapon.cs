using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType 
{
    none,       // Default/no weapon
    blaster,    // Simple blaster
    spread,     // Two shots simultaneously 
    phaser,     // [NI] Shots that move in waves
    missile,    // [NI] Homing missiles
    laser,      // [NI] DoT 
    shield      // Raise shieldLevel
}

[System.Serializable]
public class WeaponDefinition 
{
    public WeaponType type = WeaponType.none;
    public string letter;
    public Color color = Color.white;
    public GameObject projectilePrefab;
    public Color projectileColor = Color.white;
    public float damageOnHit = 0;
    public float continousDamage = 0;
    public float delayBetweenShots = 0;
    public float velocity = 20;
}


public class Wweapon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
