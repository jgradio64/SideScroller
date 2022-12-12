using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class ChasePlayer : MonoBehaviour
{
    [SerializeField] private float chaseSpeed;
    [SerializeField] private bool isFastChaser = false;
    private Enemy EnemyScript;
    private Transform Target;
    private Animator animator;
    private Player player;


    // Start is called before the first frame update
    void Start()
    {
        EnemyScript = this.GetComponent<Enemy>();
        AcquireTarget();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyScript.PlayerDetected && !EnemyScript.CanAttackPlayer)
        {
            animator.SetBool("PlayerDetected", true);
            if (!isFastChaser)
            {
                // chase the player slowly
                SlowChase();
            } 
            else
            {
                // Chase the player fast
                FastChase();
            }
        } 
        else
        {
            animator.SetBool("PlayerDetected", false);
            animator.SetBool("FastChase", false);
            animator.SetBool("SlowChase", false);
        }
    }

    private void FastChase()
    {
        animator.SetBool("FastChase", true);
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, chaseSpeed * Time.deltaTime);
    }

    private void SlowChase()
    {
        animator.SetBool("FastChase", true);
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, chaseSpeed * Time.deltaTime);
    }

    private void AcquireTarget()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        Target = player.transform;
    }
}
