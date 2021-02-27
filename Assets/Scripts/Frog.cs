using UnityEngine;
using UnityEngine.SceneManagement;

public class Frog : MonoBehaviour {

	public Rigidbody2D rb;

	void Update () {

		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			if (rb.position.x != 8)
				rb.MovePosition(rb.position + Vector2.right);
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			if (rb.position.x != -8)
				rb.MovePosition(rb.position + Vector2.left);
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow))
			rb.MovePosition(rb.position + Vector2.up);
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			if (rb.position.y != -4)
				rb.MovePosition(rb.position + Vector2.down);
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Car")
		{
			Debug.Log("Lost a life");
			GameObject.Find("GameManager").GetComponent<GameManager>().loseLife();
		}
	}
}
