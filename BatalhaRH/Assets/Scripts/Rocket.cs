using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {

	public float horizontalPower = 200.0f;
	public float verticalPower = 300.0f;
	private float hInput;
	private float vInput;
	private Rigidbody2D rb;
	private GameObject vehicle;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		vehicle = GameObject.Find ("Vehicle");
		transform.SetParent (vehicle.transform);
	}
	
	// Update is called once per frame
	void Update () {
		hInput = Input.GetAxis ("Horizontal");
		vInput = Input.GetAxis ("Vertical");
	}

	void FixedUpdate () {
//		Vector2 direction = new Vector2 (hInput, vInput);
		if (hInput != 0 || vInput != 0) {
			rb.AddForce (new Vector2(hInput, 0) * horizontalPower);
			rb.AddForce (new Vector2(0, vInput) * verticalPower);
		}
	}
}
