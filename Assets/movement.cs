using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of player movement
    public float jumpForce = 10f; // Force of player jump
    public LayerMask groundLayer; // LayerMask for detecting ground

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; // Freeze rotation
    }

    void Update()
    {
        // Move horizontally
        float moveDirection = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        // Check if grounded
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundLayer);

        // Jump
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W)) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Rotate the player 90 degrees to the right
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.Rotate(Vector3.forward, -90f);
        }
    }
}
