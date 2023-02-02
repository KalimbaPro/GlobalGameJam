using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float CoolDown;
    public float StartCoolDown;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;
    public Animator anim;
    public AudioSource swordNoise;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if(CoolDown <= 0){
            if(Input.GetKey(KeyCode.Mouse0)){
                Collider2D[] SwordHitBox = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                anim.SetBool("isAttack", true);
                swordNoise.Play();
                for (int i = 0; i < SwordHitBox.Length; i++) {
                    SwordHitBox[i].GetComponent<Enemy>().TakeDamage(damage);
                }
                StartCoroutine(AtkTime());
            }
            CoolDown = StartCoolDown;
        } 
        else
        {
            CoolDown -= Time.deltaTime;
        }
    }

    IEnumerator AtkTime()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isAttack", false);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
