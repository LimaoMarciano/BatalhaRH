using UnityEngine;
using System.Collections;

public class PieceBox : MonoBehaviour {

	public int pieceQuantity = 10;
	public GameObject[] pieces;
	public Color disabledColor;
	public float maxRange;
	public float minRange;

	private int pieceRange;
	private int remainingPieces;
	private SpriteRenderer sprite;
	private GameObject vehicle;

	// Use this for initialization
	void Awake () {
		pieceRange = pieces.Length;
		remainingPieces = pieceQuantity;
		sprite = GetComponent<SpriteRenderer> ();
		vehicle = new GameObject();
		vehicle.name = "Vehicle";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GetPiece () {
		if (remainingPieces > 0) {

			//Piece creation
			int i = Random.Range (0, pieceRange);
			GameObject piece = Instantiate (pieces [i], transform.position, Quaternion.identity) as GameObject;
			piece.transform.SetParent (vehicle.transform);
			remainingPieces -= 1;

			GameManager.instance.AddPieceToManager (piece);

			//Change graphic when piece box is depleted
			if (remainingPieces == 0) {
				sprite.color = disabledColor;
				Debug.Log ("There's no remaining pieces left");
			}

			//Assign a random position to created piece and move it there
			float x = Random.Range (-maxRange, maxRange);
			float y = Random.Range (-maxRange, maxRange);
			Vector3 randomPos = new Vector3 (x, y, 0);

			piece.GetComponent<Piece>().StartCoroutine ("MoveToTarget", randomPos);
	
			Debug.Log ("Piece box released a piece. Remaining: " + remainingPieces);
		}
	}
}
