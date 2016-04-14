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
	public GameState[] gamePhases;
	public int gamePhaseDuration = 20;
	public PieceBox pieceBox;

	public GameState gameState;

	private int currentGamePhase = 0;
	private bool isBattleStarted = false;
	private int phaseTimer;


	// Use this for initialization
	void Start () {
		instance = this;
		gameState = gamePhases[0];
		StartCoroutine ("timer", gamePhaseDuration);
	}
	
	// Update is called once per frame
	void Update () {
		if (phaseTimer == 0) {
			NextPhase ();
		}

		if (gameState == GameState.Battle && isBattleStarted == false) {
			isBattleStarted = true;
			ActivatePhysics ();
		}
	}

	public void NextPhase () {
		if (currentGamePhase < gamePhases.Length) {
			Debug.Log ("Changing game phase");
			currentGamePhase += 1;
			gameState = gamePhases [currentGamePhase];
			StartCoroutine ("timer", gamePhaseDuration);
		}
	}

	public void ActivatePhysics () {
		//		Rigidbody2D[] rbs = vehicle.transform.GetComponentsInChildren<Rigidbody2D> ();
		//		Debug.Log ("Activating physics. Found " + rbs.Length + " pieces");
		//		foreach (Rigidbody2D rb in rbs) {
		//			rb.isKinematic = false;
		//		}

		GameObject[] pieces = GameObject.FindGameObjectsWithTag ("PhysicsObj");
		foreach (GameObject go in pieces) {
			go.GetComponent<Rigidbody2D> ().isKinematic = false;
		}

		GameObject player = GameObject.Find ("Player");
		player.GetComponent<Rigidbody2D> ().isKinematic = false;
	}

	public int GetPhaseTimer () {
		return phaseTimer;
	}

	private IEnumerator timer (int count) {

		phaseTimer = count;

		while (phaseTimer != 0) {
			yield return new WaitForSeconds (1f);
			phaseTimer -= 1;
		}
	}
}
