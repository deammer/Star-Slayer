using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Wave file, for serialization.
/// </summary>

public class WaveData
{
	public WaveData() {}
	
	public string name;

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
	}
}