using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    CameraMovement cameraObj;

    [Range(30, 80)]
    public int cameraZoomOut;

	private enum State { MAP_VIEW, PLAYER_VIEW}
    private State _state;

    // Start is called before the first frame update
    void Start() {
		_state = State.PLAYER_VIEW;
        cameraObj = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.M)) {
            if (_state == State.PLAYER_VIEW) {
                cameraObj.offset = new Vector3(cameraObj.offset.x, cameraZoomOut, cameraObj.offset.z);
                _state = State.MAP_VIEW;
			} else {
				cameraObj.offset = new Vector3(cameraObj.offset.x, 16, cameraObj.offset.z);
				_state = State.PLAYER_VIEW;
			}
        }
    }
}
