using UnityEngine;
using System.Collections;

public class HomingProjectile : Projectile
{
	public float turnRate = 90f;

	private Transform target;
	private float angle;
	private Vector2 velocity = new Vector2();

	void Start ()
	{
		Destroy (gameObject, 5);
		target = GameObject.FindWithTag (ParentTag == "Player" ? "Enemy" : "Player").transform;
	}
	
	void Update ()
	{
		var dx = target.position.x - transform.position.x;
		var dy = target.position.y - transform.position.y;

		/*var dotProduct = velocity.y * dx - velocity.x * dy;
		if (dotProduct > 0)
			angle += turnRate * Time.deltaTime;
		else
			angle -= turnRate * Time.deltaTime;*/

		Vector2 direction = transform.position - target.position;
		//direction = target.InverseTransformDirection(direction);
		var targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // between -180 and 180
		if (Mathf.Abs(targetAngle - angle) <= turnRate * Time.deltaTime)
			angle = targetAngle;
	//	else
	//		angle = Mathf.Sign (a) * turnRate * Time.deltaTime;

		while (angle < 0) angle += 360f;
		while (angle > 360) angle -= 360f;

		velocity.x = Speed * Mathf.Cos (angle * Mathf.Deg2Rad);
		velocity.y = Speed * Mathf.Sin (angle * Mathf.Deg2Rad);

		Vector3 position = transform.position;
		position.x -= velocity.x * Time.deltaTime;
		position.y -= velocity.y * Time.deltaTime;
		transform.position = position;

		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}