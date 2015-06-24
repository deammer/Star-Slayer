using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControlsMenu : MonoBehaviour
{
	public static ControlsMenu instance;

	private Button _playButton;
	private Button _pauseButton;
	private Button _stopButton;

	void Awake()
	{
		if (instance == null) instance = this;
		else if (instance != this) Destroy(gameObject);

		_playButton = transform.FindChild("PlayButton").GetComponent<Button>();
		_pauseButton = transform.FindChild("PauseButton").GetComponent<Button>();
		_stopButton = transform.FindChild("StopButton").GetComponent<Button>();
	}

	public void EnablePlayButton(bool enable) { _playButton.interactable = enable; }
	public void EnablePauseButton(bool enable) { _pauseButton.interactable = enable; }
	public void EnableStopButton(bool enable) { _stopButton.interactable = enable; }
}
