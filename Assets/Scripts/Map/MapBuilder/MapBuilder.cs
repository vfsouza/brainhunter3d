using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour {
	private int[,] terrainMap;
	private Vector3[,] mapPosition;

	public GameObject grassTile;
	public GameObject mountainTile;
	public GameObject spawnObj;
	public GameObject chestObj;
	public GameObject roadTile;

	[Range(10, 50)]
	public int width;
	[Range(10, 50)]
	public int height;
	[Range(0, 100)]
	public int iniChance;
	[Range(1, 8)]
	public int birthLimit;
	[Range(1, 8)]
	public int deathLimit;
	[Range(1, 10)]
	public int numR;
	[Range(1, 3)]
	public int maxChests;

	public void doSim(int nu) {
		if (terrainMap == null) {
			terrainMap = new int[width, height];
			mapPosition = new Vector3[width, height];
			initPos();
		}
		for (int i = 0; i < nu; i++) {
			terrainMap = genTilePos(terrainMap);
		}
	}

	public void initPos() {
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				mapPosition[i, j] = new Vector3(i * 5.0f, 0, j * 5.0f);
			}
		}
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				terrainMap[x, y] = UnityEngine.Random.Range(1, 101) < iniChance ? 1 : 0;
			}
		}
	}

	public void genChests() {
		int i = 0;
		do {
			int column = UnityEngine.Random.Range(2, width - 1);
			int line = UnityEngine.Random.Range(2, height - 1);

			Debug.Log("Chest: " + column + ":" + line);

			if (terrainMap[column, line] != 0) {
				terrainMap[column, line] = 2;
				chestPosition.Add(new int[] { column, line });
				i++;
			}
		} while (i < maxChests);
	}

	public int[,] genTilePos(int[,] oldMap) {
		int[,] newMap = new int[width, height];
		int neighb;
		BoundsInt myB = new BoundsInt(-1, -1, 0, 3, 3, 1);

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				neighb = 0;
				foreach (var b in myB.allPositionsWithin) {
					if (b.x == 0 && b.y == 0) continue;
					if (x + b.x >= 0 && x + b.x < width && y + b.y >= 0 && y + b.y < height) {
						neighb += oldMap[x + b.x, y + b.y];
					} else {
						neighb++;
					}
				}

				if (oldMap[x, y] == 1) {
					if (neighb < deathLimit) newMap[x, y] = 0;

					else {
						newMap[x, y] = 1;
					}
				}

				if (oldMap[x, y] == 0) {
					if (neighb > birthLimit) newMap[x, y] = 1;

					else {
						newMap[x, y] = 0;
					}
				}
			}
		}
		return newMap;
	}

	public void buildMap() {
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				if (terrainMap[i, j] == 0) {
					Instantiate(mountainTile, mapPosition[i, j], Quaternion.identity);
				} else if (terrainMap[i, j] == 1) {
					Instantiate(grassTile, mapPosition[i, j], Quaternion.identity);
				} else if (terrainMap[i, j] == 2) {
					Instantiate(chestObj, new Vector3(mapPosition[i, j].x, 0.4f, mapPosition[i, j].z), Quaternion.identity);
					Instantiate(grassTile, mapPosition[i, j], Quaternion.identity);
				} else if (terrainMap[i, j] == 3) {
					Instantiate(roadTile, mapPosition[i, j], Quaternion.identity);
				}

				if ((i == 0 && j == 0) || (i == width - 1 && j == height - 1) || (i == 0 && j == height - 1) || (i == width - 1 && j == 0)) {
					Debug.Log("Spawn: " + i + " " + j);
					Instantiate(spawnObj, new Vector3(mapPosition[i, j].x, 1f, mapPosition[i, j].z), Quaternion.identity);
				}
			}
		}
	}

	// Start is called before the first frame update
	void Start() {
		doSim(numR);
		genChests();
		Vinibala();
		buildMap();
	}

	// Update is called once per frame
	void Update() {

	}

	private List<int[]> chestPosition = new List<int[]>();

	private void Vinibala() {
		
		for (int j = 1; j < 3; j++) {
			int[] initialPosition = chestPosition[0];
			int[] finalPosition = chestPosition[j];
			for (int i = initialPosition[0] < finalPosition[0] ? initialPosition[0] : finalPosition[0]; i < (initialPosition[0] > finalPosition[0] ? initialPosition[0] : finalPosition[0]); i++) {
				if (terrainMap[i, initialPosition[1]] != 2) {
					terrainMap[i, initialPosition[1]] = 3;
				}
			}

			for (int i = initialPosition[1] < finalPosition[1] ? initialPosition[1] : finalPosition[1]; i < (initialPosition[1] > finalPosition[1] ? initialPosition[1] : finalPosition[1]); i++) {
				if (terrainMap[finalPosition[0], i] != 2) {
					terrainMap[finalPosition[0], i] = 3;
				}
			}
		}
	}
} 
