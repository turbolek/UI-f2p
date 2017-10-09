using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SwitchSkin : MonoBehaviour {
	public Sprite firstSprite;
	public Sprite secondSprite;
	SpriteRenderer spriteRenderer;

	void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void Switch() {
		if (spriteRenderer.sprite == firstSprite) {
			spriteRenderer.sprite = secondSprite;
		} else {
			spriteRenderer.sprite = firstSprite;
		}
	}
}
