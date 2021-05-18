using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static public Main S;

    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies;
    public float enemySpawnPerSecond = 0.5f;
    public float enemyDefaultPadding = 1.5f;
    
    private BoundsCheck bndCheck;

    void Awake()
    {
        S = this;
        // Set bndCheck to reference the BoundsCheck component on this GameObject
        bndCheck = GetComponent<BoundsCheck>();
        // Invoke SpawnEnemy() once (in 2 seconds, based on default values)
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }

    void SpawnEnemy()
    {
       // Pick a random Enemy prefab to instantiate 
       int ndx = Random.Range(0, prefabEnemies.Length);
       GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);

       // Position the Enemy above the screen with a random x position
       float enemyPadding = enemyDefaultPadding;
       if (go.GetComponent<BoundsCheck>() != null)
       {
           enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
       }

       // Set the initial position for the spawned Enemy
       Vector3 pos = Vector3.zero;
       float xMin = -bndCheck.camWidth + enemyPadding;
       float yMin = bndCheck.camWidth - enemyPadding;
       pos.x = Random.Range(xMin, yMin);
       pos.y = bndCheck.camHeight + enemyPadding;
       go.transform.position = pos;

       // Invoke SpawnEnemy() again
       Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }

    public void DelayedRestart(float delay)
    {
        // Invoke Restart() method in delay seconds
        Invoke("Restart", delay);

    }

    public void Restart()
    {
        SceneManager.LoadScene("_Scene_0");
    }
}
