using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelEditorManager : MonoBehaviour
{
	public static LevelEditorManager instance;
	public EditorPlaceholder enemyPlaceholder;

	// EditorNode varibles
	public Transform nodeEditPanel;
	private EditorNode _currentNode;

	private List<EditorPlaceholder> _ships;

	void Awake()
	{
		if (instance == null) instance = this;
		else if (instance != this) Destroy(gameObject);
	}

	void Start()
	{
		_ships = new List<EditorPlaceholder>();
	}

	public void AddShip()
	{
		Transform placeholder = Instantiate(enemyPlaceholder.transform);
		placeholder.position = new Vector2(30, 0);
		_ships.Add(placeholder.GetComponent<EditorPlaceholder>());
	}

	#region EditorNode handling

	public void ShowNodePanel(EditorNode node)
	{
		_currentNode = node;
		_currentNode.GetComponent<SpriteRenderer>().color = Color.green;

		nodeEditPanel.gameObject.SetActive(true);

		//NodeEditPanel.GetComponent<RectTransform>().anchoredPosition = Camera.main.WorldToScreenPoint(node.transform.position);
		nodeEditPanel.transform.position = Camera.main.WorldToScreenPoint(node.transform.position) + new Vector3(0, 130, 0);
	}

	public void HideNodePanel()
	{
		if (_currentNode != null)
		{
			_currentNode.GetComponent<SpriteRenderer>().color = Color.white;
			_currentNode = null;
		}

		nodeEditPanel.gameObject.SetActive(false);
	}

	public void AddNodeBefore()
	{
		_currentNode.AddNodeBefore();
	}

	public void AddNodeAfter()
	{
		_currentNode.AddNodeAfter();
	}

	#endregion
}
