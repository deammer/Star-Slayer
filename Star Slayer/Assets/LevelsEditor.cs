using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelsEditor : MonoBehaviour
{
	public static LevelsEditor instance;

	public GameObject levelButtonPrefab;
	public GameObject levelsContainer;
	public GameObject levelSettingsPanel;

	private List<LevelData> _levels = new List<LevelData>();

	void Awake()
	{
		if (instance == null) instance = this;
		else if (instance != this) Destroy(gameObject);
	}

	public void CreateNewLevel()
	{
		LevelData level = new LevelData("New Level");
		_levels.Add(level);

		GameObject button = Instantiate(levelButtonPrefab);
		button.GetComponent<LevelButton>().parent = this;
		button.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
		button.transform.SetParent(levelsContainer.transform, false);
	}

	public void SaveData()
	{

	}

	public void ShowLevelSettings(LevelData data)
	{
		levelSettingsPanel.SetActive(true);
		levelSettingsPanel.GetComponent<LevelSettingsPanel>().SetName(data.name);
	}
}
