using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class HordeController : MonoBehaviour
{
    private enum State { ACTIVE, WAITING }
    private State _state;
    private int horde = 0;
    private string hordeTextTemplate = "Horde: ";
    private List<GameObject> hordeCount;

    public TextMeshProUGUI hordeText;
    public int maxHorde;
    public List<GameObject> spawners;
    public GameObject enemy;
    public int maxEnemies;

    void Start()
    {
        _state = State.WAITING;
        spawners = GameObject.FindGameObjectsWithTag("Spawn").ToList();
        hordeCount = GameObject.FindGameObjectsWithTag("Enemy").ToList();
		HordeTextUpdate();
    }

    private void HordeTextUpdate() {
        hordeText.text = hordeTextTemplate + horde.ToString();

	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && horde < maxHorde) {
            horde++;
            StartHorde();
            HordeTextUpdate();
        }
        TerminateHorde();
	}

    private void TerminateHorde() {
        if (hordeCount.Count == 0) {
            
        }
    }

    private void StartHorde() {
		_state = State.ACTIVE;
		for (int i = 0; i < spawners.Count; i++) {
            Instantiate(enemy, spawners[i].transform);
		}
        return;
    }
}
