using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehavior : MonoBehaviour
{
    public GameObject[] walls;

    public void UpdateRoom(bool[] status, bool boss)
    {
        for (int i = 0; i < status.Length; ++i)
        {
            walls[i].SetActive(!status[i]);
        }
    }
}
