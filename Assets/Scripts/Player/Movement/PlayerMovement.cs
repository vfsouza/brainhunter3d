using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private CharacterController _controller;
	private Vector3 _velocity;
	private Animator _animator;
	private int _isWalkingHash;

	[Range(1, 20)]
	public float jumpForce;
	[Range(1, 20)]
	public float movementSpeed;
	[Range(1, 10)]
	public float rotationSpeed;
	[Range(-10, -20)]
	public float gravity;

	// Start is called before the first frame update
	void Start()
    {
		_controller = GetComponent<CharacterController>();
		_animator = GetComponent<Animator>();

		_isWalkingHash = Animator.StringToHash("isWalking");
	}

    // Update is called once per frame
    void Update()
    {
		Movement();
		RotateTowardsMouseCursor();
	}

	void Animate() {
		bool isWalking = _animator.GetBool(_isWalkingHash);
		bool isMoving = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;

		if (!isWalking && isMoving) {
			_animator.SetBool("isWalking", true);
		} else if (isWalking && !isMoving) {
			_animator.SetBool("isWalking", false);
		}
	}

	void RotateTowardsMouseCursor() {
		Vector3 mouseRay = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		Vector3 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
		float angle = Mathf.Atan2(mouseRay.x - positionOnScreen.x, mouseRay.y - positionOnScreen.y) * Mathf.Rad2Deg;

		transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
	}

	void Movement() {
		Animate();

		bool ground = _controller.isGrounded;
		if (ground && _velocity.y < 0) {
			_velocity.y = 0f;
		}

		Vector3 movement = new Vector3(Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime);

		_controller.Move(movement);

		_velocity.y += gravity * Time.deltaTime;
		_controller.Move(_velocity * Time.deltaTime);
	}
}
