using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	[SerializeField]
	protected int health = 2;

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
			Transform explosion = Instantiate(explosionEffect);
			explosion.position = transform.position;

			Destroy(gameObject);
		}
	}
}