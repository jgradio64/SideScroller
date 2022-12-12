using Assets.Scripts;
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
    private CapsuleCollider2D coll;
    public Transform AttackZone;
    public float attackRange = .62f;

    public bool isJumping;
    public bool canJump;
    public bool isGrounded;

    public Player player;

    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 3.5f;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private LayerMask destructableLayers;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private PhysicsMaterial2D noFriction;
    [SerializeField] private PhysicsMaterial2D fullFriction;

    private Vector2 newVelocity;
    private Vector2 newForce;

    private enum MovementState { idle, running, jumping, falling, attacking, comboAttacking, dead };
    private MovementState state;

    // Start is called before the first frame update
    void Start()
    {
        PlayerAttributes.strength = 5;
        PlayerAttributes.agility = 5;
        PlayerAttributes.dexterity = 5;

        moveSpeed = (PlayerAttributes.agility * 1) + 3;
        jumpForce = (PlayerAttributes.dexterity * .5f) + 2;
        damage = PlayerAttributes.strength * 10;


        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseControl.gameIsPaused)
        {
            if (!player.IsDead)
            {

                dirX = Input.GetAxisRaw("Horizontal");
                rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

                if (Input.GetButtonDown("Jump"))
                {
                    Jump();
                }
            
                UpdateAnimationState();
            }
        }
    }

    private void FixedUpdate()
    {
        IsGrounded();
    }

    private void UpdateAnimationState()
    {
        if (Input.GetButtonDown("Fire1")) Attack();
        else if (Input.GetButtonDown("Fire2")) DoubleAttack(); 
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

            if (rb.velocity.y > .1f) state = MovementState.jumping;
            else if (rb.velocity.y < -1f) state = MovementState.falling;
        }

        animator.SetInteger("state", (int)state);
    }

    private void Jump()
    {
        if (canJump)
        {
            canJump = false;
            isJumping = true;
            newVelocity.Set(0.0f, 0.0f);
            rb.velocity = newVelocity;
            newForce.Set(0.0f, jumpForce);
            rb.AddForce(newForce, ForceMode2D.Impulse);
        }
    }

    private void Attack()
    {
        state = MovementState.attacking;
        animator.SetInteger("state", (int)state);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackZone.position, attackRange, enemyLayers);
        Collider2D[] hitDestructables = Physics2D.OverlapCircleAll(AttackZone.position, attackRange, destructableLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }
        foreach (Collider2D destr in hitDestructables)
        {
            destr.GetComponent<Destructable>().HitDesctructable();
        }
    }

    private void DoubleAttack()
    {
        state = MovementState.comboAttacking;
        animator.SetInteger("state", (int)state);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackZone.position, attackRange, enemyLayers);
        Collider2D[] hitDestructables = Physics2D.OverlapCircleAll(AttackZone.position, attackRange, destructableLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage*2);
        }
        foreach (Collider2D destr in hitDestructables)
        {
            destr.GetComponent<Destructable>().HitDesctructable();
        }
    }

    private void IsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, 0.15f, groundLayer);
        if (rb.velocity.y <= 0.0f)
        {
            isJumping = false;
        }

        if (isGrounded && !isJumping)
        {
            canJump = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(AttackZone == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(AttackZone.position, attackRange);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundCheck.position, .015f);
    }
}
