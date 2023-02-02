using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int power;
    public GameObject enemy;
    public LayerMask WhatIsPlayer;
    public Transform attackPos;
    public float attackRange;
    public float TimeBetweenAttack;
    public AudioSource strikeNoise;

    private Animator anim;
    private bool attack = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            StartCoroutine(DeathTime());
        }
        if (attack == true)
        {
            anim.SetBool("isAttack", true);
            StartCoroutine(AtkTime());
            attack = false; 
            StartCoroutine(WaitforAttack());
        }
    }

    IEnumerator AtkTime()
    {
        yield return new WaitForSeconds(1f);
        Collider2D[] MonsterHitBox = Physics2D.OverlapCircleAll(attackPos.position, attackRange, WhatIsPlayer);
        for (int i = 0; i < MonsterHitBox.Length; i++)
        {
            MonsterHitBox[i].GetComponent<PlayerHealth>().TakeDamage(power);
            strikeNoise.Play();
        }
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("isAttack", false);
    }

    IEnumerator WaitforAttack()
    {
        yield return new WaitForSeconds(TimeBetweenAttack);
        attack = true;
    }
    IEnumerator DeathTime()
    {
        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(0.70f);
        Destroy(enemy);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            anim.SetBool("isDead", false);
            StartCoroutine(DeathTime());
        }
        else
        {
            anim.SetBool("isDmg", true);
            StartCoroutine(HitTime());
        }
    }
    IEnumerator HitTime()
    {
        yield return new WaitForSeconds(0.80f);
        anim.SetBool("isDmg", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
