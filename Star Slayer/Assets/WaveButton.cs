using UnityEngine;
using System.Collections;

public class WaveButton : MonoBehaviour
{
	public WaveData waveData { get { return _waveData; } set { _waveData = value; UpdateButtonText(); } }
	private WaveData _waveData;

	[SerializeField]
	private UnityEngine.UI.Text buttonText;

	void Awake()
	{
		_waveData = new WaveData();
	}

	private void UpdateButtonText()
	{
		if (_waveData != null)
			buttonText.text = "Wave: " + _waveData.name;
	}

	public void Activate()
	{
		WaveEditor.instance.EditWave(_waveData);
	}
}
