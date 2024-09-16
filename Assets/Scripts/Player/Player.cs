using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Animator animator;

    public float Speed = 4.5f;
    public float HP = 100f;

    protected bool isGround;
    protected float GroundCheck = 0f;
    protected float JumpPower = 0f;

    protected bool isFacingRight = true;
    [SerializeField]
    protected Boss Boss;

    protected void Start()
    {
        Boss = GameObject.FindWithTag("Boss").GetComponent<Boss>();
    }

    protected void Update()
    {
        //move&facing code start
        float axis = Input.GetAxisRaw("Horizontal");
        float XSpeed = axis * Speed;
        rigid.velocity = new Vector2(XSpeed, rigid.velocity.y);
        if (axis != 0)
        {
            animator.SetBool("run", true);
            if (axis > 0 && !isFacingRight)
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
                isFacingRight = !isFacingRight;
            }
            else if (axis < 0 && isFacingRight)
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
                isFacingRight = !isFacingRight;
            }
        }
        else
        {
            animator.SetBool("run", false);
        }
        //move&facing code end

        //jump code start
        isGround = Physics2D.Raycast(transform.position, Vector2.down, GroundCheck, LayerMask.GetMask("Ground"));
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            animator.SetBool("jump", !isGround);
            rigid.velocity = Vector2.zero;
            rigid.AddForce(Vector2.up * JumpPower);
        }
        //jump code end
    }

    public void Die()
    {
        if (animator.GetBool("death"))
        {
            animator.SetBool("death", true);
        }
        Destroy(gameObject);
    }
}
