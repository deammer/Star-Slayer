using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public float ScrollSpeed = 2f;

	void Awake()
	{
		if (instance == null) instance = this;
		else if (instance != this) Destroy(gameObject);
	}

}
