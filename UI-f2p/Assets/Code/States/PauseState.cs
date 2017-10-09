using UnityEngine;
using UnityEngine.UI;

public class PauseState : State {
	public Sprite[] skins;
	public Button returnButton;
	public Transform skinElementsParent;
	public GameObject skinElementPrefab;

	void Awake() {
		SetActionToButton(returnButton, OnReturn);
		FillSkinsList();
		Time.timeScale = 0f;
	}

	void FillSkinsList() {
		for (int i = 0; i < skins.Length; i++) {
			GameObject skinInstance = GameObject.Instantiate(skinElementPrefab) as GameObject;
			skinInstance.transform.SetParent(skinElementsParent, false);

			SkinElement skinElement = skinInstance.GetComponent<SkinElement>();
			skinElement.image.sprite = skins[i];

			int index = i;
			skinElement.button.onClick.AddListener(() => {
				GameplayState.player.GetComponent<Player>().SetSkin(skins[index]);
				OnReturn();
			});
		}
	}

	void OnReturn() {
		SimpleStateMachine.instance.SetState<GameplayState>();
	}
}