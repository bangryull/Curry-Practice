using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slime : Boss
{
    protected bool patternReady = false;
    protected bool nextPattern = false;
    public float patternDelay = 3f;

    public int currentPlatform = 2;
    public float jumptest;

    [SerializeField]
    private float jumpTime;
    private bool onJumping = false;

    private void Start()
    {
        base.Start();
        StartCoroutine(PatternDelay());
    }

    private void Update()
    {
        base.Update();
        if(!isGround && !nextPattern)
        {
            onJumping = true;
        }
        else
        {
            onJumping=false;
        }
    }

    protected override void PatternStart()
    {
        if (!patternReady)
        {
            return;
        }
        else if (patternReady)
        {
            if(!nextPattern)
            {
                StartCoroutine(JumpPattern());
            }
            else
            {
                if(currentPlatform == 2)
                {
                    StartCoroutine(BulletPattern());
                }
                else
                {
                    nextPattern = false;
                }
            }
        }
    }

    private IEnumerator JumpPattern()
    {
        patternReady = false;
        for (int i = 0; i < 3; i++)
        {
            int target_platform = Random.Range(1, 4);
            if (currentPlatform == target_platform) //jump to up
            {
                Debug.Log("jump to up");
                Jumping(Vector2.up, 0f, target_platform);
                yield return new WaitForSeconds(jumpTime);
            }
            else if (currentPlatform == 1)
            {
                if (target_platform == 2) //jump 1 to 2
                {
                    Debug.Log("jump 1 to 2");
                    if (!isFacingRight)
                    {
                        FlipX();
                    }
                    Jumping(Vector2.right, 250f, target_platform);
                    yield return new WaitForSeconds(jumpTime);
                    transform.position = new Vector3(0, transform.position.y, transform.position.z);
                }
            }
            else if (currentPlatform == 2)
            {
                if (target_platform == 1) //jump 2 to 1
                {
                    Debug.Log("jump 2 to 1");
                    if(isFacingRight) 
                    {
                        FlipX();
                    }
                    Jumping(Vector2.left, 205, target_platform);
                    yield return new WaitForSeconds(jumpTime);
                }
                else if (target_platform == 3) //jump 2 to 3
                {
                    Debug.Log("jump 2 to 3");
                    if (!isFacingRight)
                    {
                        FlipX();
                    }
                    Jumping(Vector2.right, 205f, target_platform);
                    yield return new WaitForSeconds(jumpTime);
                }
            }
            else if (currentPlatform == 3)
            {
                if (target_platform == 2) //jump 3 to 2
                {
                    Debug.Log("jump 3 to 2");
                    if (isFacingRight)
                    {
                        FlipX();
                    }
                    Jumping(Vector2.left, 250f, target_platform);
                    yield return new WaitForSeconds(jumpTime);
                    transform.position = new Vector3(0, transform.position.y, transform.position.z);
                }
            }
        }
        patternReady = true;
        nextPattern = true;
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && onJumping)
        {
            Player player = col.gameObject.GetComponent<Player>();
            player.TakeDamage(5f);
        }
    }

    private void Jumping(Vector2 xDirection, float xPower, int target_platform)
    {
        onJumping = true;
        animator.SetTrigger("jump");
        rigid.velocity = Vector2.zero;
        rigid.AddForce(Vector2.up * JumpPower * rigid.mass + xDirection * rigid.mass * xPower);
        currentPlatform = target_platform;
        
    }

    private IEnumerator BulletPattern()
    {
        patternReady = false;
        nextPattern = false;
        patternReady = true;
        yield return new WaitForSeconds(0f);
    }

    private IEnumerator PatternDelay() 
    {
        yield return new WaitForSeconds(patternDelay);
        patternReady = true;
    }
}
