using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    [SerializeField] float TimerForNextAttack;
    [SerializeField] float AttackCooldown;

    private GameObject parentGO;
    private Enemy EnemyScript;
    private bool readyToAttack;
    private Animator animator;
    private Player player;
    
    void Start()
    {
        parentGO = this.transform.parent.gameObject;
        EnemyScript = parentGO.GetComponent<Enemy>();
        player = GameObject.Find("Player").GetComponent<Player>();
        animator =  this.transform.parent.gameObject.GetComponent<Animator>();
        AttackCooldown = 3;
        TimerForNextAttack = AttackCooldown;
    }

    void Update()
    {
        if (readyToAttack && !player.IsDead)
        {
            if (TimerForNextAttack > 0)
            {
                TimerForNextAttack  -= Time.deltaTime;
            }
            else if (TimerForNextAttack <= 0)
            {
                Attack();
                TimerForNextAttack = AttackCooldown;
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.CompareTag("Player")){
            readyToAttack = true;
            EnemyScript.CanAttackPlayer = true;
        }        
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Player")){
            readyToAttack = false;
            EnemyScript.CanAttackPlayer = false;
        }
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        player.TakeDamage(EnemyScript.strength * 10);
    }
}
