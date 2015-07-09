using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelData
{
	public string name;
	public List<WaveData> waves;

	public LevelData(string name)
	{
		this.name = name;
		waves = new List<WaveData>();
	}
}
