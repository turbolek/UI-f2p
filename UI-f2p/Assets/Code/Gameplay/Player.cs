using UnityEngine;

public class Player : MonoBehaviour {
	public float jumpForce = 5f;
	public float moveSpeed = 2f;
	public float size = 32;
	public float maxSizeY = 64;

	Sprite currentSkin;
	Rigidbody2D body;
	bool isGrounded;
	bool hasKey;

	public void SetSkin(Sprite skin) {
		GameObject skinObject = GetComponentInChildren<SpriteRenderer>().gameObject;
		Vector2 skinSize = skin.rect.size;

		Vector3 scale = Vector3.one;
		scale.x = size / skinSize.x;
		scale.y = size / skinSize.x;

		skinObject.transform.localScale = scale;
		skinObject.GetComponent<SpriteRenderer>().sprite = skin;
	}
	
	void Awake() {
		body = GetComponent<Rigidbody2D>();
		currentSkin = GetComponentInChildren<SpriteRenderer>().sprite;
		SetSkin(currentSkin);
	}

	void Update() {
		UpdateMovement();
		CheckLose();
	}

	void UpdateMovement() {
		if (Input.GetKey(KeyCode.A)) {
			MoveHorizontal(-1);
		} else if (Input.GetKey(KeyCode.D)) {
			MoveHorizontal(1);
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			Jump();
		}
	}

	void MoveHorizontal(float input) {
		transform.Translate(new Vector2(input * moveSpeed * Time.deltaTime, 0));
	}

	void Jump() {
		if (isGrounded) {
			body.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
			isGrounded = false;
		}
	}

	void CheckLose() {
		if (transform.position.y < -10f) {
			Defeat();
		}
	}

	void Victory() {
		SimpleStateMachine.instance.SetState<GameOverState>();
		GameOverState gameOver = SimpleStateMachine.instance.currentState as GameOverState;
		gameOver.SetVictory();

		GameObject.Destroy(gameObject);
	}

	void Defeat() {
		SimpleStateMachine.instance.SetState<GameOverState>();
		GameOverState gameOver = SimpleStateMachine.instance.currentState as GameOverState;
		gameOver.SetDefeat();

		GameObject.Destroy(gameObject);
	}

	void OnCollisionStay2D(Collision2D coll) {
		if (body.velocity.y == 0) {
			isGrounded = true;
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		isGrounded = false;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag.Equals("Finish") && hasKey) {
			Victory();
		} else if (collider.gameObject.tag.Equals("Key")) {
			hasKey = true;
			GameObject.FindGameObjectWithTag("Finish").GetComponent<SwitchSkin>().Switch();
			GameObject.Destroy(collider.gameObject);
		}
	}
}
