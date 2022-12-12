using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public int maxHealth = 100;
    CapsuleCollider2D playerCollider;

    public int currentHealth;
    public bool IsDead;
    private Animator animator;

    public HealthBar healthBar;


    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        IsDead = false;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0 && !IsDead)
        {
            Die();
        }
    }

    void Die()
    {
        IsDead = true;
        healthBar.gameObject.SetActive(false);
        animator.SetTrigger("Death");
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
    }
}
