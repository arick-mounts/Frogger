using UnityEngine;

public class Car : MonoBehaviour {

	public Rigidbody2D rb;

	public static float StaticSpeed = 10f;

	public float speed = 1f;

	void Start()
	{
		speed = Random.Range(StaticSpeed - 2, StaticSpeed + 2);
	}

	void FixedUpdate () {
		Vector2 forward = new Vector2(transform.right.x, transform.right.y);
		rb.MovePosition(rb.position + forward * Time.fixedDeltaTime * speed);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Terminator")
		{
			this.Destroy();
		}
	}

    public void Destroy()
    {
        CarSpawner.cars.Remove(this.gameObject);
		Destroy(this.gameObject);
	}
}
