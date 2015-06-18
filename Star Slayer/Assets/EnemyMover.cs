using UnityEngine;
using System.Collections;

public class EnemyMover : MonoBehaviour
{
	public int speed;

	private Vector2 _position = new Vector2();

	void Start()
	{
		_position = transform.position;
	}

	void Update ()
	{
		_position.x -= Time.deltaTime * speed;
		transform.position = _position;
	}
}
