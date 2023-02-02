using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffs : MonoBehaviour
{
    public Dictionary<int, bool> BuffList = new Dictionary<int, bool>()
    {
        { 0, false },       // plus dmg
        { 1, false },       // plus vie
        { 2, false },       // plus tank
    };
}
