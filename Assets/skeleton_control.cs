using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeleton_control : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;
    public float attackRange;
    public float chaseRange = 5f;
    private PlayerController player;
    public int hp;
    private bool isChasePlayer;

    private bool isAttacked, isAttacking;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        currentPoint=pointB.transform;
        anim.SetBool("isRunning", true);
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isAttacked || isAttacking) return;
        if (!isChasePlayer)
        {
            Vector2 point = currentPoint.position - transform.position;
            if(currentPoint == pointB.transform)
            {
                rb.velocity = new Vector2(speed, 0);
            }
            else
            {
                rb.velocity = new Vector2(-speed, 0);
            }
            if(Vector2.Distance(transform.position, currentPoint.position)<0.5f && currentPoint == pointB.transform)
            {
                Flip();
                currentPoint=pointA.transform;
            }
            if(Vector2.Distance(transform.position, currentPoint.position)<0.5f && currentPoint == pointA.transform)
            {
                Flip();
                currentPoint=pointB.transform;
            }
        }
        else
        {
            if (!IsAttackDirection())
            {
                Flip();
            }

            if (transform.localScale.x < 0)
            {
                rb.velocity = new Vector2(-speed, 0);
            }
            else
            {
                rb.velocity = new Vector2(speed, 0);
            }
        }
        CheckChasePlayer();
        CheckAttack();
    }

    private void Flip ()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale; 
    }
    private void OnDrawGizmos1()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

    public void TakeDam()
    {
        isAttacked = true;
        isAttacking = false;
        anim.SetBool("attack", false);
        hp--;
        if (hp > 0)
        {
            anim.SetBool("takeDam", true);
        }
        else
        {
            anim.SetTrigger("dead");
            // GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }

    public void CheckAttack()
    {
        isAttacked = false;
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < attackRange && IsAttackDirection())
        {
            anim.SetBool("attack", true);
            isAttacking = true;
        }
        else
        {
            anim.SetBool("attack", false);
            isAttacking = false;
        }
        anim.SetBool("takeDam", false);
    }

    public void DealDamToPlayer()
    {
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < attackRange)
        {
            //TODO: Deal dam to player;
            player.TakeDam();
        }
    }

    private bool IsAttackDirection()
    {
        return (transform.position.x > player.transform.position.x && transform.localScale.x < 0)
            || (transform.position.x < player.transform.position.x && transform.localScale.x > 0);
    }

    private void CheckChasePlayer()
    {
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < chaseRange)
        {
            isChasePlayer = true;
        }
    }
}
