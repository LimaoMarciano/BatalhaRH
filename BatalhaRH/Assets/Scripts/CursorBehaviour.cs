using UnityEngine;
using System.Collections;

public class CursorBehaviour : MonoBehaviour {

	public float cursorSpeed = 1;

	public LayerMask interactibleLayers;

	private float hInput;
	private float vInput;

	private string HorizontalMove;
	private string VerticalMove;
	private string HorizontalAim;
	private string VerticalAim;
	private string Action;
	private string Fire;


	private Vector2 cursorPosition;
	private Collider2D targetPiece;
	private bool isHoldingPiece = false;
	private Vector2 holdInitialPos;
	private Vector2 pieceInitialPos;

	public PlayerNum playerNum;

	// Use this for initialization
	void Start () {
		SetInputStrings ();
	}
	
	// Update is called once per frame
	void Update () {
		Collider2D targetCollider;

		cursorPosition = transform.position;

		hInput = Input.GetAxis (HorizontalMove);
		vInput = Input.GetAxis (VerticalMove);

		if (GameManager.instance.gameState == GameState.SelectPieces) {

			if (Input.GetButtonDown (Action)) {

				if (isHoldingPiece) {
					ReleasePiece ();
				} else {
					targetCollider = GameManager.instance.ActionEvent (this, cursorPosition);
					
					if (targetCollider) {
						HoldPiece (targetCollider);
					}
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

	void HoldPiece (Collider2D collider) {
		targetPiece = collider;
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

		if (playerNum == PlayerNum.Player1) {
			playerPrefix = "P1";
		}
		if (playerNum == PlayerNum.Player2) {
			playerPrefix = "P2";
		}
		if (playerNum == PlayerNum.Player3) {
			playerPrefix = "P3";
		}
		if (playerNum == PlayerNum.Player4) {
			playerPrefix = "P4";
		}

		HorizontalMove = playerPrefix + "HorizontalMove";
		VerticalMove = playerPrefix + "VerticalMove";
		HorizontalAim = playerPrefix + "HorizontalAim";
		VerticalAim = playerPrefix + "VerticalAim";
		Action = playerPrefix + "Action";
		Fire = playerPrefix + "Fire";
	}
}
