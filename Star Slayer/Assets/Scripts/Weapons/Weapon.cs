using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
	public Transform ProjectilePrefab;

	[Range(0, 1f)]
	public float accuracy = 1f;
	public float ShotsPerSecond = 4f;

	private string tag;
	private float shootingDelay;
	private bool canShoot;

	void Start ()
	{
		canShoot = true;

		if (transform.parent != null)
			tag = transform.parent.gameObject.tag;
	}
	
	void Update ()
	{
		if (shootingDelay > 0)
			shootingDelay -= Time.deltaTime;
		else
			canShoot = true;
	}

	virtual public void Trigger()
	{
		if (canShoot)
		{
			shootingDelay += 1f / ShotsPerSecond;
			canShoot = false;
			Shoot();
		}
	}

	protected float GetAccuracyOffset()
	{
		return Random.Range (-(accuracy * 45f - 45f), accuracy * 45f - 45f);
	}

	virtual protected void Shoot()
	{
		Transform bullet = Instantiate(ProjectilePrefab);
		bullet.position = transform.position;

		Projectile projectile = bullet.GetComponent<Projectile>();
		projectile.ParentTag = tag;
		projectile.Angle = 0 + GetAccuracyOffset();
	}
}
