using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	[Range (0.0f, 1.0f)]
	public float pieceRotationSpeed = 0.3f;
	public GameObject nailPrefab;

	private Vector2 mousePosition;
	private Collider2D targetPiece;
	private bool isHoldingPiece = false;
	private Vector2 holdInitialPos;
	private Vector2 pieceInitialPos;

	private float mouseWheelInput;
	private GameObject vehicle;

	// Use this for initialization
	void Start () {
		vehicle = GameObject.Find ("Vehicle");
	}
	
	// Update is called once per frame
	void Update () {
		mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		if (Input.GetMouseButtonDown(0)) {
			if (isHoldingPiece) {
				ReleasePiece ();
			} else {
				HoldPiece ();
			}

		}

		if (isHoldingPiece) {
			mouseWheelInput = Input.GetAxis ("Mouse ScrollWheel");
			RotatePiece (mouseWheelInput);
			MovePiece ();
		}

		if (Input.GetMouseButtonDown (1)) {
			BindPieces ();
		}
	
	}

	void HoldPiece () {
		targetPiece = Physics2D.OverlapPoint(mousePosition, LayerMask.GetMask("Pieces"));
//		holdOffsetPos = targetPiece.transform.InverseTransformPoint(mousePosition);
		if (targetPiece) {
			isHoldingPiece = true;
			holdInitialPos = mousePosition;
			pieceInitialPos = targetPiece.transform.position;
			Debug.Log ("Grabbed piece " + targetPiece.name);
		}

	}

	void MovePiece () {
		if (targetPiece) {
			targetPiece.transform.position = (mousePosition - holdInitialPos) + pieceInitialPos;
		}
	}

	void ReleasePiece () {
		Debug.Log ("Released piece " + targetPiece.name);
		targetPiece = null;
		isHoldingPiece = false;
	}

	void RotatePiece (float input) {
		Vector3 rotation = new Vector3 (0, 0, input * 100);
		targetPiece.transform.Rotate (rotation * pieceRotationSpeed);
//		targetPiece.transform.RotateAround(mousePosition, Vector3.forward, input * 100 * pieceRotationSpeed);
	}

	void BindPieces () {
		Collider2D[] pieces;
		HingeJoint2D[] joints;
		GameObject nail;

		pieces = Physics2D.OverlapPointAll (mousePosition, LayerMask.GetMask ("Pieces"));


		if (pieces.Length > 1) {
			Debug.Log ("Binding " + pieces.Length + " pieces together.");

			nail = Instantiate (nailPrefab, mousePosition, Quaternion.identity) as GameObject;
			nail.transform.SetParent (vehicle.transform);
			for (int i = 0; i < pieces.Length; i++) {
				nail.gameObject.AddComponent<HingeJoint2D> ();
			}

			joints = nail.GetComponents<HingeJoint2D> ();
			for (int j = 0; j < pieces.Length; j++) {
				joints [j].connectedBody = pieces [j].attachedRigidbody;
				joints [j].anchor = nail.transform.InverseTransformPoint (mousePosition);
				joints [j].connectedAnchor = pieces [j].transform.InverseTransformPoint (mousePosition);
				joints [j].enableCollision = false;
				joints [j].breakForce = 500;
			}
				
		} else {
			Debug.Log ("There's no pieces to bind.");
		}

	}
}
