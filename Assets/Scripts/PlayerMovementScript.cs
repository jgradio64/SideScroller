using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 3.5f;
    [SerializeField] private float slopeCheckDistance;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private LayerMask destructableLayers;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private PhysicsMaterial2D noFriction;
    [SerializeField] private PhysicsMaterial2D fullFriction;

    private Rigidbody2D rb;
    private Animator animator;
    private CapsuleCollider2D cc;
    public Transform AttackZone;

    public float attackRange = .62f;
    private float slopeDownAngle;
    private float slopeDownAngleOld;
    private float dirX = 0f;

    private bool isJumping;
    private bool canJump;
    private bool isGrounded;
    private bool isOnSlope;

    public Player player;
    private AttackScript AS;

    private Vector2 newVelocity;
    private Vector2 newForce;
    private Vector2 colliderSize;
    private Vector2 slopeNormalPerp;

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
        cc = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();

        colliderSize = cc.size;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseControl.gameIsPaused && !player.IsDead)
        {
            CheckInput();
        }
    }

    private void FixedUpdate()
    {
        IsGrounded();
        SlopeCheck();
        ApplyMovement();
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        animator.SetInteger("state", (int)state);
    }

    private void SlopeCheck()
    {
        Vector2 checkPosition = transform.position - new Vector3(0f, colliderSize.y / 2);
        SlopeCheckVertical(checkPosition);
    }

    private void SlopeCheckHorizontal(Vector2 checkPosition)
    {

    }    
    
    private void SlopeCheckVertical(Vector2 checkPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPosition, Vector2.down, slopeCheckDistance, groundLayer);

        if (hit)
        {
            slopeNormalPerp = Vector2.Perpendicular(hit.normal);
            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if(slopeDownAngle != slopeDownAngleOld)
            {
                isOnSlope = true;
            }

            slopeDownAngleOld = slopeDownAngle;
            Debug.DrawRay(hit.point, hit.normal, Color.magenta);
            Debug.DrawRay(hit.point, slopeNormalPerp, Color.green);
        }
    }

    private void CheckInput()
    {
        if (Input.GetButtonDown("Fire1")) Attack();
        else if (Input.GetButtonDown("Fire2")) DoubleAttack();
        else
        {
            dirX = Input.GetAxisRaw("Horizontal");

            // Flipping the way a character faces
            if (dirX > 0f)
            {
                gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            else if (dirX < 0)
            {
                gameObject.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            }

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
    }

    private void ApplyMovement() 
    { 
        if (isGrounded && !isOnSlope && !isJumping)
        {
            state = MovementState.running;
            newVelocity.Set(dirX * moveSpeed, 0.0f);
            rb.velocity = newVelocity;
        }
        else if (isGrounded && isOnSlope && !isJumping)
        {
            state = MovementState.running;
            newVelocity.Set(moveSpeed * slopeNormalPerp.x * -dirX, moveSpeed * slopeNormalPerp.y * -dirX);
            rb.velocity = newVelocity;
        }
        else if (!isGrounded)
        {
            newVelocity.Set(dirX * moveSpeed, rb.velocity.y);
            rb.velocity = newVelocity;
        } 

        

        if(rb.velocity.x == 0 && rb.velocity.y == 0){
            state = MovementState.idle;
        }
    }

    private void Jump()
    {
        if (canJump)
        {
            newVelocity.Set(0.0f, 0.0f);
            rb.velocity = newVelocity;
            newForce.Set(0.0f, jumpForce);
            rb.AddForce(newForce, ForceMode2D.Impulse);
            state = MovementState.jumping;
            canJump = false;
            isJumping = true;
        }
    }

    private void Attack()
    {
        state = MovementState.attacking;
        animator.SetInteger("state", (int)state);
        AS.Attack();
    }

    private void DoubleAttack()
    {
        state = MovementState.comboAttacking;
        animator.SetInteger("state", (int)state);
        AS.DoubleAttack();
    }

    private void IsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, 0.15f, groundLayer);

        if (rb.velocity.y <= 0.0f)
        {
            isJumping = false;
            state = MovementState.falling;
        }

        if (isGrounded && !isJumping)
        {
            canJump = true;
        } 
        else
        {
            canJump = false;
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
