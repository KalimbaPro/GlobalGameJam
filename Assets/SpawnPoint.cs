using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Transform player;
    void Start()
    {
        player.position = transform.position;
    }
}
