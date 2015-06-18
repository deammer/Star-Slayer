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

		#region Doesn't really work
		// match the angle to -180 -> 180
		while (angle > 180) angle -= 360f;
		while (angle <= -180) angle += 360f;

		float angleDiff = targetAngle - angle;
		angleDiff = Mathf.Clamp(angleDiff, -turnRate * Time.deltaTime, turnRate * Time.deltaTime);

		if (Mathf.Abs(targetAngle - angle) <= turnRate * Time.deltaTime)
			angle = targetAngle;
		else
			angle += angleDiff;

		#endregion

		#region New approach

		targetAngle = Mathf.Atan2(direction.y, direction.x);
		angle = GetNewAngle(angle, targetAngle, turnRate * Time.deltaTime * 2f) * Mathf.Rad2Deg;

		#endregion

		velocity.x = Speed * Mathf.Cos (angle * Mathf.Deg2Rad);
		velocity.y = Speed * Mathf.Sin (angle * Mathf.Deg2Rad);

		Vector3 position = transform.position;
		position.x -= velocity.x * Time.deltaTime;
		position.y -= velocity.y * Time.deltaTime;
		transform.position = position;

		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	private float GetNewAngle(float fromRad, float toRad, float step)
	{
		// Ensure that 0 <= angle < 2pi for both "from" and "to" 
		while (fromRad < 0) 
			fromRad += Mathf.PI * 2f;
		while (fromRad >= Mathf.PI * 2f)
			fromRad -= Mathf.PI * 2f;
		
		while (toRad < 0) 
			toRad += Mathf.PI * 2f; 
		while(toRad >= Mathf.PI * 2f) 
			toRad -= Mathf.PI * 2f; 
		
		if(Mathf.Abs(fromRad - toRad) < Mathf.PI) 
		{ 
			// The simple case - a straight lerp will do. 
			return Mathf.Lerp(fromRad, toRad, step); 
		} 
		
		// If we get here we have the more complex case. 
		// First, increment the lesser value to be greater. 
		if(fromRad < toRad) 
			fromRad += Mathf.PI * 2f;
		else 
			toRad += Mathf.PI * 2f;
		
		float retVal = Mathf.Lerp(fromRad, toRad, step); 
		
		// Now ensure the return value is between 0 and 2pi 
		if(retVal >= Mathf.PI * 2f)
			retVal -= Mathf.PI * 2f;
		return retVal;
	}
}