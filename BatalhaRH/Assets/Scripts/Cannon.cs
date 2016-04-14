using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

	public GameObject ammoPrefab;
	public GameObject cannonHead;
	public float turnSpeed = 10;
	public float power = 50;
	public float cooldown = 1;

	private float hInput;
	private float vInput;
	private float timer = 0;
	private GameObject ammo;
	private GameObject vehicle;

	private bool isReady = true;
	private bool isFireBtnDown = false;

	// Use this for initialization
	void Start () {
		vehicle = GameObject.Find ("Vehicle");
		transform.SetParent (vehicle.transform);
	}
	
	// Update is called once per frame
	void Update () {

		if (GameManager.instance.gameState == GameState.Battle) {
			hInput = Input.GetAxis ("P1HorizontalAim");
			vInput = Input.GetAxis ("P1VerticalAim");

			Aim ();

			if (isReady) {
				if (Input.GetButtonDown ("P1Hammer")) {
					isFireBtnDown = true;
					isReady = false;
				}
			} else {
				timer += Time.deltaTime;
				if (timer >= cooldown) {
					isReady = true;
					timer = 0;
				}
			}
		}



	}

	void FixedUpdate () {
		if (isFireBtnDown) {
			Shoot ();
		}

		isFireBtnDown = false;
	}

	void Aim () {
		Vector3 direction = new Vector3 (hInput, vInput, 0);
		Vector3.Normalize (direction);

		if (direction != Vector3.zero) {
			float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
			cannonHead.transform.rotation = Quaternion.Slerp (cannonHead.transform.rotation, Quaternion.Euler (0, 0, angle), turnSpeed * Time.deltaTime);
		}
	}

	void Shoot () {
		ammo = Instantiate (ammoPrefab, transform.position, Quaternion.identity) as GameObject;
		ammo.GetComponent<Rigidbody2D> ().AddForce (cannonHead.transform.right * power, ForceMode2D.Impulse);
	}
}
