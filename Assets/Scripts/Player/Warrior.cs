using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Player
{
    [SerializeField]
    private GameObject ult_right;
    [SerializeField]
    private GameObject ult_left;

    public Transform normalAttackPos;
    public Vector2 normalAttackBoxSize;

    public Transform attackSkillPos;
    public Vector2 attackSkillBoxSize;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(normalAttackPos.position, normalAttackBoxSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(attackSkillPos.position, attackSkillBoxSize);
    }

    protected override void NormalAttack()
    {
        if (!attackable || !normalAttackable)
        {
            return;
        }

        AudioManager.instance.PlaySound(normalAttackSound); //공격 가능, 평타 가능, 사운드 클립이 존재할 때 사용 가능
        animator.SetTrigger("attack");
        attackable = false;
        normalAttackable = false;
        StartCoroutine(NormalAttackDelay());

        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(normalAttackPos.position, normalAttackBoxSize, 0);
        foreach(Collider2D col in collider2Ds)
        {
            if(col.tag == "Boss")
            {
                boss.TakeDamage(normalAttackDamage);
                ChargeUlt(normalAttackChargeUlt);
            }
        }
    }
    private IEnumerator NormalAttackDelay()
    {
        yield return new WaitForSeconds(normalAttackCooldown);
        attackable = true;
        normalAttackable = true;
    }

    protected override void AttackSkill()
    {
        if (!attackable || !attackSkillable)
        {
            return;
        }

        AudioManager.instance.PlaySound(attackSkillSound);
        StartCoroutine(AttackSkillSoundControl());
        animator.SetTrigger("attackskill");
        attackable = false;
        attackSkillable = false;
        StartCoroutine(AttackSkillDelay());
        StartCoroutine(AttackSkillCooldown());

        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(attackSkillPos.position, attackSkillBoxSize, 0);
        foreach (Collider2D col in collider2Ds)
        {
            if (col.tag == "Boss")
            {
                boss.TakeDamage(attackSkillDamage);
                ChargeUlt(attackSkillChargeUlt);
            }
        }
    }
    private IEnumerator AttackSkillDelay()
    {
        yield return new WaitForSeconds(attackSkillDelay);
        attackable = true;
    }
    private IEnumerator AttackSkillCooldown()
    {
        yield return new WaitForSeconds(attackSkillCooldown);
        attackSkillable = true;
    }
    private IEnumerator AttackSkillSoundControl()
    {
        yield return new WaitForSeconds(0.2f);
        AudioManager.instance.PlaySound(attackSkillSound);
    }

    protected override void MoveSkill()
    {
        if (!attackable || !moveSkillable)
        {
            return;
        }

        AudioManager.instance.PlaySound(moveSkillSound);
        animator.SetTrigger("dash");
        isInvincible = true;
        cantMove = true;
        attackable = false;
        moveSkillable = false;
        canDown = false;

        gravityTmp = rigid.gravityScale;
        rigid.gravityScale = 0;
        rigid.velocity = Vector2.zero;
        if(isFacingRight)
            rigid.AddForce(Vector2.right * moveSkillPower);
        else
            rigid.AddForce(Vector2.left * moveSkillPower);

        StartCoroutine(AfterMoveSkill());
        StartCoroutine(MoveSkillDelay());
    }
    private IEnumerator AfterMoveSkill()
    {
        yield return new WaitForSeconds(moveSkillDelay);

        rigid.velocity = Vector2.zero;
        rigid.gravityScale = gravityTmp;
        isInvincible = false;
        cantMove = false;
        attackable = true;
        canDown = true;
    }
    private IEnumerator MoveSkillDelay()
    {
        yield return new WaitForSeconds(moveSkillCooldown);
        moveSkillable = true;
    }


    protected override void UltSkill()
    {
        if(!attackable || ultSkillGage < 100f)
        {
            return;
        }

        AudioManager.instance.PlaySound(ultSkillSound);
        FireSlash();
        ultSkillGage = 0;
    }
    void FireSlash()
    {
        float direction = transform.localScale.x > 0 ? 1f : -1f; // 캐릭터의 방향
        Vector2 slashDirection = new Vector2(direction, 0).normalized; // X축 방향 설정

        // 발사체 생성
        GameObject slash;
        if (isFacingRight) { slash = Instantiate(ult_right, transform.position, Quaternion.Euler(0, -90, 90)); }
        else { slash = Instantiate(ult_left, transform.position, Quaternion.Euler(180, -90, 90)); }
        Warrior_ult_bullet bulletScript = slash.GetComponent<Warrior_ult_bullet>();
        if (bulletScript != null)
        {
            bulletScript.direction = slashDirection;
        }
    }
}
