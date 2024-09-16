using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Player
{
    public float attackDamage = 3f;
    public float attackDelay = 3f;
    private float attackTimer = 1000f;
    public Transform pos;
    public Vector2 boxSize;

    private void Start()
    {
        base.Start();

        //jump setting
        JumpPower = 650f;
        GroundCheck = 0.5f;
    }

    void Update()
    {
        base.Update();

        //attack code start
        attackTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.J))
        {
            if(attackTimer >= attackDelay)
            {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach(Collider2D collider in collider2Ds)
                {
                    if(collider.tag == "Boss")
                    {
                        Boss.HP -= attackDamage;
                    }
                }
                animator.SetTrigger("attack");
                attackTimer = 0;
            }
        }
        //attack code end
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
}
