using UnityEngine;
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

	// play mode
	private Vector3 _originalPosition;
	private int _currentNodeIndex;
	private float _currentSpeed;

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

	#region Saving
	public static List<WaveData.ShipData> GetShipDataList()
	{
		List<WaveData.ShipData> list = new List<WaveData.ShipData>();
		WaveData.ShipData data;

		foreach (EditorPlaceholder ship in _instances)
		{
			data = new WaveData.ShipData();
			data.shipName = "Ship Name";
			data.path = ship.GetPath();
			list.Add(data);
		}

		return list;
	}
	#endregion

	#region Play mode
	public void Play()
	{
		_originalPosition = transform.position;
		_currentNodeIndex = 0;

		StartCoroutine(MoveAlongPath());

		_path.gameObject.SetActive(false);
	}

	public void Stop()
	{
		StopAllCoroutines();
		transform.position = _originalPosition;
		_path.gameObject.SetActive(true);
	}

	public IEnumerator MoveAlongPath()
	{
		EditorNode currentNode = _nodes[_currentNodeIndex];

		float duration;
		Vector3[] destinations = new Vector3[_nodes.Count];
		for (int i = 0; i < _nodes.Count; i++)
			destinations[i] = _nodes[i].transform.position;

		while (_currentNodeIndex < _nodes.Count)
		{
			currentNode = _nodes[_currentNodeIndex];
			duration = Vector3.Distance(transform.position, destinations[_currentNodeIndex]) / currentNode.speed;
			yield return StartCoroutine(transform.MoveTo(destinations[_currentNodeIndex], duration, Ease.Linear));

			// wait if necessary
			yield return new WaitForSeconds(currentNode.waitTime);

			_currentNodeIndex ++;
		}
	}
	#endregion

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

	public int GetIndexOfNode(EditorNode node)
	{
		return _nodes.IndexOf(node);
	}

	public List<Vector3> GetPath()
	{
		List<Vector3> data = new List<Vector3>();
		foreach (EditorNode node in _nodes)
			data.Add(node.transform.position);
		return data;
	}
}
