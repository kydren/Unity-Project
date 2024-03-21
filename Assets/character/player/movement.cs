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
    private bool canJump = false; // Track if the player can jump
    private bool isAttacking = false; // Track if the player is currently attacking

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.freezeRotation = true; // Freeze rotation
    }

    void Update()
    {
        // Move horizontally
        float moveDirection = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

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

        // Jump
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W)) && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canJump = false;
        }

        // Attack with animation
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (moveDirection > 0)
            {
                Attack();
            }
            else if (moveDirection < 0.1f)
            {
                StopAttack();
            }
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
        animator.SetTrigger("Attack"); // Trigger attack animation
        isAttacking = true;

        // Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        // Damage enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            // Implement your damage logic here
            Debug.Log("Attacked enemy: " + enemy.name);
        }
    }

    void StopAttack()
    {
        animator.SetTrigger("StopAttack"); // Stop attack animation
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
}
