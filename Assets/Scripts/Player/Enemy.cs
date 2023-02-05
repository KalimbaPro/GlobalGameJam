using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public int power;
    public GameObject enemy;
    public LayerMask WhatIsPlayer;

    public Transform attackPos;
    public Transform spellLeftPos;
    public Transform spellRightPos;
    public GameObject spellLeft;
    public GameObject SpellRight;


    public bool isBoss;
    public float seeRange;
    public float attackRange;
    public float TimeBetweenAttack;
    public float speed;
    public AudioSource strikeNoise;
    public AudioSource deathNoise;
    public AudioSource hitNoise;

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
        Collider2D[] playersInRange = Physics2D.OverlapCircleAll(attackPos.position, seeRange, WhatIsPlayer);
        if (playersInRange.Length > 0) {
            if (playersInRange[0].transform.position.x > transform.position.x) {
                transform.GetComponent<SpriteRenderer>().flipX = false;
            } else {
                transform.GetComponent<SpriteRenderer>().flipX = true;
            }
            transform.position = Vector2.MoveTowards(transform.position, playersInRange[0].transform.position, speed * Time.deltaTime);
        }
        if (health <= 0)
        {
            StartCoroutine(DeathTime());
        }
        Collider2D[] playerInAttackRange = Physics2D.OverlapCircleAll(attackPos.position, attackRange, WhatIsPlayer);
        if (playerInAttackRange.Length > 0) {
            if (attack == true) {
                anim.SetBool("isAttack", true);
                StartCoroutine(AtkTime());
                attack = false; 
                StartCoroutine(WaitforAttack());
            }
        }
        if (isBoss)
        {
            Collider2D[] playerInSpellRangeLeft = Physics2D.OverlapCircleAll(spellLeftPos.position, 0.5f, WhatIsPlayer);
            Collider2D[] playerInSpellRangeRight = Physics2D.OverlapCircleAll(spellRightPos.position, 0.5f, WhatIsPlayer);
            if (playerInSpellRangeLeft.Length > 0 || playerInSpellRangeRight.Length > 0)
            {
                if (attack == true)
                {
                    anim.SetBool("isSpell", true);
                    spellLeft.SetActive(true);
                    SpellRight.SetActive(true);
                    StartCoroutine(SpellTime());
                    attack = false;
                    StartCoroutine(WaitforAttack());
                }
            }
        }
        Physics2D.IgnoreLayerCollision(8, 9);
    }

    IEnumerator SpellTime()
    {
        yield return new WaitForSeconds(1f);
        Collider2D[] MonsterHitBox1 = Physics2D.OverlapCircleAll(spellLeftPos.position, 0.5f, WhatIsPlayer);
        Collider2D[] MonsterHitBox2 = Physics2D.OverlapCircleAll(spellRightPos.position, 0.5f, WhatIsPlayer);
        for (int i = 0; i < MonsterHitBox1.Length; i++)
        {
            MonsterHitBox1[i].GetComponent<PlayerHealth>().TakeDamage(power);
        }
        for (int i = 0; i < MonsterHitBox2.Length; i++)
        {
            MonsterHitBox2[i].GetComponent<PlayerHealth>().TakeDamage(power);
        }
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("isSpell", false);
        spellLeft.SetActive(false);
        SpellRight.SetActive(false);
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
        deathNoise.Play();
        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(0.70f);
        Destroy(enemy);
    }
    public void TakeDamage(float damage)
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
        hitNoise.Play();
        yield return new WaitForSeconds(0.80f);

        anim.SetBool("isDmg", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
