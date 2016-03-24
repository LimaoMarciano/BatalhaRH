using UnityEngine;
using System.Collections;

public class CursorBehaviour : MonoBehaviour {

	public float cursorSpeed = 1;

	private float hInput;
	private float vInput;

	private string HorizontalMove;
	private string VerticalMove;
	private string HorizontalAim;
	private string VerticalAim;
	private string Grab;
	private string Fire;


	private Vector2 cursorPosition;
	private Collider2D targetPiece;
	private bool isHoldingPiece = false;
	private Vector2 holdInitialPos;
	private Vector2 pieceInitialPos;

	public enum Player {
		Player1,
		Player2,
		Player3,
		Player4
	};

	public Player player;

	// Use this for initialization
	void Start () {
		SetInputStrings ();
	}
	
	// Update is called once per frame
	void Update () {
		cursorPosition = transform.position;

		hInput = Input.GetAxis (HorizontalMove);
		vInput = Input.GetAxis (VerticalMove);

		if (GameManager.instance.gameState == GameState.SelectPieces) {
			if (Input.GetButtonDown (Grab)) {
				if (isHoldingPiece) {
					ReleasePiece ();
				} else {
					HoldPiece ();
				}
			}

			if (isHoldingPiece) {
//			float mouseWheelInput = Input.GetAxis ("Mouse ScrollWheel");
//			RotatePiece (mouseWheelInput);
				MovePiece ();
			}
		}

	}

	void FixedUpdate () {
		MoveCursor ();
	}

	void MoveCursor () {
		Vector3 input;
		input = new Vector3 (hInput, vInput, 0);
		Vector3.ClampMagnitude (input, 1);
		transform.Translate (input * cursorSpeed * Time.fixedDeltaTime);
	}

	void HoldPiece () {
		targetPiece = Physics2D.OverlapPoint(transform.position, LayerMask.GetMask("Pieces"));
		//		holdOffsetPos = targetPiece.transform.InverseTransformPoint(mousePosition);
		if (targetPiece) {
			isHoldingPiece = true;
			holdInitialPos = cursorPosition;
			pieceInitialPos = targetPiece.transform.position;
			Debug.Log ("Grabbed piece " + targetPiece.name);
		}
	}

	void MovePiece () {
		if (targetPiece) {
			targetPiece.transform.position = (cursorPosition - holdInitialPos) + pieceInitialPos;
		}
	}

	void ReleasePiece () {
		Debug.Log ("Released piece " + targetPiece.name);
		targetPiece = null;
		isHoldingPiece = false;
	}

	void SetInputStrings () {
		string playerPrefix = "";

//		switch (player) {
//		case Player.Player1:
//			playerPrefix = "P1";
//			break;
//		case Player.Player2:
//			playerPrefix = "P2";
//			break;
//		case Player.Player3:
//			playerPrefix = "P3";
//			break;
//		case Player.Player4:
//			playerPrefix = "P1";
//			break;
//		}

		if (player == Player.Player1) {
			playerPrefix = "P1";
		}
		if (player == Player.Player2) {
			playerPrefix = "P2";
		}
		if (player == Player.Player3) {
			playerPrefix = "P3";
		}
		if (player == Player.Player4) {
			playerPrefix = "P4";
		}

		HorizontalMove = playerPrefix + "HorizontalMove";
		VerticalMove = playerPrefix + "VerticalMove";
		HorizontalAim = playerPrefix + "HorizontalAim";
		VerticalAim = playerPrefix + "VerticalAim";
		Grab = playerPrefix + "Grab";
		Fire = playerPrefix + "Fire";
	}
}
