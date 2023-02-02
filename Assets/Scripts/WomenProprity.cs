using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomenProprity : MonoBehaviour
{
    public string type;

    public PlayerAttack attack;
    public PlayerHealth health;
    public PlayerMovement movement;

    public void UpgradeProperty()
    {
        Debug.Log("ccououc");
        if(type == "orc")
        {
            attack.damage *= 2;
        }
        if(type == "elf")
        {
            movement.moveSpeed *= 1.50f;
        }
        if(type == "barbarian")
        {
            health.myHealth *= 1.75f;
            health.currentHealth = health.myHealth;
        }
    }
}
