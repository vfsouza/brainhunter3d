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
    private PlayerAttack pa;
    private PlayerInteraction pi;
    private PlayerMovement pm;
    private float playerAccuracy;
    private float playerMovement;
    private float playerFirerate;
    private int playerHealth;
    private Registro registro;
    private int enemyHealth = 1;
    private GameObject enemyAlive;

    public List<GameObject> hordeCount = new List<GameObject>();
    public TextMeshProUGUI hordeText;
    public int maxHorde;
    public List<GameObject> spawners;
    public GameObject enemy;
    public int maxEnemies;
    public TextMeshProUGUI healthText;

    void Start()
    {
        healthText.text = "Enemy health: " + enemyHealth;
        registro = new Registro();
        pa = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
		pi = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();
		pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

		_state = State.WAITING;
        spawners = GameObject.FindGameObjectsWithTag("Spawn").ToList();
		HordeTextUpdate();
    }

    private void HordeTextUpdate() {
        hordeText.text = hordeTextTemplate + horde.ToString();

	}

    void Update()
    {

		playerAccuracy = pa.accuracy;
		playerFirerate = pa.fireSpeed;
		playerHealth = pi.health;
		playerMovement = pm.movementSpeed;

		if (Input.GetKeyDown(KeyCode.Space) && horde < maxHorde && _state == State.WAITING) {
            horde++;
			_state = State.ACTIVE;
			StartCoroutine(StartHorde());
			HordeTextUpdate();
        } else if (hordeCount.Count == 0 && _state == State.ACTIVE) {
            TerminateHorde();
		}

		hordeCount = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        GetPlayerUpgrade();
	}

    public void GetPlayerUpgrade() {
        if (playerAccuracy != pa.accuracy || playerFirerate != pa.fireSpeed || playerHealth != pi.health || playerMovement != pm.movementSpeed) {
            registro.registro.Add(new Registro(pa.accuracy, pa.fireSpeed, pi.health, pm.movementSpeed));
        }
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

		for (int i = 0; i < hordeCount.Count; i++) {
			hordeCount[i].GetComponent<EnemyMovement>().health = enemyHealth;
		}

		if (pa.accuracy >= 0.85) {
			if (pa.fireSpeed <= 0.4) {
				enemyHealth += 1;
			}
			enemyHealth += 1;
		} else {
			if (pi.health > 2) {
				enemyHealth += 1;
			}
		}

		enemyAlive = GameObject.FindGameObjectWithTag("Enemy");
        healthText.text = "Enemy health: " + enemyAlive.GetComponent<EnemyMovement>().health;
	}

    class Registro {
        public bool classEnemy { get; set; }
        public float accuracy { get; set; }
        public float firerate { get; set; }
        public int health { get; set; }
        public float movementSpeed { get; set; }
        public List<Registro> registro { get; set; } = new List<Registro>();

		public Registro(float accuracy, float firerate, int health, float movementSpeed) {
			this.accuracy = accuracy;
			this.firerate = firerate;
			this.health = health;
			this.movementSpeed = movementSpeed;
		}

		public Registro(float accuracy, float firerate, int health, float movementSpeed, bool classEnemy) {
            this.accuracy = accuracy;
            this.firerate = firerate;
            this.health = health;
            this.movementSpeed = movementSpeed;
            this.classEnemy = classEnemy;
        }

        public Registro() {
            registro.Add(new Registro(0.8f, 0.4f, 3, 8.5f, true));
			registro.Add(new Registro(1f, 0.4f, 3, 9f, true));
			registro.Add(new Registro(0.7f, 0.5f, 2, 8.5f, false));
			registro.Add(new Registro(0.85f, 0.5f, 1, 8.5f, false));
			registro.Add(new Registro(0.75f, 0.4f, 1, 10f, true));
			registro.Add(new Registro(0.8f, 0.5f, 1, 8f, false));
			registro.Add(new Registro(1f, 0.3f, 3, 10f, true));
			registro.Add(new Registro(0.8f, 0.4f, 3, 8.5f, false));
			registro.Add(new Registro(0.7f, 0.35f, 2, 9f, true));
			registro.Add(new Registro(0.75f, 0.45f, 2, 8f, false));
		}
    }
}
