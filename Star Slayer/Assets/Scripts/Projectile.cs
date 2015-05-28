using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
	[HideInInspector]
	public string ParentTag;
	[HideInInspector]
	public Vector3 Angle;

	public float Speed = 20;
	public int Damage;
	public Transform ImpactEffect;

	void Start()
	{
		Destroy(gameObject, 3f);
	}

	void Update()
	{
		transform.Translate(Angle * Speed * Time.deltaTime);
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
