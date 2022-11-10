using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform zombie;

	[SerializeField]
	public Vector3 offset;

    void Update()
    {
        Follow();
    }

    void Follow() {
        Vector3 followPosition = offset + zombie.position;
        transform.position = followPosition;

        transform.LookAt(zombie.position);
    }
}
