using UnityEngine;
using System.Collections;

public class MoveWithBackground : MonoBehaviour
{
	// move with the background
	void Update ()
	{
		transform.Translate(-GameManager.instance.ScrollSpeed * Time.deltaTime, 0, 0);
	}
}
