using UnityEngine;
using System.Collections;

public class LevelButton : MonoBehaviour
{
	public LevelsEditor parent;
	public LevelData levelData;

	void Awake()
	{
		levelData = new LevelData("New Level");
	}

	public void ShowLevelSettings()
	{
		parent.ShowLevelSettings(this.levelData);
	}
}
