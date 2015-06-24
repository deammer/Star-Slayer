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
		string shipName;
		public List<Vector3> path;
	}
}