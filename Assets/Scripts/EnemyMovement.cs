using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float runSpeed = 6f;
    [SerializeField] private float walkSpeed = 2f;
    private Vector3 targetPosition;
    public Transform player;
    private Animator animator;

    [SerializeField] GameObject[] patrolWaypoints;
    private Vector3 nextWaypoint;
    private bool isWaiting;
    int currentWaypointIndex;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, GetWaypointPosition()) < .05f)
        {
            SetNextWaypoint();
        }
        else
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
        {
            gameObject.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        } 
        else
        {
            gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        }
    }

    //IEnumerator PatrolWait()
    //{
    //    animator.SetBool("IsWaiting", true);
    //    animator.SetBool("IsPatrolling", false);
    //    isWaiting = true;
    //    yield return new WaitForSeconds(5);
    //    animator.SetBool("IsWaiting", false);
    //    animator.SetBool("IsPatrolling", true);
    //    isWaiting = false;
    //    SetNextWaypoint()
    //}
}
