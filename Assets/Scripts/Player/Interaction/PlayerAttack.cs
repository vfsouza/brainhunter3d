using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectile;
    public float fireSpeed;
    private bool allowFire = true;

	private void Start() {

	}

    IEnumerator FireProjectile() {
        allowFire = false;
		GameObject projectileObj = Instantiate(projectile, new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity);

		projectileObj.transform.Rotate(Vector3.up * transform.rotation.eulerAngles.y, Space.World);
        yield return new WaitForSeconds(0.5f);
		allowFire = true;
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && allowFire) {
            StartCoroutine(FireProjectile());
        }
	}
}
