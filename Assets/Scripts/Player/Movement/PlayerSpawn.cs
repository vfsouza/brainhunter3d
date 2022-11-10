using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{

    public MapBuilder mapBuilder;
    public CameraMovement cameraMovement;
    public GameObject spawnPrefab;

    private void Start() {
        int column = mapBuilder.width / 2;
		int line = mapBuilder.height / 2;
        GameObject obj = Instantiate(spawnPrefab, new Vector3(column * 5, 0.2f, line * 5), Quaternion.identity);
        cameraMovement.zombie = obj.transform;
	}
}
