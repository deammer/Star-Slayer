using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

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
		// make sure the directory exists
		if (!Directory.Exists(Path.Combine(Application.dataPath, "Levels")))
			Directory.CreateDirectory(Path.Combine(Application.dataPath, "Levels"));

		// save 1 file per level
		foreach (LevelData data in _levels)
		{
			var fileName = "Levels/Level_" + data.name + ".xml";
			var path = Path.Combine(Application.dataPath, fileName);
			var serializer = new XmlSerializer(typeof(LevelData));
			var stream = new FileStream(path, FileMode.OpenOrCreate);
			serializer.Serialize(stream, data);
			stream.Close();

			Debug.Log("Saved level at " + path);
		}
	}

	public void ShowLevelSettings(LevelData data)
	{
		levelSettingsPanel.SetActive(true);
		levelSettingsPanel.GetComponent<LevelSettingsPanel>().SetLevel(data);
		levelSettingsPanel.GetComponent<LevelSettingsPanel>().SetName(data.name);
	}
}
