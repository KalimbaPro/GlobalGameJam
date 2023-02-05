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

        attack = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerAttack>();
        health = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerHealth>();
        movement = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerMovement>();
        Debug.Log("ccououc");
        if(type == "orc")
        {
            attack.QuickAttackDamage *= 1.2f;
            attack.HeavyAttackDamage *= 1.6f;
        }
        if(type == "elf")
        {
            attack.QuickAttackDamage *= 1.6f;
            attack.HeavyAttackDamage *= 0.80f;
            movement.moveSpeed *= 1.50f;
        }
        if(type == "barbarian")
        {
            attack.QuickAttackDamage *= 1.2f;
            attack.HeavyAttackDamage *= 1.2f;
            health.myHealth *= 1.75f;
            health.currentHealth = health.myHealth;
        }
    }
}
