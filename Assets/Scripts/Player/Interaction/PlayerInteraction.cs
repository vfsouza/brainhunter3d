using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Queue<GameObject> chests = new Queue<GameObject>();
    public int health = 3;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Chest")) {
            other.GetComponent<MeshRenderer>().enabled = false;
            other.GetComponent<BoxCollider>().enabled = false;
            chests.Enqueue(other.gameObject);
            Invoke("Respawn", 10);
        } else if (other.CompareTag("Enemy")) {
            Destroy(other.gameObject);
            health--;
            if (health == 0) {
                Destroy(gameObject);
            }
        }
    }

    private void Respawn() {
        GameObject chest = chests.Dequeue();
		chest.GetComponent<MeshRenderer>().enabled = true;
		chest.GetComponent<BoxCollider>().enabled = true;
	}
}
