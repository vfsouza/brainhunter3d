using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject _player;
    private CharacterController _ch;
    public float enemyMovement;
    // Start is called before the first frame update
    void Start()
    {
		_player = GameObject.FindGameObjectWithTag("Player");
        _ch = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(_player.transform.position);
        Movement();
    }

    private void Movement() {
		Vector3 movement = transform.forward * enemyMovement;
        _ch.Move(movement * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Projectile"))
        Destroy(gameObject);
    }
}
