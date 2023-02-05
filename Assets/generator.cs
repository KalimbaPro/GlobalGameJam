using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generator : MonoBehaviour
{

    public GameObject[] spawned;
    public float spawnChance = 100;
    // Start is called before the first frame update
    void Start()
    {
        if (spawned.Length == 0)
            return;
        if (Random.Range(1, 100) <= spawnChance)
        {
            Instantiate(spawned[Random.Range(0, spawned.Length - 1)], transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
