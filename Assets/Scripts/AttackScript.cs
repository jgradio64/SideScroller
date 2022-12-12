using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private LayerMask destructableLayers;
    [SerializeField] private Transform AttackZone;
    [SerializeField] private float reach;

    private int damage;
    

    // Start is called before the first frame update
    void Start()
    {
        damage = PlayerAttributes.strength * 10;
        reach = .62f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackZone.position, reach, enemyLayers);
        Collider2D[] hitDestructables = Physics2D.OverlapCircleAll(AttackZone.position, reach, destructableLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }
        foreach (Collider2D destr in hitDestructables)
        {
            destr.GetComponent<Destructable>().HitDesctructable();
        }
    }

    public void DoubleAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackZone.position, reach, enemyLayers);
        Collider2D[] hitDestructables = Physics2D.OverlapCircleAll(AttackZone.position, reach, destructableLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage * 2);
        }
        foreach (Collider2D destr in hitDestructables)
        {
            destr.GetComponent<Destructable>().HitDesctructable();
        }
    }
}
