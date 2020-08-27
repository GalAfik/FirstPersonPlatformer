using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement_CC : MonoBehaviour
{
	CharacterController controller;

	public float speed = 12f;
	public float jumpHeight = 3f;
	public float gravityModifier = 1f;

	public Transform groundCheck;
	public float groundDistance = 0.1f;
	public LayerMask groundMask;

	Vector3 velocity;
	bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
		controller = GetComponent<CharacterController>();    
    }

    // Update is called once per frame
    void Update()
    {
		HandleHorizontalMove();
		HandleVerticalMove();
	}

	void HandleHorizontalMove()
	{
		// Raycast - test if the player is on the ground
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

		// Get strafing input
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		// Use the player's local left/right and forward/back axes to move, rather than the absolute axes
		Vector3 move = transform.right * x + transform.forward * z;

		// Apply movement vector
		controller.Move(move * speed * Time.deltaTime);
	}

	void HandleVerticalMove()
	{
		// Handle gravity
		velocity.y += Physics.gravity.y * Time.deltaTime;

		// Reset vertical velocity when grounded
		if (isGrounded && velocity.y < 0) velocity.y = -2f;

		// Handle Jumping
		if (Input.GetButtonDown("Jump") && isGrounded)
		{
			velocity.y = Mathf.Sqrt(jumpHeight * -2f * (Physics.gravity.y / gravityModifier));
		}

		// Apply vertical velocity
		controller.Move(velocity * gravityModifier * Time.deltaTime);
	}
}
