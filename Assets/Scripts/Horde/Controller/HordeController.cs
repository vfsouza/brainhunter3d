using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HordeController : MonoBehaviour
{
    private enum State { ACTIVE, WAITING }
    private State _state;
    private int horde = 0;
    private string hordeTextTemplate = "Horde: ";

    public List<GameObject> hordeCount = new List<GameObject>();

    public TextMeshProUGUI hordeText;
    public int maxHorde;
    public List<GameObject> spawners;
    public GameObject enemy;
    public int maxEnemies;

    void Start()
    {
		_state = State.WAITING;
        spawners = GameObject.FindGameObjectsWithTag("Spawn").ToList();
		HordeTextUpdate();
    }

    private void HordeTextUpdate() {
        hordeText.text = hordeTextTemplate + horde.ToString();

	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && horde < maxHorde && _state == State.WAITING) {
            horde++;
			_state = State.ACTIVE;
			StartCoroutine(StartHorde());
			HordeTextUpdate();
        } else if (hordeCount.Count == 0 && _state == State.ACTIVE) {
            TerminateHorde();
		}
		hordeCount = GameObject.FindGameObjectsWithTag("Enemy").ToList();
	}

    private void TerminateHorde() {
        _state = State.WAITING;
    }

	IEnumerator StartHorde() {
		for (int i = 0; i < maxEnemies * horde; i++) {
			for (int j = 0; j < spawners.Count; j++) {
				Instantiate(enemy, spawners[j].transform);
				yield return new WaitForSeconds(0.12f);
			}
		}
	}
}
