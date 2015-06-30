using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;

public class WaveIO
{
	private static string _data;
	
	// http://wiki.unity3d.com/index.php?title=Save_and_Load_from_XML
	
	private static string Serialize(WaveData file)
	{
		MemoryStream memStream = new MemoryStream();
		XmlSerializer xs = new XmlSerializer(typeof(WaveData));
		XmlTextWriter xmlWriter = new XmlTextWriter(memStream, Encoding.UTF8);
		
		xs.Serialize(xmlWriter, file);
		memStream = (MemoryStream)xmlWriter.BaseStream;
		
		UTF8Encoding encoding = new UTF8Encoding();
		string serialized = encoding.GetString(memStream.ToArray());
		return serialized;
	}
	
	public static void SaveXML(WaveData waveData)
	{
		StreamWriter writer;
		FileInfo info = new FileInfo("Assets\\Resources\\TestSaveWave.xml");
		
		if (info.Exists)
		{
			info.Delete();
			Debug.Log("The file already existed, so we wiped it.");
		}
		
		writer = info.CreateText();
		writer.Write(Serialize(waveData).ToString());
		writer.Close();

		Debug.Log("Finished writing to file.");
	}
	
	public static void LoadXML()
	{
		StreamReader reader = File.OpenText("Assets\\Resources\\TestSaveWave.xml");
		string info = reader.ReadToEnd();
		reader.Close();
		
		_data = info;
		
		Debug.Log("File loaded.");
	}
}