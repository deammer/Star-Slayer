using UnityEngine;
using System.Collections;

public class MultiShotWeapon : Weapon
{
	public Transform[] origins;
	public bool alternateOrigins = false;

	private int lastOrigin = 0;

	override protected void Shoot()
	{
		if (alternateOrigins)
		{
			Transform origin;
			lastOrigin ++;
			if (lastOrigin >= origins.Length)
				lastOrigin = 0;

			origin = origins[lastOrigin];

			Transform bullet = Instantiate(ProjectilePrefab);
			bullet.position = origin.position;
			
			Projectile projectile = bullet.GetComponent<Projectile>();
			projectile.ParentTag = tag;
			projectile.Angle = Vector2.right;
		}
		else
		{
			for (int i = 0; i < origins.Length; i++)
			{
				Transform bullet = Instantiate(ProjectilePrefab);
				bullet.position = origins[i].position;
				
				Projectile projectile = bullet.GetComponent<Projectile>();
				projectile.ParentTag = tag;
				projectile.Angle = Vector2.right + GetAccuracyOffset();
			}
		}
	}
}
