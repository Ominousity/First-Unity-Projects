using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public CharacterController controller;

	public float speed = 3f;
	public float runSpeed = 5f;
	public float gravity = 9.82f;
	public float jumpHeight = 0.1f;
	public KeyCode runKey = KeyCode.LeftShift;
	public KeyCode crouchKey = KeyCode.LeftControl;

	public Transform groundCheck;
	public float groundDistance = 0.4f;
	public LayerMask groundMask;

	Vector3 velocity;
	bool isGrounded;

    // Update is called once per frame
    void Update()
    {
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

		if (isGrounded && velocity.y < 0)
		{
			velocity.y = -2f;
		}

        float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		Vector3 move = transform.right * x + transform.forward * z;

		if (Input.GetKeyDown("w") && Input.GetKeyDown("runKey"))
		{
			controller.Move(move * runSpeed * Time.deltaTime);
		}
		else
		{
			controller.Move(move * speed * Time.deltaTime);
		}
		
		if (Input.GetKeyDown("crouchKey") && isGrounded)
		{
			//Here goes scaling
			speed = 2;
			runSpeed = 0;
		}

		if (Input.GetButtonDown("Jump") && isGrounded)
		{
			velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
		}

		velocity.y += gravity * Time.deltaTime;

		controller.Move(velocity * Time.deltaTime);
    }
}
