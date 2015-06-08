using UnityEngine;
using System.Collections;

public class HomingProjectile : MonoBehaviour
{
	public float speed = 60f;
	public float turnRate = 90f;

	public string targetTag = "Enemy";

	private Transform target;
	private float angle;
	private Vector2 velocity = new Vector2();

	void Start ()
	{
		Destroy (gameObject, 5);
		target = GameObject.FindWithTag (targetTag).transform;
		Debug.Log ("target is " + target.ToString ());
	}
	
	void Update ()
	{
		var dx = target.position.x - transform.position.x;
		var dy = target.position.y - transform.position.y;

		if (velocity.y * dx - velocity.x * dy > 0)
			angle += turnRate * Time.deltaTime;
		else
			angle -= turnRate * Time.deltaTime;

		while (angle < 0) angle += 360f;
		while (angle > 360) angle -= 360f;

		velocity.x = speed * Mathf.Cos (angle * Mathf.Deg2Rad);
		velocity.y = speed * Mathf.Sin (angle * Mathf.Deg2Rad);

		Vector3 position = transform.position;
		position.x -= velocity.x * Time.deltaTime;
		position.y -= velocity.y * Time.deltaTime;
		transform.position = position;
	}
}
