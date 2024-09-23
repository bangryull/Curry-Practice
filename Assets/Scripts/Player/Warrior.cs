using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Player
{
    public float attackDamage = 3f;
    public float attackDelay = 3f;

    private bool attackable = true;
    
    public Transform pos;
    public Vector2 boxSize;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }

    protected override void Attack()
    {
        if (!attackable)
        {
            return;
        }
        
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach(Collider2D col in collider2Ds)
        {
            if(col.tag == "Boss")
            {
                Boss.TakeDamage(attackDamage);
            }
        }
        animator.SetTrigger("attack");
        
        attackable = false;
        StartCoroutine(AttackDelay());
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);
        attackable = true;
    }
}
