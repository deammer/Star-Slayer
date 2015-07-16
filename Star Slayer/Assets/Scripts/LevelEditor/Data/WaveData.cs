using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Wave file, for serialization.
/// </summary>
using System.Xml.Serialization;

[XmlRoot("WaveData")]
public class WaveData
{
	public WaveData() {}

	[XmlElement("Name")]
	public string name;

	[XmlArray("ShipDataList")]
	[XmlArrayItem("ShipData")]
	public List<ShipData> shipData;

	public struct ShipData
	{
		public string shipName;
		public Vector3 startLocation;
		public List<PathNode> pathNodes;
	}

	public struct PathNode
	{
		public Vector3 location;
		public bool shootWhileWaiting;
		public bool shootWhileMoving;
		public int speed;
		public float waitTime;

		public void SetFromEditorNode(EditorNode node)
		{
			location = node.transform.position;
			shootWhileWaiting = node.shootWhileWaiting;
			shootWhileMoving = node.shootWhileMoving;
			speed = node.speed;
			waitTime = node.waitTime;
		}
	}
}