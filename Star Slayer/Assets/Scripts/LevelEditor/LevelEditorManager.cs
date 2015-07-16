using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelEditorManager : MonoBehaviour
{
	public static LevelEditorManager instance;

	void Awake()
	{
		if (instance == null) instance = this;
		else if (instance != this) Destroy(gameObject);
	}

	#region Saving
	public void Save()
	{
		/*WaveData waveData = new WaveData();
		waveData.name = "First Wave";
		waveData.shipData = EditorPlaceholder.GetShipDataList();

		WaveIO.SaveXML(waveData);*/

		LevelsEditor.instance.SaveData();
	}
	#endregion
}
