using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FileButton : MonoBehaviour
{
	[SerializeField]
	private Text nameLabel;
	[SerializeField]
	private Text dateLabel;

	public string fileName {
		get { return nameLabel.text; }
		set { nameLabel.text = value; } }
	public string date {
		get { return dateLabel.text; }
		set { dateLabel.text = value; } }

	public void OnClick()
	{

	}
}
