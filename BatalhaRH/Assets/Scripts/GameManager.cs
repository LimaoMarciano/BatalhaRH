using UnityEngine;
using System.Collections;

public enum GameState {
	SelectPieces,
	BindPieces,
	BindWeapon,
	Battle
};

public class GameManager : MonoBehaviour {

	public static GameManager instance { get; private set; }

	public GameState gameState;

	// Use this for initialization
	void Start () {
		instance = this;
		gameState = GameState.SelectPieces;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
