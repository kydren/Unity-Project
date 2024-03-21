using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObjectController : MonoBehaviour
{
    public Transform pointC;
    public Transform pointD;
    public float speed;
    private Animator animator;
    private bool isMovingToPointD = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isRunning", true);
    }

    void Update()
    {
        // Determine the target position
        Vector3 targetPosition = isMovingToPointD ? pointD.position : pointC.position;

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check if the object has reached the target position
        if (transform.position == targetPosition)
        {
            // Toggle the movement direction
            isMovingToPointD = !isMovingToPointD;

            // Flip the object's facing direction
            Flip();

            // Toggle the animator bool
            animator.SetBool("isRunning", isMovingToPointD);
        }
    }

    // Flip the object's facing direction
    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
