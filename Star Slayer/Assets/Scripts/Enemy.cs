using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	[SerializeField]
	protected int health = 2;
	protected float _actualHealth;

	[HideInInspector]
	public bool destroyed;

	public Transform explosionEffect;

	void Awake()
	{
		destroyed = false;
	}

	void Start()
	{
		_actualHealth = health;
	}

	public void Damage(float amount)
	{
		_actualHealth = Mathf.Max(_actualHealth - amount, 0f);

		if (!destroyed && _actualHealth <= 0)
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