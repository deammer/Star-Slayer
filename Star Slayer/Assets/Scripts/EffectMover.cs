using UnityEngine;
using System.Collections;

public class EffectMover : MonoBehaviour
{
	public Vector2 direction;

	void Update ()
	{
		transform.Translate(direction.x * Time.deltaTime, direction.y * Time.deltaTime, 0);
	}
}
