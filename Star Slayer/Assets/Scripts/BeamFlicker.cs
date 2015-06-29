using UnityEngine;
using System.Collections;

public class BeamFlicker : MonoBehaviour
{
	private Vector3 _originalScale;
	private Vector3 _scale;

	void Start ()
	{
		_originalScale = transform.localScale;
		_scale = _originalScale;
	}
	
	void Update ()
	{
		_scale.y = Mathf.Sin(Mathf.Repeat(Time.time + Random.Range(0f, 2f), Mathf.PI * 2) * 15) * 1.5f;
		transform.localScale = _scale;
	}
}
