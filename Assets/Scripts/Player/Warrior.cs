using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Player
{
    void Update()
    {
        base.Update();

        isGround = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, LayerMask.GetMask("Ground"));
        if(Input.GetKeyDown(KeyCode.Space) && isGround) 
        {
            animator.SetBool("jump", !isGround);
            rigid.velocity = Vector2.zero;
            rigid.AddForce(Vector2.up * 650);
        }
    }
}
