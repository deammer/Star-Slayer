using UnityEngine;
using System.Collections;

public class PathMover : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTweenPath path = GetComponent<iTweenPath>();
		if (path != null)
			iTween.MoveTo(gameObject, iTween.Hash("path", path.nodes.ToArray(), "time", 5, "easetype", iTween.EaseType.linear));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
