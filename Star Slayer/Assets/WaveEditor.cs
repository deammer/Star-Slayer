using UnityEngine;
using System.Collections;

public class WaveEditor : MonoBehaviour
{
	public static WaveEditor instance;

	private WaveData _currentWave;

	void Awake()
	{
		if (instance == null) instance = this;
		else if (instance != this) Destroy(gameObject);

		gameObject.SetActive(false);
	}

	public void EditWave(WaveData wave)
	{
		_currentWave = wave;

		// hide the unnecessary panels
		LevelsEditor.instance.gameObject.SetActive(false);

		// show the wave editor
		gameObject.SetActive(true);
	}
}
