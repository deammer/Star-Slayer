using UnityEngine;
using System.Collections;

public class HomingProjectile : Projectile
{
	public float turnRate = 90f;

	private Transform target;
	private Vector2 velocity = new Vector2();
	private float currentSpeed = 0;

	[SerializeField]
	private float acceleration = 5f;

	void Start ()
	{
		Destroy (gameObject, 5);
		FindTarget();

		// start at half speed
		currentSpeed = Speed * .2f;
	}

	void Update()
	{
		if (target != null)
		{
			Vector2 direction = target.position - transform.position;
			var targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			if (targetAngle < 0) targetAngle += 360f;
			
			// by now, 0 <= targetAngle < 360
			
			// angle diff
			float angleDifference = targetAngle - angle;
			while (angleDifference > 180) angleDifference -= 360f;
			while (angleDifference <= -180) angleDifference += 360f;

			// the turn speed is proportional to the speed of the projectile
			var proportionalTurnRate = turnRate * currentSpeed / Speed;

			if (Mathf.Abs(angleDifference) < proportionalTurnRate * Time.deltaTime)
				angle = targetAngle;
			else
				angle += Mathf.Sign(angleDifference) * proportionalTurnRate * Time.deltaTime;

			// keep 0 <= angle < 360f
			if (angle < 0) angle += 360f;
			//Debug.Log("Angle: " + angle + ", Target: " + targetAngle + ", Diff: " + angleDifference);
		}
		else
			FindTarget();

		// set the rotation
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		// accelerate
		currentSpeed = Mathf.Clamp(currentSpeed + acceleration * Time.deltaTime, 0, Speed);

		// move forward since we're rotated
		transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawRay(transform.position, transform.right * 5);

		if (target != null)
		{
			Vector2 direction = target.position - transform.position;
			Gizmos.color = Color.green;
			Gizmos.DrawRay(transform.position, direction * .5f);
		}
	}

	private void FindTarget()
	{
		if (ParentTag == "Player")
		{
			// find the closest enemy
			GameObject [] enemies = GameObject.FindGameObjectsWithTag("Enemy");
			if (enemies == null || enemies.Length == 0)
				target = null;
			else
			{
				GameObject closest = null;
				float distance = Mathf.Infinity;
				Vector3 position = transform.position;
				foreach (GameObject go in enemies)
				{
					Vector3 diff = go.transform.position - position;
					float curDistance = diff.sqrMagnitude;
					if (curDistance < distance)
					{
						distance = curDistance;
						closest = go;
					}
				}
				target = closest.transform;
			}
		}
		else if (ParentTag == "Enemy")
		{
			// target the player
			target = GameObject.FindWithTag("Player").transform;
		}
	}
}