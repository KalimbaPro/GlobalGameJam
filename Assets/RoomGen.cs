using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGen : MonoBehaviour
{
    public GameObject[] contentList;
    public GameObject bossRoom;

    void Start()
    {

    }

    public void gen(bool boss = false)
    {
        if (!boss)
            Instantiate(contentList[Random.Range(0, contentList.Length- 1)], transform);
        else
            Instantiate(bossRoom, transform);
    }
}
