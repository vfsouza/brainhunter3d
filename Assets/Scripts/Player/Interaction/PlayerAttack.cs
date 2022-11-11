using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool allowFire = true;
    private float accuracy;
    private float fireSpeed = 0.5f;

    public GameObject projectile;
    public PlayerMovement playerMovement;

	private void Start() {
        accuracy = 0.80f;
	}

    IEnumerator FireProjectile() {
        allowFire = false;
		GameObject projectileObj = Instantiate(projectile, new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity);

        float actualAccuracy = Random.Range(accuracy, 2.0f - accuracy);

		projectileObj.transform.Rotate(Vector3.up * transform.rotation.eulerAngles.y * actualAccuracy, Space.World);
		yield return new WaitForSeconds(fireSpeed);
		allowFire = true;
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && allowFire) {
            StartCoroutine(FireProjectile());
        }
	}

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Chest")) {
            int random = Random.Range(0, 3);

			if (random == 0) {
                if (playerMovement.movementSpeed < 10) {
                    playerMovement.movementSpeed += 0.1f;
                    Debug.Log("Movement buff: " + playerMovement.movementSpeed);
                } else if (fireSpeed > 0.3f) {
					fireSpeed -= 0.05f;
					Debug.Log("Firespeed buff: " + fireSpeed);
				}
			} else if (random == 1) {
                if (fireSpeed > 0.3f) {
                    fireSpeed -= 0.05f;
				    Debug.Log("Firespeed buff: " + fireSpeed);
                } else if (accuracy != 1.0f) {
					accuracy += 0.05f;
					Debug.Log("Accuracy buff: " + accuracy);
				}
            } else if (random == 2) {
                if (accuracy != 1.0f) {
                    accuracy += 0.05f;
				    Debug.Log("Accuracy buff: " + accuracy);
                } else if (playerMovement.movementSpeed < 10) {
					playerMovement.movementSpeed += 0.1f;
					Debug.Log("Movement buff: " + playerMovement.movementSpeed);
				}
            }
        }
    }
}
