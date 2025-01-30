using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SlimeCollider : MonoBehaviour
{
    [SerializeField]
    private Boss boss;

    [SerializeField]
    private float player_knockBack;

    // Start is called before the first frame update
    void Start()
    {
        Collider2D parentCollider = boss.GetComponent<Collider2D>();
        Collider2D myCollider = GetComponent<Collider2D>();
        if (parentCollider is CapsuleCollider2D parentCapsule && myCollider is CapsuleCollider2D myCapsule)
        {
            myCapsule.size = parentCapsule.size;
            myCapsule.offset = parentCapsule.offset;
            myCapsule.direction = parentCapsule.direction;
        }
        Physics2D.IgnoreCollision(myCollider, parentCollider);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && boss.GetComponent<Slime>().onJumping)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if(boss.transform.position.x < player.transform.position.x)
                player.TakeDamage(5f, player_knockBack, -1);
            else
                player.TakeDamage(5f, player_knockBack, 1);
        }
    }
}
