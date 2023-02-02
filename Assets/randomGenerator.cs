using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomGenerator : MonoBehaviour
{
    public GameObject[] objList;
    public GameObject self;
    void Start()
    {
        if (objList.Length != 0)
        {
            var obj = Instantiate(objList[Random.Range(0, objList.Length)], transform.position, Quaternion.identity);
            transform.parent = obj.transform;
        }
        Destroy(self);
    }
}
