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
			SetThrustersInTransform(backThruster, true);
			SetThrustersInTransform(frontThruster, false);
		}
		else
		{
			SetThrustersInTransform(frontThruster, true);
			SetThrustersInTransform(backThruster, false);
		}

		if (direction.y > 0)
		{
			SetThrustersInTransform(bottomThruster, true);
			SetThrustersInTransform(topThruster, false);
		}
		else if (direction.y < 0)
		{
			SetThrustersInTransform(bottomThruster, false);
			SetThrustersInTransform(topThruster, true);
		}
		else
		{
			SetThrustersInTransform(bottomThruster, false);
			SetThrustersInTransform(topThruster, false);
		}
	}

	private void SetThrustersInTransform(Transform t, bool enable)
	{
		ParticleSystem system;
		foreach (Transform child in t)
		{
			system = child.GetComponent<ParticleSystem>();
			if (system != null)
				system.enableEmission = enable;
		}
	}
}
