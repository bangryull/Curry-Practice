using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public float Speed = 4.5f;
    public bool isGround;


    protected void Update()
    {
        float axis = Input.GetAxisRaw("Horizontal");
        float XSpeed = axis * Speed;
        rigid.velocity = new Vector2(XSpeed, rigid.velocity.y);
        if (axis != 0)
        {
            animator.SetBool("run", true);
            if(axis >0) spriteRenderer.flipX = false;
            else if(axis < 0) spriteRenderer.flipX = true;
        }
        else
        {
            animator.SetBool("run", false);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
