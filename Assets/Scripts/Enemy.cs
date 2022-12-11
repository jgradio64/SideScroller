using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour
{
    [SerializeField] public int maxHealth = 100;
    public int currentHealth = 100;
    private Animator animator;
    private Rigidbody2D rb;
    BoxCollider2D mobBoxCollider;




    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        mobBoxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        //if (dirX > 0f)
        //{
        //    state = MovementState.running;
        //    gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        //}
        //else if (dirX < 0)
        //{
        //    state = MovementState.running;
        //    gameObject.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        //}
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Hit();
        if(currentHealth <= 0)
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
        animator.SetBool("IsDead", true);
        GetComponent<Rigidbody2D>().gravityScale = 0.0f; 
        mobBoxCollider.enabled = false;
        this.enabled = false;
    }
}
