using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Animator animator;

    public float Speed = 4.5f;
    public float MaxHP = 100f;
    public float HP;

    protected bool isGround;
    protected bool canDown;
    [SerializeField] protected float GroundCheck = 0f;
    [SerializeField] protected float JumpPower = 0f;
    [SerializeField] protected float downVelocityLimit = 15f;

    protected bool cantMove = false;

    public float KnockbackDuration = 0.4f;

    public bool isFacingRight = true;
    [SerializeField]
    protected Boss boss;

    public UnityEvent<float> onTakeDamage;
    public UnityEvent playerDie;
    public float damagedCooldown = 1f;
    private bool canDamaged = true;
    protected bool isInvincible = false;

    public float normalAttackDamage = 3f;
    public float normalAttackCooldown = 3f;
    public float normalAttackChargeUlt = 1f;

    public float attackSkillDamage = 3f;
    public float attackSkillCooldown = 3f;
    public float attackSkillDelay = 0.5f;
    public float attackSkillChargeUlt = 1f;

    public float moveSkillDamage = 0f;
    public float moveSkillCooldown = 3f;
    public float moveSkillPower = 3f;
    public float moveSkillDelay = 0.5f;
    protected float gravityTmp;
    public float moveSkillChargeUlt = 0f;

    public float ultSkillDamage = 3f;
    public float ultSkillGage = 0f;

    protected bool attackable = true;
    protected bool normalAttackable = true;
    protected bool attackSkillable = true;
    protected bool moveSkillable = true;
    protected bool ultSkillable = true;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public Color damageColor;
    public float effectDuration = 0.1f;

    public AudioClip normalAttackSound;
    public AudioClip attackSkillSound;
    public AudioClip moveSkillSound;
    public AudioClip ultSkillSound;

    protected void Start()
    {
        HP = MaxHP;
        boss = GameObject.FindWithTag("Boss").GetComponent<Boss>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        damageColor = spriteRenderer.color;
        damageColor.a = 0.1f;
    }

    protected void Update()
    {
        IsGroundReload();
        Move();

        if(rigid.velocity.y < -downVelocityLimit)
        {
            rigid.AddForce(Vector2.up * (downVelocityLimit - rigid.velocity.y));
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.S) && !isGround && canDown)
        {
            rigid.velocity = Vector2.zero;
            rigid.AddForce(Vector2.down * JumpPower / 3);
            canDown = false;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            NormalAttack();
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            AttackSkill();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            MoveSkill();
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            UltSkill();
        }
    }

    private void IsGroundReload()
    {
        isGround = Physics2D.Raycast(transform.position, Vector2.down, GroundCheck, LayerMask.GetMask("Ground"));
        if (isGround && !cantMove) 
        {
            canDown = true;
        }
    }

    public void TakeDamage(float damage, float knockback_power, int knockback_axis)
    {
        if (canDamaged && !isInvincible)
        {
            HP -= damage;
            onTakeDamage.Invoke(damage);
            canDamaged= false;
            KnockBack(knockback_power, knockback_axis);
            StartCoroutine(DamageDelayCheck());
            if (HP <= 0)
            {
                Die();
            }
            else
            {
                StartCoroutine(DamageFlash());
            }
        }
    }
    private IEnumerator DamageFlash()
    {
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(effectDuration);
        spriteRenderer.color = originalColor;
        yield return new WaitForSeconds(effectDuration);
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(effectDuration);
        spriteRenderer.color = originalColor;
    }

    public void KnockBack(float power, int axis)
    {
        cantMove = true;
        Vector2 v = Vector2.left * axis + Vector2.up * 1.5f;
        rigid.velocity = Vector2.zero;
        rigid.AddForce(v * power);
        canDown = false;
        StartCoroutine(EndKnockbackAfterDelay());
    }
    private IEnumerator EndKnockbackAfterDelay()
    {
        yield return new WaitForSeconds(KnockbackDuration);
        cantMove = false; // 넉백 상태 해제
    }

    private IEnumerator DamageDelayCheck()
    {
        yield return new WaitForSeconds(damagedCooldown);
        canDamaged = true;
    }

    public void Die()
    {
        if (animator.GetBool("death"))
        {
            animator.SetBool("death", true);
        }
        playerDie.Invoke();
        Destroy(gameObject);
    }

    private void Move()
    {
        if (cantMove)
            return;
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
    }

    protected void Jump()
    {
        if(cantMove) return;
        animator.SetBool("jump", !isGround);
        rigid.velocity = Vector2.zero;
        rigid.AddForce(Vector2.up * JumpPower);
    }

    public void ChargeUlt(float charge)
    {
        ultSkillGage += charge;
        if(ultSkillGage > 100f) { ultSkillGage = 100f; }
    }

    protected virtual void NormalAttack()
    {
    }
    protected virtual void AttackSkill()
    {
    }
    protected virtual void MoveSkill()
    {
    }
    protected virtual void UltSkill()
    {
    }
}
