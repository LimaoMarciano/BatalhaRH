using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ProtoUI : MonoBehaviour {

	public Text textPhaseTimer;

	private GameObject vehicle;

	// Use this for initialization
	void Start () {
		vehicle = GameObject.Find ("Vehicle");
	}
	
	// Update is called once per frame
	void Update () {
		textPhaseTimer.text = GameManager.instance.GetPhaseTimer().ToString();
	}

	public void ActivatePhysics () {
		GameManager.instance.gameState = GameState.Battle;

//		Rigidbody2D[] rbs = vehicle.transform.GetComponentsInChildren<Rigidbody2D> ();
//		Debug.Log ("Activating physics. Found " + rbs.Length + " pieces");
//		foreach (Rigidbody2D rb in rbs) {
//			rb.isKinematic = false;
//		}

		GameObject[] pieces = GameObject.FindGameObjectsWithTag ("PhysicsObj");
		foreach (GameObject go in pieces) {
			go.GetComponent<Rigidbody2D> ().isKinematic = false;
		}

		GameObject player = GameObject.Find ("Player");
		player.GetComponent<Rigidbody2D> ().isKinematic = false;
	}
}
