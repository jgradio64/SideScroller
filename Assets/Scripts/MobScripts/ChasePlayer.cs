using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ChasePlayer : MonoBehaviour
{
    [SerializeField] private float chaseSpeed;
    [SerializeField] private bool isFastChaser = false;
    [SerializeField] private float ClosingDistance = 1.0f;

    private Enemy EnemyScript;
    private Transform Target;
    private Animator animator;
    private Player player;
    private bool closeEnough;


    // Start is called before the first frame update
    void Start()
    {
        closeEnough = false;
        EnemyScript = this.GetComponent<Enemy>();
        AcquireTarget();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        IsCloseEnough();
        if (EnemyScript.PlayerDetected && !closeEnough)
        {
            animator.SetBool("PlayerDetected", true);
            if (isFastChaser)
            {
                // Chase the player fast
                FastChase();
            } 
            else
            {                
                // chase the player slowly
                SlowChase();
            }
        } 
        else
        {
            animator.SetBool("PlayerDetected", false);
            animator.SetBool("FastChase", false);
            animator.SetBool("SlowChase", false);
        }
    }

    private void IsCloseEnough()
    {
        float enemyX = transform.position.x;
        float targetX = Target.transform.position.x;
        float remDist = Math.Abs(enemyX - targetX);
        closeEnough = remDist <= ClosingDistance;
    }

    private void FastChase()
    {
        animator.SetBool("FastChase", true);
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, chaseSpeed * Time.deltaTime);
    }

    private void SlowChase()
    {
        animator.SetBool("SlowChase", true);
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, chaseSpeed * Time.deltaTime);
    }

    private void AcquireTarget()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        Target = player.transform;
    }
}
