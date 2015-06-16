using UnityEngine;
using System.Collections;

public class ThrusterSystem : MonoBehaviour
{
	public Transform backThruster;
	public Transform topThruster;
	public Transform bottomThruster;
	public Transform frontThruster;

	private ParticleSystem [] _backThrusters;
	private ParticleSystem [] _topThrusters;
	private ParticleSystem [] _bottomThrusters;
	private ParticleSystem [] _frontThrusters;

	private PlayerShipController _controller;

	void Start ()
	{
		_controller = GetComponent<PlayerShipController>();


	}
	
	void Update ()
	{
		Vector2 direction = _controller.direction;

		if (direction.x >= 0)
		{
			backThruster.gameObject.SetActive(true);
			frontThruster.gameObject.SetActive(false);
		}
		else
		{
			frontThruster.gameObject.SetActive(true);
			backThruster.gameObject.SetActive(false);
		}

		if (direction.y > 0)
		{
			bottomThruster.gameObject.SetActive(true);
			topThruster.gameObject.SetActive(false);
		}
		else if (direction.y < 0)
		{
			bottomThruster.gameObject.SetActive(false);
			topThruster.gameObject.SetActive(true);
		}
		else
		{
			bottomThruster.gameObject.SetActive(false);
			topThruster.gameObject.SetActive(false);
		}
	}
}
