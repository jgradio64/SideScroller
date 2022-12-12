using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    public Enemy EnemyScript;
    public bool readyToAttack;
    Animator animator;
    public Player player;
    float TimerForNextAttack, Cooldown;

    void Start()
    {
        animator =  this.transform.parent.gameObject.GetComponent<Animator>();
        Cooldown = 3;
        TimerForNextAttack = Cooldown;
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
                TimerForNextAttack = Cooldown;
            }
        }

        if (player.IsDead)
        {
            EnemyScript.CanAttackPlayer = false;
        }

    }

    void OnTriggerEnter2D(Collider2D col)
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
        player.TakeDamage(EnemyScript.damage);
    }
}
