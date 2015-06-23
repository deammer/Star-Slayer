﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditorPlaceholder : MonoBehaviour
{
	private static List<EditorPlaceholder> _instances = new List<EditorPlaceholder>();
	private static EditorPlaceholder _selectedInstance;

	public EditorNode nodePrefab;

	private Transform _path;
	private LineRenderer _pathRenderer;
	private List<EditorNode> _nodes;

	void Start()
	{
		_instances.Add(this);

		// cache the path and the existing nodes
		_path = transform.FindChild("Path");
		_pathRenderer = _path.GetComponent<LineRenderer>();
		_nodes = new List<EditorNode>();

		int index = 0;
		foreach (Transform child in _path)
		{
			_nodes.Add (child.GetComponent<EditorNode>());

			// set the previousNodes
			if (index == 0)
				_nodes[0].previousNode = transform;
			else
				_nodes[index].previousNode = _nodes[index - 1].transform;

			// set the nextNodes
			if (index > 0)
				_nodes[index - 1].nextNode = _nodes[index].transform;

			index ++;
		}
		UpdatePath();
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

	#region Node callbacks
	public void NodeMoved(EditorNode node)
	{
		UpdatePath();
	}

	public void NodeDropped(EditorNode node)
	{

	}

	public void NodeCreated(EditorNode node, int index)
	{
		// reorder the nodes
		_nodes.Insert(index, node);

		Debug.Log ("NodeCreated(): there are " + _nodes.Count + " nodes");
		Debug.Log("The first node is " + _nodes[0].name);
		Debug.Log("The last node is " + _nodes[_nodes.Count - 1].name);

		Debug.Log("The first node's previousNode is " + _nodes[0].previousNode.name);
		UpdatePath ();
	}

	#endregion

	private void UpdatePath()
	{
		_pathRenderer.SetVertexCount(_nodes.Count + 1);
		_pathRenderer.SetPosition(0, new Vector3());

		for (int i = 0; i < _nodes.Count; i++)
		{
			_pathRenderer.SetPosition(i + 1, _nodes[i].transform.localPosition);
		}
	}

	// each node has a box collider that goes all the way to the last node
	private void UpdateColliders()
	{
		/*Vector3 previousNode;
		Transform node;
		Vector3 direction = node.position - previousNode;
		node.right = direction.normalized;

		// resize collider
		BoxCollider2D collider = node.GetComponent<BoxCollider2D>();
		collider.offset = Vector3.right * direction.magnitude * .5f;
		collider.size = new Vector2(direction.magnitude, 1);
		*/
	}
}