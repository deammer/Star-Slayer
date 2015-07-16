using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("MonsterCollection")]
public class LevelData
{
	[XmlElement("Name")]
	public string name;

	[XmlArray("Waves")]
	[XmlArrayItem("WaveData")]
	public List<WaveData> waves;

	public LevelData()
	{
		this.name = "New Level";
		waves = new List<WaveData>();
	}

	public LevelData(string name)
	{
		this.name = name;
		waves = new List<WaveData>();
	}
}
