using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGen : MonoBehaviour
{
    public GameObject[] contentList;
    

    void Start()
    {
        Instantiate(contentList[Random.Range(0, contentList.Length)], transform);
    }
}
