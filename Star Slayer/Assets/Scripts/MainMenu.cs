using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public void StartGame()
	{
		Application.LoadLevel("Game");
	}

	public void StartEditor()
	{
		Application.LoadLevel("LevelEditor");
	}
}
