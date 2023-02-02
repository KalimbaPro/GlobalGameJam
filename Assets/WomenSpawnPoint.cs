using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomenSpawnPoint : MonoBehaviour
{
    void Awake()
    {
        dungeonGenerator.WomenSpawn.Add(transform);
    }
}
