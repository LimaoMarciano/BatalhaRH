using UnityEngine;
using System.Collections;

public class CursorBehaviour : MonoBehaviour {

	public float cursorSpeed = 1;
	public float rotationSpeed = 6;

	public LayerMask interactibleLayers;

	private float hInput;
	private float vInput;
	private float h2Input;
	private float v2Input;

	private string HorizontalMove;
	private string VerticalMove;
	private string HorizontalAim;
	private string VerticalAim;
	private string Action;
	private string Hammer;


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
		h2Input = Input.GetAxis (HorizontalAim);
		v2Input = Input.GetAxis (VerticalAim);

	

		if (Input.GetButtonDown (Action)) {

			if (isHoldingPiece) {
				ReleasePiece ();
			} else {
				targetCollider = PieceManager.instance.ActionEvent (this, cursorPosition);
				
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
		

		if (GameManager.instance.gameState == GameState.BindPieces) {
			if (Input.GetButtonDown (Hammer)) {
				PieceManager.instance.BindPieces (cursorPosition);
			}
		}

	}

	void FixedUpdate () {
		MoveCursor ();

		if (GameManager.instance.gameState != GameState.Battle) {
			RotatePiece ();
		}


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

	public void ReleasePiece () {
		Debug.Log ("Released piece " + targetPiece.name);
		PieceManager.instance.ReleasePiece (targetPiece);
		targetPiece = null;
		isHoldingPiece = false;
	}

	private void RotatePiece () {
		if (targetPiece) {
			Vector3 rotation = new Vector3 (0, 0, h2Input);
			targetPiece.transform.Rotate (rotation * rotationSpeed);
		}
	}

	void SetInputStrings () {
		string playerPrefix = "";

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
		Hammer = playerPrefix + "Hammer";
	}
		
}
