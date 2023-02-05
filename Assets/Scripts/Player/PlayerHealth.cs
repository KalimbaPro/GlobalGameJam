using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    public Image healthBar;
    public Animator anim;
    public GameObject player;
    public float myHealth;

    public float currentHealth;
    private float calculateLife;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("PlayerHealth");
            return;
        }

        instance = this;
    }

    void Start()
    {
        currentHealth = myHealth;
    }

    void Update()
    {
        calculateLife = currentHealth / myHealth;
        healthBar.fillAmount = Mathf.MoveTowards(healthBar.fillAmount, calculateLife, Time.deltaTime);
        if (currentHealth <= 0)
        {
            StartCoroutine(DeathTime());
        }
    }

    IEnumerator DeathTime()
    {
        yield return new WaitForSeconds(0.70f);
        anim.SetBool("isDead", false);
        SceneManager.LoadScene("familyTree");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            anim.SetBool("isDead", true);
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
        yield return new WaitForSeconds(0.70f);
        anim.SetBool("isDmg", false);
    }
    public void GetHeal(float heal)
    {
        currentHealth += heal;
    }
}
