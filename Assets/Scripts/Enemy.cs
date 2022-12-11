using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour
{
    [SerializeField] public int maxHealth = 100;
    public int currentHealth = 100;
    private Animator animator;
    private Rigidbody2D rb;
    CapsuleCollider2D mobCollider;
    public bool PlayerDetected { get; set; }
    public bool IsDead;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        mobCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        if (PlayerDetected)
        {
            Debug.Log("PlayerDetected");
        }
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
        mobCollider.enabled = false;
        this.enabled = false;
    }
}
