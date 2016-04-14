using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {

	public float horizontalPower = 200.0f;
	public float verticalPower = 300.0f;
	public Rigidbody2D rb;
	private float hInput;
	private float vInput;
	private GameObject vehicle;

	// Use this for initialization
	void Start () {
//		rb = GetComponent<Rigidbody2D> ();
//		vehicle = GameObject.Find ("Vehicle");
//		transform.SetParent (vehicle.transform);
	}
	
	// Update is called once per frame
	void Update () {
		hInput = Input.GetAxis ("P1HorizontalMove");
		vInput = Input.GetAxis ("P1VerticalMove");

		if (GameManager.instance.gameState == GameState.Battle) {
			rb.isKinematic = false;
		}
	}

	void FixedUpdate () {
//		Vector2 direction = new Vector2 (hInput, vInput);
		if (GameManager.instance.gameState == GameState.Battle) {
			if (hInput != 0 || vInput != 0) {
				rb.AddForce (new Vector2 (hInput, 0) * horizontalPower);
				rb.AddForce (new Vector2 (0, vInput) * verticalPower);
			}
		}
	}
}
