using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WaveEditor : MonoBehaviour
{
	public static WaveEditor instance;

	public EditorPlaceholder enemyPlaceholder;

	// EditorPlaceholders
	private List<EditorPlaceholder> _ships;

	// EditorNode editor
	public Transform nodeEditPanel;
	private EditorNode _currentNode;

	private WaveData _currentWave;

	// editor controls
	private bool _isPlaying = false;

	public Transform shipContainer;

	void Awake()
	{
		if (instance == null) instance = this;
		else if (instance != this) Destroy(gameObject);

		gameObject.SetActive(false);
	}

	void Start()
	{
		_ships = new List<EditorPlaceholder>();

		foreach (Transform child in shipContainer)
			_ships.Add(child.GetComponent<EditorPlaceholder>());
	}

	public void EditWave(WaveData wave)
	{
		_currentWave = wave;

		// hide the unnecessary panels
		LevelsEditor.instance.gameObject.SetActive(false);

		// show the wave editor
		gameObject.SetActive(true);
	}

	public void AddShip()
	{
		Transform placeholder = Instantiate(enemyPlaceholder.transform);
		placeholder.position = new Vector2(30, 0);
		_ships.Add(placeholder.GetComponent<EditorPlaceholder>());
		placeholder.SetParent(shipContainer);
	}

	#region Editor controls
	
	public void Play()
	{
		_isPlaying = true;
		
		// update the UI
		ControlsMenu.instance.EnableStopButton(true);
		ControlsMenu.instance.EnablePlayButton(false);
		ControlsMenu.instance.EnablePauseButton(true);
		
		// launch the things
		foreach (EditorPlaceholder ship in _ships)
			ship.Play();
	}
	
	public void Pause()
	{
	}
	
	public void Stop()
	{
		_isPlaying = false;
		
		ControlsMenu.instance.EnableStopButton(false);
		ControlsMenu.instance.EnablePlayButton(true);
		ControlsMenu.instance.EnablePauseButton(false);
		
		
		// stop all the things
		foreach (EditorPlaceholder ship in _ships)
			ship.Stop();
	}
	
	public bool inPlayMode { get { return _isPlaying; } }
	
	#endregion
	
	#region EditorNode handling
	
	public void ShowNodePanel(EditorNode node)
	{
		_currentNode = node;
		_currentNode.GetComponent<SpriteRenderer>().color = Color.green;
		
		nodeEditPanel.gameObject.SetActive(true);
		
		// set the values of the panel
		nodeEditPanel.FindChild("WaitInput").GetComponent<InputField>().text = _currentNode.waitTime.ToString();
		nodeEditPanel.FindChild("SpeedInput").GetComponent<InputField>().text = _currentNode.speed.ToString();
		nodeEditPanel.FindChild("ShootWaitingToggle").GetComponent<Toggle>().isOn = _currentNode.shootWhileWaiting;
		nodeEditPanel.FindChild("ShootMovingToggle").GetComponent<Toggle>().isOn = _currentNode.shootWhileMoving;
		
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
	
	public void SetNodeWaitTime(string time)
	{
		if (_currentNode != null)
			_currentNode.waitTime = float.Parse(time);
	}
	
	public void SetNodeSpeed(string speed)
	{
		if (_currentNode != null)
			_currentNode.speed = int.Parse(speed);
	}
	
	public void SetNodeAttackWhileMoving(bool value)
	{
		if (_currentNode != null)
			_currentNode.shootWhileMoving = value;
	}
	
	public void SetNodeAttackWhileWaiting(bool value)
	{
		if (_currentNode != null)
			_currentNode.shootWhileWaiting = value;
	}
	
	#endregion
}
