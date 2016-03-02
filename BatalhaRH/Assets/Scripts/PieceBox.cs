using UnityEngine;
using System.Collections;

public class PieceBox : MonoBehaviour {

	public int pieceQuantity = 10;
	public GameObject[] pieces;
	public Color disabledColor;

	private int range;
	private int remainingPieces;
	private SpriteRenderer sprite;
	private GameObject vehicle;

	// Use this for initialization
	void Awake () {
		range = pieces.Length;
		remainingPieces = pieceQuantity;
		sprite = GetComponent<SpriteRenderer> ();
		vehicle = new GameObject();
		vehicle.name = "Vehicle";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown () {
		if (remainingPieces > 0) {
			int i = Random.Range (0, range);

			float x = Random.Range (-4, 4);
			float y = Random.Range (-2, 4);
			Vector3 randomPos = new Vector3 (x, y, 0);

			GameObject piece = Instantiate (pieces [i], randomPos, Quaternion.identity) as GameObject;
			piece.transform.SetParent (vehicle.transform);
			remainingPieces -= 1;
			Debug.Log ("Piece box released a piece. Remaining: " + remainingPieces);
		} else {
			sprite.color = disabledColor;
			Debug.Log ("There's no remaining pieces left");
		}

	}
}
