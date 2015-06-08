using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveGenerator : MonoBehaviour
{
	public List<Wave> waves;
	
	private Wave _currentWave;
	public Wave currentWave { get { return _currentWave;} }
	
	private WaveAction _currentAction;
	public WaveAction currentAction { get { return _currentAction;} }
	
	IEnumerator SpawnLoop()
	{
		foreach(Wave wave in waves)
		{
			_currentWave = wave;
			foreach(WaveAction action in wave.actions)
			{
				_currentAction = action;
				if(action.delay > 0)
					yield return new WaitForSeconds(action.delay);
				
				if (action.prefab != null && action.spawnCount > 0)
				{
					//float xPos = Camera.main.ViewportToWorldPoint(new Vector3(1.1f, 0, 0)).x; // screen is 0,0 top-left to 1,1 bottom right. 1.1 is a bit more than the width of the screen
					Debug.Log("Spawning " + action.spawnCount + " " + action.prefab.name);
					Vector3 location;
					
					for (int i = 0; i < action.spawnCount; i++)
					{
						if (action.spawnCount == 1)
							location = Camera.main.ViewportToWorldPoint(new Vector3(1.1f, .5f, 0)); // center of the screen
						else if (action.spawnCount == 2)
							location = Camera.main.ViewportToWorldPoint(new Vector3(1.1f, .25f + i * .5f, 0)); // 1/4 and 3/4
						else
							location = Camera.main.ViewportToWorldPoint(new Vector3(1.1f, .1f + i * .8f / (action.spawnCount - 1), 0));
						Instantiate(action.prefab, new Vector3(location.x, location.y, 0), Quaternion.identity);
					}
				}
			}
			yield return null;  // prevents crash if all delays are 0
		}
		
		yield return null;  // prevents crash if all delays are 0
		
		Debug.Log("Spawn loop complete.");
	}
	
	void OnGUI()
	{
		//		string text = "Wave = " + _currentWave.name + "\nAction = " + _currentAction.name;
		//		GUI.TextField(new Rect(0, 0, Screen.width * .2f, Screen.height * .2f), text);
		//
		//		if (_currentAction != null)
		//		{
		//			if (_currentAction.message != "")
		//			{
		//				float size = Screen.width * .08f;
		//				Vector2 offset = new Vector2(.5f, .5f);
		//				guiText.pixelOffset = new Vector2(offset.x * Screen.width, offset.y * Screen.height);
		//				guiText.fontSize = (int)size;
		//			}
		//			guiText.text = _currentAction.message;
		//		}
	}
	
	void Start()
	{
		StartCoroutine(SpawnLoop());
	}
	
}

[System.Serializable]
public class WaveAction
{
	public string name;
	public float delay;
	public Transform prefab;
	public int spawnCount;
	public string message;
}

[System.Serializable]
public class Wave
{
	public string name;
	public List<WaveAction> actions;
}