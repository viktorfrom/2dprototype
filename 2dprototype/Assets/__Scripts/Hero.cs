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
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;
    public Weapon[] weapons;

    [Header("Set Dynamically")]
    [SerializeField]
    private float _shieldLevel = 1;

    private GameObject lastTriggerGo = null;

    // Declare a new delegate type WeaponFireDelegate
    public delegate void WeaponFireDelegate();
    // Create a WeaponFireDelegate field named fireDelegate
    public WeaponFireDelegate fireDelegate;

    void Start()
    {
        if (S == null) 
        {
            S = this; // Set the Singleton

            ClearWeapons();
            weapons[0].SetType(WeaponType.blaster);
        }
        else 
        {
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.S!");
        }

        // fireDelegate += TempFire;
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

        // Allow the ship to fire
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     TempFire();
        // }

        if (Input.GetAxis("Jump") == 1 && fireDelegate != null)
        {
            fireDelegate();
        }
    }

    // void TempFire() 
    // {
    //     GameObject projGO = Instantiate<GameObject>(projectilePrefab);
    //     projGO.transform.position = transform.position;
    //     Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
        // rigidB.velocity = Vector3.up * projectileSpeed;

    //     Projectile proj = projGO.GetComponent<Projectile>();
    //     proj.type = WeaponType.blaster;
    //     float tSpeed = Main.GetWeaponDefinition(proj.type).velocity;
    //     rigidB.velocity = Vector3.up * tSpeed;
    // }

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
        else if (go.tag == "PowerUp")
        {
            AbsorbPowerUp(go);
        }
        else 
        {
            print("Triggered: " + go.name);
        }
    }

    public void AbsorbPowerUp(GameObject go)
    {
        PowerUp pu = go.GetComponent<PowerUp>();

        switch (pu.type)
        {
            case WeaponType.shield:
                shieldLevel ++;
                break;
            
            default: 
                if (GetEmptyWeaponSlot() == null && pu.type != WeaponType.blaster)
                {
                    foreach (Weapon w in weapons)
                    {
                        if (w.type == WeaponType.blaster)
                        {
                            w.SetType(pu.type);
                            break;
                        }
                    }
                }
                else
                {
                    Weapon w = GetEmptyWeaponSlot();
                    if (w != null)
                    {
                        w.SetType(pu.type);
                    }
                }
                break;
        }

        pu.AbsorbedBy(this.gameObject);
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

    Weapon GetEmptyWeaponSlot()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].type == WeaponType.none)
            {
                return(weapons[i]);
            }
        }
        return(null);
    }

    void ClearWeapons()
    {
        foreach (Weapon w in weapons)
        {
            w.SetType(WeaponType.none);
        }
    }
}
