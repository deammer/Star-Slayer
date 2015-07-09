using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Item
{
	public string fileName;
	public string lastEdit;
}

public class LoadMenuFileList : MonoBehaviour
{
	public Transform contentPanel;
	public GameObject itemPrefab;

	private List<Item> _itemList;

	void Start()
	{
		CreateListOfFiles();
		PopulateList();
	}

	private void CreateListOfFiles()
	{
		print (Application.persistentDataPath);
		print (Application.dataPath);
	}

	private void PopulateList()
	{
		if (_itemList == null) return;

		foreach (var item in _itemList)
		{
			GameObject newItem = Instantiate(itemPrefab);
			FileButton button = newItem.GetComponent<FileButton>();
			button.date = item.lastEdit;
			button.fileName = item.fileName;
			button.transform.SetParent(contentPanel);
		}
	}
}
