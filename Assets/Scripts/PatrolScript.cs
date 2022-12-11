using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PatrolScript: MonoBehaviour
{
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private int waitTime = 5;
    [SerializeField] GameObject[] patrolWaypoints;

    private int currentWaypointIndex;
    private Vector3 nextWaypoint;
    private Animator animator;
    private bool isWaiting;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, GetWaypointPosition()) < .05f && !isWaiting)
        {
            StartCoroutine(PatrolWait());
        } 
        else if(!isWaiting)
        {
            transform.position = Vector3.MoveTowards(transform.position, GetWaypointPosition(), walkSpeed * Time.deltaTime);
            animator.SetBool("IsPatrolling", true);
        }
    }

    private Vector3 GetWaypointPosition()
    {
        var waypoint = patrolWaypoints[currentWaypointIndex].transform;
        var nextWaypointPosition = new Vector3(waypoint.position.x, transform.position.y, transform.position.z);
        FaceEnemy(waypoint.position.x);
        return nextWaypointPosition;
    }

    private void SetNextWaypoint()
    {
        currentWaypointIndex++;
        if (currentWaypointIndex >= patrolWaypoints.Length)
        {
            currentWaypointIndex = 0;
        }

    }

    private void FaceEnemy(float waypointX)
    {
        if(waypointX > transform.position.x)
            gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); 
        else
            gameObject.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
    }

    IEnumerator PatrolWait()
    {
        animator.SetBool("IsWaiting", true);
        animator.SetBool("IsPatrolling", false);
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        animator.SetBool("IsWaiting", false);
        animator.SetBool("IsPatrolling", true);
        isWaiting = false;
        SetNextWaypoint();
    }
}
