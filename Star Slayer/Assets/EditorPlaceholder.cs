using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditorPlaceholder : MonoBehaviour
{
	private static List<EditorPlaceholder> _instances = new List<EditorPlaceholder>();
	private static EditorPlaceholder _selectedInstance;

	private Transform _path;

	void Start()
	{
		_instances.Add(this);

		_path = transform.FindChild("Path");
	}

	void OnMouseUp()
	{
		Select();
	}

	public void Select()
	{
		if (_selectedInstance != null)
			_selectedInstance.Deselect();

		_selectedInstance = this;

		// update the visuals
		_path.gameObject.SetActive(true);
		GetComponent<SpriteRenderer>().color = Color.green;
	}

	public void Deselect()
	{
		_path.gameObject.SetActive(false);
		GetComponent<SpriteRenderer>().color = Color.white;
	}

	void OnDestroy()
	{
		_instances.Remove(this);
	}

	// each node has a box collider that goes all the way to the last node
	private void UpdateColliders()
	{
		Vector3 previousNode;
		Transform node;
		Vector3 direction = node.position - previousNode;
		node.right = direction.normalized;

		// resize collider
		BoxCollider2D collider = node.GetComponent<BoxCollider2D>();
		collider.offset = Vector3.right * direction.magnitude * .5f;
		collider.size = new Vector2(direction.magnitude, 1);

	}
}
