using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverState : State {
	public Button restartButton;
	public Text messageText;
	public string victoryMessage;
	public string defeatMessage;

	public void SetVictory() {
		messageText.text = victoryMessage;
	}

	public void SetDefeat() {
		messageText.text = defeatMessage;
	}

	void Awake() {
		SetActionToButton(restartButton, OnRestart);
		Time.timeScale = 0f;
	}

	void OnRestart() {
		SceneManager.UnloadSceneAsync("Gameplay");
		SimpleStateMachine.instance.SetState<LoadingState>();
	}
}
