using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of player movement
    public float jumpForce = 10f; // Force of player jump
    public LayerMask groundLayer; // LayerMask for detecting ground
    public Transform attackPoint; // Point where the attack originates
    public float attackRange = 0.5f; // Range of the attack
    public LayerMask enemyLayer; // LayerMask for detecting enemies

    private Rigidbody2D rb;
    private Animator animator;
    private bool facingRight = true; // Keep track of the player's facing direction
    public bool canJump = false; // Track if the player can jump
    private bool isAttacking = false;
    private bool isAttacked = false;// Track if the player is currently attacking
    public int hp;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.freezeRotation = true; // Freeze rotation
    }

    void Update()
    {
        // Move horizontally
        if(isAttacked) return;
        float moveDirection = Input.GetAxis("Horizontal");
        if (!isAttacking)
        {
            rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        }

        // Flip the player's sprite if moving in the opposite direction
        if (moveDirection > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveDirection < 0 && facingRight)
        {
            Flip();
        }

        // Update the animator based on movement
        animator.SetFloat("MOVE", Mathf.Abs(moveDirection));

        if (Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            canJump = true;
        }
        // Jump
        if ((Input.GetKeyDown(KeyCode.W)) && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canJump = false;
        }

        // Attack with animation
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }

        // Rotate the player 90 degrees to the right
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.Rotate(Vector3.forward, -90f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("san"))
        {
            canJump = true;
        }
    }

    void Attack()
    {
        animator.SetBool("attack", true); // Trigger attack animation
        isAttacking = true;

        // Detect enemies in range
    }

    public void DealDamToEnemy()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        // Debug.LogError(hitEnemies.Length);
        // Damage enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            // Implement your damage logic here
            Debug.Log("Attacked enemy: " + enemy.name);
            if (enemy.CompareTag("flyingeye"))
            {
                var flyingeye = enemy.GetComponent<FlyingObjectController>();
                flyingeye.TakeDam();
            }
            else if (enemy.CompareTag("skeleton"))
            {
                var skeleton = enemy.GetComponent<skeleton_control>();
                skeleton.TakeDam();
            }
        }
    }

    void StopAttack()
    {
        animator.SetBool("attack", false); // Stop attack animation
        isAttacking = false;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        // Draw attack range gizmo
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void Flip()
    {
        // Switch the direction the player is facing
        facingRight = !facingRight;

        // Flip the player's sprite by reversing its scale
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void TakeDam()
    {
        if(hp <= 0) return;
        hp--;
        isAttacking = false;
        isAttacked = true;
        if (hp > 0)
        {
            animator.SetTrigger("takeDam");
        }
        else
        {
            animator.SetBool("dead", true);
        }
    }

    public void EndTakeDam()
    {
        if(hp <= 0) return;
        isAttacked = false;
    }
}
