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

	public Collider2D ActionEvent (CursorBehaviour player, Vector3 actionPos) {
		Collider2D collider = Physics2D.OverlapPoint (actionPos, interactibleLayers);

		if (collider) {
			if (collider.gameObject.layer == LayerMask.NameToLayer ("PieceBox")) {
				GameManager.instance.pieceBox.GetPiece ();
				return null;
			}

			if (collider.gameObject.layer == LayerMask.NameToLayer ("Pieces")) {
				Piece piece = pieces [collider];
				if (!piece.isHeld) {
					piece.isHeld = true;
					piece.owner = player.playerNum;
					return collider;
				}

			}
		}

		return null;
	}
}
