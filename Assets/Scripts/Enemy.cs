using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour
{
    [SerializeField] public int maxHealth = 100;
    [SerializeField] public float attackRange;


    public int currentHealth;
    public int strength;
    public int damage;
    private Animator animator;
    public Rigidbody2D rb;
    CapsuleCollider2D mobCollider;
    public LayerMask playerLayer;

    public bool PlayerDetected { get; set; }
    public bool CanAttackPlayer { get; set; }
    public bool IsDead;
    public Transform AttackZone;

    // Start is called before the first frame update
    void Start()
    {
        strength = 2;
        attackRange = .6f;
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        mobCollider = GetComponent<CapsuleCollider2D>();
        CanAttackPlayer = false;
        damage = strength * 10;
        SetAttackZone();
    }

    private void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Hit();
        if(currentHealth <= 0 && !IsDead)
        {
            Die();
        }
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
        mobCollider.enabled = false;
        rb.simulated = false;
    }

    void SetAttackZone()
    {
        AttackZone.transform.localPosition = new Vector3(.45f, 1.0f, 0f);
    }
}
