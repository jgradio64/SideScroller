using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] public int strength = 2;
    [SerializeField] private Transform AttackZone;

    private CapsuleCollider2D mobCollider;
    public HealthBar healthBar;
    private Animator animator;
    private Rigidbody2D rb;

    public bool CanAttackPlayer { get; set; }
    public bool PlayerDetected { get; set; }
    public bool IsDead;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        mobCollider = GetComponent<CapsuleCollider2D>();
        CanAttackPlayer = false;
        SetAttackZone();
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        Hit();
        if (currentHealth <= 0 && !IsDead)
        {
            Die();
        };
    }

    void Hit()
    {
        animator.SetTrigger("Hurt");
    }

    void Die()
    {
        IsDead = true;
        animator.SetBool("IsDead", true);
        GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        PlayerStats.MobKills += 1;
        healthBar.gameObject.SetActive(false);
        mobCollider.enabled = false;
        rb.simulated = false;
    }

    void SetAttackZone()
    {
        AttackZone.transform.localPosition = new Vector3(.45f, 1.0f, 0f);
    }
}
