using UnityEngine;
using System.Collections;

public class ComboWeapon : Weapon
{
	public Weapon[] Weapons;

	public override void Trigger ()
	{
		foreach (Weapon weapon in Weapons)
			weapon.Trigger();
	}
}