﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public CharacterController controller;
	//alle variabler i forhold til speed og lignende 
	public float speed = 4f;
	public float gravity = -9.82f;
	public float jumpHeight = 0.1f;
	public KeyCode runKey = KeyCode.LeftShift;
	public KeyCode crouchKey = KeyCode.LeftControl;
	public KeyCode tiltLeft = KeyCode.Q;
	public KeyCode tiltRight = KeyCode.E;

	public Transform groundCheck;
	public float groundDistance = 0.4f;
	public LayerMask groundMask;


	Vector3 velocity;
	bool isGrounded;
	// Update is called once per frame
	void Update()
    {
		//tjekker om playeren er groundet
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

		if (isGrounded && velocity.y < 0)
		{
			velocity.y = -2f;
	
		}


		//x og y aksen bevægelse
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		Vector3 move = transform.right * x + transform.forward * z;
		
		//jump funktionen
		if (Input.GetButtonDown("Jump") && isGrounded)
		{
			velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
		}

		controller.Move(move * speed * Time.deltaTime);
		//w key til fremad og shift key til løb, speed variablen sat op når der bliver løbt
		if (Input.GetKey(KeyCode.W) && Input.GetKeyDown(runKey))
		{
			speed = 7f;
		}
		if (Input.GetKeyUp(KeyCode.W))
		{
			speed = 4f;

		}


		//crouching key og speed ned når spilleren crocher
		if (Input.GetKeyDown(crouchKey) && isGrounded)
		{
			speed = 2f;
			controller.height = 0.5f;

		}
		if (Input.GetKeyUp(crouchKey) && isGrounded)
		{
			speed = 4f;
			controller.height = 1.6f;
		}
		//Debug.Log(speed);

		velocity.y += gravity * Time.deltaTime;

		controller.Move(velocity * Time.deltaTime);

		//skridt lyd
		if (Mathf.Abs(Input.GetAxis("Vertical")) <= 0.1)
		{
			this.GetComponent<AudioSource>().Play();
		}
	}



	void OnCollisionEnter(Collision collision)
	{
		print(collision.gameObject.tag);

		
	}
}
