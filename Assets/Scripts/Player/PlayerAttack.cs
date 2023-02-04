using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float CoolDown;
    private float CoolDownValue;
    public float StartCoolDownQuick;
    public float StartCoolDownHeavy;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public float QuickAttackDamage;
    public float HeavyAttackDamage;
    private float damage;

    private bool Attack = false;

    public Animator attack;
    public AudioSource swordNoise;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        Collider2D[] SwordHitBox = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
        float waitTimeBefore = 0.0f;
        float waitTimeAfter = 0.0f;
        if (CoolDown <= 0){
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Attack = true;
                damage = QuickAttackDamage;
                attack.SetBool("isAttack", true);
                attack.SetBool("isHeavy", false);
                waitTimeBefore = 0.05f;
                waitTimeAfter = 0.2f;
                CoolDownValue = StartCoolDownQuick;

            }
            if (Input.GetKey(KeyCode.Mouse1))
            {
                Attack = true;
                damage = HeavyAttackDamage;
                attack.SetBool("isAttack", true);
                attack.SetBool("isHeavy", true);
                waitTimeBefore = 0.35f;
                waitTimeAfter = 0.2f;
                CoolDownValue = StartCoolDownHeavy; ;
            }
            if (Attack) {
                swordNoise.Play();
                StartCoroutine(AtkTime(SwordHitBox, waitTimeBefore, waitTimeAfter));
                CoolDown = CoolDownValue;
            }
        } 
        else
        {
            CoolDown -= Time.deltaTime;
        }
    }

    IEnumerator AtkTime(Collider2D[] SwordHitBox, float wiatTimeBefore, float WaitTimeAfter)
    {
        yield return new WaitForSeconds(wiatTimeBefore);
        for (int i = 0; i < SwordHitBox.Length; i++)
        {
            SwordHitBox[i].GetComponent<Enemy>().TakeDamage(damage);
        }
        yield return new WaitForSeconds(WaitTimeAfter);
        attack.SetBool("isAttack", false);
        attack.SetBool("isHeavy", false);
        Attack = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
