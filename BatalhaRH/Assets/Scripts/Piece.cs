using UnityEngine;
using System.Collections;

public class Piece : MonoBehaviour {

	private int id;
	public PlayerNum owner;
	public bool isHeld = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator MoveToTarget (Vector3 targetPos) {
		while (Vector3.Distance (transform.position, targetPos) > 0.2f) {
			transform.position = Vector3.Lerp (transform.position, targetPos, 0.2f);

			yield return null;
		}
	}

	public void SetId (int value) {
		id = value;
	}
}
