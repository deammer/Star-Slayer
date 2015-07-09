using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSettingsPanel : MonoBehaviour
{
	public GameObject waveButtonPrefab;
	public GameObject waveContainer;

	private Text _panelName;
	private InputField _levelNameInput;

	private LevelData _level;

	void Awake()
	{
		_panelName = transform.Find("LevelName").GetComponent<Text>();
		_levelNameInput = transform.Find("NameInput").GetComponent<InputField>();
	}

	public void SetLevel(LevelData level)
	{
		_level = level;
	}

	public void SetName(string name)
	{
		_panelName.text = name;

		if (_levelNameInput.text != name)
			_levelNameInput.text = name;
	}

	public void AddWave()
	{
		GameObject button = Instantiate(waveButtonPrefab);
		button.transform.SetParent(waveContainer.transform);
		button.GetComponent<WaveButton>().waveData = new WaveData();
	}
}
