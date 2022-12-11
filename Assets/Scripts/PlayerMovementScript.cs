using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private float dirX = 0f;
    private SpriteRenderer sr;
    private BoxCollider2D coll;
    public Transform AttackZone;
    public float attackRange = .62f;
    public LayerMask enemyLayers;

    [SerializeField] private int strength;
    [SerializeField] private int agility;
    [SerializeField] private int dexterity;


    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 3.5f;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask solidGround;

    private enum MovementState { idle, running, jumping, falling, attacking, comboAttacking };
    private MovementState state;

    // Start is called before the first frame update
    void Start()
    {
        strength = 5;
        agility = 5;
        dexterity = 5;

        moveSpeed = (agility * 1) + 3;
        jumpForce = (dexterity * .5f) + 2;
        damage = strength * 10;


        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseControl.gameIsPaused)
        {

            dirX = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            UpdateAnimationState();
        }
    }

    private void UpdateAnimationState()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            DoubleAttack();
        } 
        else
        {
            if (dirX > 0f)
            {
                state = MovementState.running;
                gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            else if (dirX < 0)
            {
                state = MovementState.running;
                gameObject.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            }
            else
            {
                state = MovementState.idle;
            }

            if (rb.velocity.y > .1f)
            {
                state = MovementState.jumping;
            }
            else if (rb.velocity.y < -.1f)
            {
                state = MovementState.falling;
            }
        }

        animator.SetInteger("state", (int)state);
    }

    private void Attack()
    {
        state = MovementState.attacking;
        animator.SetInteger("state", (int)state);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackZone.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    private void DoubleAttack()
    {
        state = MovementState.comboAttacking;
        animator.SetInteger("state", (int)state);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackZone.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage*2);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, solidGround);
    }

    private void OnDrawGizmosSelected()
    {
        if(AttackZone == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(AttackZone.position, attackRange);
    }
}
