using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundfollow : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public float followSpeed = 2f; // Speed at which the background follows the player

    void Update()
    {
        if (playerTransform != null)
        {
            // Calculate the target position for the background
            Vector3 targetPosition = new Vector3(playerTransform.position.x, transform.position.y, transform.position.z);

            // Move the background towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
