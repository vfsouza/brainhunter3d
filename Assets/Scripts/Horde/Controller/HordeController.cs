using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HordeController : MonoBehaviour
{
    private enum State { ACTIVE, WAITING }
    private State _state;
    private int horde = 0;
    private string hordeTextTemplate = "Horde: ";

    public TextMeshProUGUI hordeText;
    public int maxHorde;

    void Start()
    {
        _state = State.WAITING;
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
    }

    private void TerminateHorde() {
        _state = State.WAITING;
    }

    private void StartHorde() {
        _state = State.ACTIVE;
        return;
    }
}
