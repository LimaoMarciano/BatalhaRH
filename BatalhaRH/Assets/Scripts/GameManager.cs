using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameState {
	SelectPieces,
	BindPieces,
	BindWeapon,
	Battle
};

public enum PlayerNum {
	Player1,
	Player2,
	Player3,
	Player4
};

public class GameManager : MonoBehaviour {

	public static GameManager instance { get; private set; }
	public LayerMask interactibleLayers;

	private Dictionary<Collider2D, Piece> pieces = new Dictionary<Collider2D, Piece>();
	private int id = 0;
	public GameState gameState;
	public PieceBox pieceBox;

	// Use this for initialization
	void Start () {
		instance = this;
		gameState = GameState.SelectPieces;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddPieceToManager (GameObject pieceObj) {
		Piece piece = pieceObj.GetComponent<Piece> ();
		Collider2D collider = pieceObj.GetComponent<Collider2D> ();
		piece.SetId (id);
		id += 1;
		pieces.Add (collider, piece);
	}

	public void ReleasePiece (Collider2D collider) {
		Piece piece = pieces [collider];
		piece.isHeld = false;
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
				Piece piece = pieces [collider];
				//Check is the piece is beign held by someone
				if (!piece.isHeld) {
					piece.isHeld = true;
					piece.owner = player;
					return collider;
				} else {
					piece.owner.ReleasePiece ();
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
