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
    private bool isMoving = false;
    private BoxCollider2D coll;

    public GameObject AttackZone;
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 3.5f;
    [SerializeField] private LayerMask solidGround;

    // Start is called before the first frame update
    void Start()
    {
        AttackZone = transform.GetChild(0).gameObject;
        AttackZone.SetActive(false);
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

            UpdateAttackState();
            UpdateRunningState();
            UpdateJumpingState();
        }
    }

    private void UpdateAttackState()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            StartCoroutine(Attack());

        }
        if (Input.GetButtonDown("Fire2") && !isMoving){
            StartCoroutine(DoubleAttack());
        } 

    }

    private void UpdateJumpingState()
    {
        if(rb.velocity.y > .1f)
        {
            animator.SetBool("IsJumping", true);
        } 
        else if (IsGrounded())
        {
            animator.SetBool("IsJumping", false);
        }
    }

    private void UpdateRunningState()
    {
        if (dirX > 0f)
        {
            animator.SetBool("IsMoving", true);
            sr.flipX = false;
            isMoving = true;
        }
        else if (dirX < 0)
        {
            animator.SetBool("IsMoving", true);
            sr.flipX = true;
            isMoving = true;
        }
        else
        {
            animator.SetBool("IsMoving", false);
            isMoving = false;
        }
    }

    IEnumerator Attack()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        AttackZone.SetActive(true);
        animator.SetBool("IsAttacking", true);
        moveSpeed = 0;
        yield return new WaitForSeconds(.35f);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        animator.SetBool("IsAttacking", false);
        moveSpeed = 8f;
        AttackZone.SetActive(false);
    }

    IEnumerator DoubleAttack()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        AttackZone.SetActive(true);
        animator.SetBool("IsDoubleAttacking", true);
        moveSpeed = 0;
        yield return new WaitForSeconds(.5f);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        animator.SetBool("IsDoubleAttacking", false);
        moveSpeed = 8f;
        AttackZone.SetActive(false);

    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, solidGround);
    }
}
