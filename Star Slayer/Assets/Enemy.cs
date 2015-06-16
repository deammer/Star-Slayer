using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public int health;
	[HideInInspector]
	public bool destroyed;

	public Transform explosionEffect;

	void Awake()
	{
		destroyed = false;
	}

	public void Damage(int amount)
	{
		health = Mathf.Min(health - amount, 0);

		if (!destroyed && health <= 0)
		{
			Die();
		}
	}

	protected void Die()
	{
		if (destroyed) return;

		destroyed = true;

		if (explosionEffect)
		{
			Instantiate(explosionEffect, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}