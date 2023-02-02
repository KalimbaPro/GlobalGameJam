using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BartManager : MonoBehaviour
{
    public Enemy bart;
    // Start is called before the first frame update
    void Start()
    {
        int chance = UnityEngine.Random.Range(0, 2);

        if (chance == 1)
        {
            bart.health = 0;
        }
    }
}
