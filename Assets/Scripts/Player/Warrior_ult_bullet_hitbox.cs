using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Warrior_ult_bullet_hitbox : MonoBehaviour
{
    public Player player;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Boss")
        {
            Boss boss = collision.GetComponent<Boss>();
            boss.TakeDamage(player.ultSkillDamage);
            Destroy(bullet);
        }
    }
}
