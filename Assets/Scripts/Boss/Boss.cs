using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
    public float MaxHP = 1000f;
    public float HP;

    public UnityEvent<float> onTakeDamage;

    protected void Start()
    {
        HP = MaxHP;
    }
    protected void Update()
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

    private void Die()
    {
        throw new System.NotImplementedException();
    }
}
