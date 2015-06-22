using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelEditorManager : MonoBehaviour
{
	public EditorPlaceholder enemyPlaceholder;

	private List<EditorPlaceholder> _ships;

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
}
