using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private GameObject chest;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Chest")) {
            other.GetComponent<MeshRenderer>().enabled = false;
            other.GetComponent<BoxCollider>().enabled = false;
            chest = other.gameObject;
            Invoke("Respawn", 4);
        }
    }

    private void Respawn() {
		chest.GetComponent<MeshRenderer>().enabled = true;
		chest.GetComponent<BoxCollider>().enabled = true;
	}
}
