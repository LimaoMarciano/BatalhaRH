using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ProtoUI : MonoBehaviour {

	private GameObject vehicle;

	// Use this for initialization
	void Start () {
		vehicle = GameObject.Find ("Vehicle");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ActivatePhysics () {
		Rigidbody2D[] rbs = vehicle.transform.GetComponentsInChildren<Rigidbody2D> ();
		Debug.Log ("Activating physics. Found " + rbs.Length + " pieces");
		foreach (Rigidbody2D rb in rbs) {
			rb.isKinematic = false;
		}

		GameObject player = GameObject.Find ("Player");
		player.GetComponent<Rigidbody2D> ().isKinematic = false;
	}
}
