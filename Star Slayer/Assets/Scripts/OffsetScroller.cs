using UnityEngine;
using System.Collections;

public class OffsetScroller : MonoBehaviour
{
	[RangeAttribute(0f,1f)]
	public float ScrollSpeedRatio;
	private Vector2 savedOffset;
	private Renderer _renderer;
	private float textureOffset;
	
	void Start ()
	{
		savedOffset = GetComponent<Renderer>().sharedMaterial.GetTextureOffset ("_MainTex");
		_renderer = GetComponent<Renderer>();
		textureOffset = _renderer.material.mainTextureScale.x / transform.localScale.x;
	}
	
	void Update ()
	{
		float currentOffset = _renderer.sharedMaterial.GetTextureOffset("_MainTex").x;
		float x = Mathf.Repeat (currentOffset + Time.deltaTime * ScrollSpeedRatio * GameManager.instance.ScrollSpeed * textureOffset, 1f);
		Vector2 offset = new Vector2 (x, savedOffset.y);
		_renderer.sharedMaterial.SetTextureOffset ("_MainTex", offset);
	}
	
	void OnDisable ()
	{
		_renderer.sharedMaterial.SetTextureOffset ("_MainTex", savedOffset);
	}
}