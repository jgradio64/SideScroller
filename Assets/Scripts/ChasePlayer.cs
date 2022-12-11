using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class ChasePlayer : MonoBehaviour
{
    public Enemy EnemyScript;
    [SerializeField] private float runSpeed = 5f;
    public Transform Target;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyScript.PlayerDetected)
        {
            animator.SetBool("PlayerDetected", true);
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, runSpeed * Time.deltaTime);
        } 
        else
        {
            animator.SetBool("PlayerDetected", false);
        }
    }
}
