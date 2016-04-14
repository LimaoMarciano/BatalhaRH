using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PieceManager : MonoBehaviour {

	public static PieceManager instance { get; private set; }
	public LayerMask interactibleLayers;
	public GameObject nailPrefab;

	private Dictionary<Collider2D, Piece> piecesList = new Dictionary<Collider2D, Piece>();
	private int id = 0;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddPieceToManager (GameObject pieceObj) {
		Piece piece = pieceObj.GetComponent<Piece> ();
		Collider2D collider = pieceObj.GetComponent<Collider2D> ();
		piece.SetId (id);
		id += 1;
		piecesList.Add (collider, piece);
	}

	public void ReleasePiece (Collider2D collider) {
		Piece piece = piecesList [collider];
		piece.isHeld = false;
	}

	public void BindPieces (Vector2 bindPosition) {
		Collider2D[] piecesHit;
		HingeJoint2D[] joints;
		GameObject nail;

		piecesHit = Physics2D.OverlapPointAll (bindPosition, LayerMask.GetMask ("Pieces"));



		if (piecesHit.Length > 1) {
			Debug.Log ("Binding " + piecesHit.Length + " pieces together.");

			foreach (Collider2D col in piecesHit) {
				if (col.gameObject.tag != "Chassis") {
					Piece piece = piecesList [col];
					piece.isBinded = true;
				}
			}

			nail = Instantiate (nailPrefab, bindPosition, Quaternion.identity) as GameObject;
			//			nail.transform.SetParent (vehicle.transform);
			for (int i = 0; i < piecesHit.Length; i++) {
				nail.gameObject.AddComponent<HingeJoint2D> ();
			}

			joints = nail.GetComponents<HingeJoint2D> ();
			for (int j = 0; j < piecesHit.Length; j++) {
				
				joints [j].connectedBody = piecesHit [j].attachedRigidbody;
				joints [j].anchor = nail.transform.InverseTransformPoint (bindPosition);
				joints [j].connectedAnchor = piecesHit [j].transform.InverseTransformPoint (bindPosition);
				joints [j].enableCollision = false;
				joints [j].breakForce = 500;
			}

		} else {
			Debug.Log ("There's no pieces to bind.");
		}

	}

	public Collider2D ActionEvent (CursorBehaviour player, Vector3 actionPos) {
		Collider2D collider = Physics2D.OverlapPoint (actionPos, interactibleLayers);

		if (collider) {
			//If a player interacts with the piece box
			if (collider.gameObject.layer == LayerMask.NameToLayer ("PieceBox")) {
				GameManager.instance.pieceBox.SpawnPiece ();
				return null;
			}

			//If a player interacts with a piece
			if (collider.gameObject.layer == LayerMask.NameToLayer ("Pieces")) {
				Piece piece = piecesList [collider];
				//Check is the piece is beign held by someone
				if (!piece.isHeld) {
					if (piece.isBinded) {
						return null;
					}
					piece.isHeld = true;
					piece.holder = player;
					return collider;
				} else {
					piece.holder.ReleasePiece ();
					Vector3 randomPos = new Vector3 (Random.Range (-1, 1), Random.Range (-1, 1), 0);
					if (randomPos.x >= 0) {
						randomPos.x += 0.5f;
					}
					if (randomPos.x < 0) {
						randomPos.x -= 0.5f;
					}
					if (randomPos.y >= 0) {
						randomPos.y += 0.5f;
					}
					if (randomPos.y < 0) {
						randomPos.y -= 0.5f;
					}
					Vector3 targetPos = piece.transform.position + randomPos;
					piece.StartCoroutine ("MoveToTarget", targetPos);
				}

			}
		}

		return null;
	}
}
