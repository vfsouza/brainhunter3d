using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour {

	private CharacterController _controller;

	public float projectSpeed;

	void Start()
    {
		_controller = GetComponent<CharacterController>();
    }

    void Update()
    {
		Vector3 movement = transform.forward * 20; ;
		_controller.Move(movement * Time.deltaTime);
	}
}
