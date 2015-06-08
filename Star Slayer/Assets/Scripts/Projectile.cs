﻿using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
	[HideInInspector]
	public string ParentTag;
	[HideInInspector]
	public float Angle;

	public float Speed = 20;
	public int Damage;
	public Transform ImpactEffect;

	private Quaternion rotation;
	private Vector3 direction;

	void Start()
	{
		rotation = Quaternion.AngleAxis(Angle, Vector3.forward);
		transform.rotation = rotation;

		direction = new Vector3 (Mathf.Cos (Angle * Mathf.PI / 180), Mathf.Sin (Angle * Mathf.PI / 180), 0);
		Debug.Log (direction.ToString ());
		Destroy(gameObject, 3f);
	}

	void Update()
	{
		Vector3 position = transform.position;
		position.x += direction.x * Speed * Time.deltaTime;
		position.y += direction.y * Speed * Time.deltaTime;
		transform.position = position;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		bool hasImpacted = false;

		if (other.tag == "Enemy" && ParentTag == "Player")
		{
			hasImpacted = true;
		}
		else if (other.tag == "Player" && ParentTag == "Enemy")
		{
			hasImpacted = true;
		}

		if (hasImpacted)
			Impact();
	}

	protected void Impact()
	{
		if (ImpactEffect)
			Instantiate(ImpactEffect);

		Destroy(gameObject);
	}
}
