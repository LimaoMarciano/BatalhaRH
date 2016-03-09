using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public SpriteRenderer sprite;
	public Color deadColor;
	public float damagePerForce = 2;
	private Rigidbody2D rb;
	private float health = 100;
	private bool isDead = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void Kill () {
		Debug.Log ("Player is dead");
		isDead = true;
		sprite.color = deadColor;
	}

	void OnCollisionEnter2D (Collision2D collision) {
		float impactForce;

		if (!isDead) {
			if (collision.rigidbody) {
				impactForce = collision.relativeVelocity.magnitude * collision.rigidbody.mass;
			} else {
				impactForce = rb.velocity.magnitude * rb.mass;
			}
			health -= impactForce * damagePerForce;
			Debug.Log ("Damage: " + impactForce * damagePerForce);
			if (health <= 0) {
				Kill ();
			}
		}
	}
}
