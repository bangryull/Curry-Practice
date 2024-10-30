using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Animator animator;

    public float MaxHP = 1000f;
    public float HP;

    public UnityEvent<float> onTakeDamage;
    public UnityEvent bossDie;

    public bool isGround;
    [SerializeField] protected float GroundCheck = 0f;
    [SerializeField] protected float JumpPower = 0f;

    protected bool isFacingRight = true;

    public Transform pos;

    protected void Start()
    {
        HP = MaxHP;
    }
    protected void Update()
    {
        IsGroundReload();
        PatternStart();
    }

    protected virtual void PatternStart()
    {
    }

    public void TakeDamage(float damage)
    {
        HP -= damage;
        onTakeDamage.Invoke(damage);
        if (HP <= 0)
        {
            Die();
        }
    }

    protected void FlipX()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        isFacingRight = !isFacingRight;
    }

    private void IsGroundReload()
    {
        isGround = Physics2D.Raycast(transform.position, Vector2.down, GroundCheck, LayerMask.GetMask("Ground"));
    }

    private void Die()
    {
        bossDie.Invoke();
        Destroy(gameObject);
        throw new System.NotImplementedException();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(pos.position.x, pos.position.y));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + GroundCheck * -1));
    }
}
