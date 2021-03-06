﻿using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
	public Transform ProjectilePrefab;

	[Range(0, 1f)]
	public float accuracy = 1f;
	public float ShotsPerSecond = 4f;

	protected string parentTag;
	private float shootingDelay;
	private bool canShoot;

	void Start ()
	{
		canShoot = true;
		parentTag = transform.root.tag;

		SpriteRenderer _renderer = GetComponent<SpriteRenderer>();
		if (_renderer != null)
			_renderer.enabled = false;
	}

	void Update ()
	{
		if (shootingDelay > 0)
			shootingDelay -= Time.deltaTime;
		else
			canShoot = true;

		if (Input.GetButton ("Fire") || Input.GetButton("Controller A"))
			Trigger();
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
		if (projectile != null)
		{
			projectile.ParentTag = parentTag;
			projectile.angle = 0 + GetAccuracyOffset () + transform.eulerAngles.z;
		}
	}
}
