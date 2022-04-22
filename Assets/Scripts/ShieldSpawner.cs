using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSpawner : MonoBehaviour
{
    public static ShieldSpawner instance;

    public GameObject shieldObject;

    public Transform[] spawnPositions;

    public bool canSpawnShield = true;
    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawnShield)
        {
            int randomPosition = Random.Range(0, spawnPositions.Length);
            Instantiate(shieldObject, new Vector3(spawnPositions[randomPosition].position.x, spawnPositions[randomPosition].position.y + 0.5f,
                spawnPositions[randomPosition].position.z), Quaternion.identity);
            canSpawnShield = false;
        }
    }
}
