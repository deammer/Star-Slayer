using UnityEngine;
using System.Collections;

public class EditorNode : MonoBehaviour
{
	public Transform previousNode;
	public Transform nextNode;

	private bool _mouseDown = false;
	private Vector3 _positionBeforeDrag;
	private Vector3 _originalPosition;
	private Vector3 _originalScale;

	private EditorPlaceholder _parent;

	void Start()
	{
		// find the _parent that has an EditorPlaceholder component
		bool reachedTop = false;
		Transform test = transform.parent;
		while (!reachedTop && test != null)
		{
			if ((_parent = test.GetComponent<EditorPlaceholder>()) != null)
				reachedTop = true;
			else
				test = test.parent;
		}

		if (_parent == null)
			Debug.LogError ("This node's parent is null :(");
	}

	public void AddNodeBefore()
	{
		Transform t = Instantiate(_parent.nodePrefab.transform);
		Vector3 position = (previousNode.position + transform.position) * .5f;
		t.position = position;

		t.SetParent(transform.parent);

		EditorNode newNode = t.GetComponent<EditorNode>();
		newNode.nextNode = this.transform;
		newNode.previousNode = this.previousNode;
		this.previousNode = t;

		// tell the parent that we added a node
		_parent.NodeCreated(newNode, transform.GetSiblingIndex());
	}

	public void AddNodeAfter()
	{

	}

	private void OnClick()
	{
		LevelEditorManager.instance.ShowNodePanel(this);
	}

	void OnMouseDown()
	{
		_originalPosition = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
		_originalScale = transform.localScale;
		transform.localScale *= 1.3f;

		_mouseDown = true;

		_positionBeforeDrag = transform.position;

		LevelEditorManager.instance.HideNodePanel();
	}

	void OnMouseUp()
	{
		if (_mouseDown)
		{
			_mouseDown = false;

			// reset the visuals
			transform.localScale = _originalScale;

			// if it was a drag event
			bool dragged = _positionBeforeDrag != transform.position;
			if (dragged)
			{
				// update the parent EditorPlaceholder
				if (_parent != null)
					_parent.NodeDropped(this);
			}
			else // if it was a click event
			{
				OnClick();
			}
		}
	}

	void OnMouseDrag()
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + _originalPosition;
		transform.position = curPosition;

		if (_parent != null)
			_parent.NodeMoved(this);
	}
}
