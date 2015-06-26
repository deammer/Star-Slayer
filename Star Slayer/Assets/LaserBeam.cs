using UnityEngine;
using System.Collections;

public class LaserBeam : MonoBehaviour
{
	[Header("Laser Parts")]
	public GameObject laserStart;
	public GameObject laserMiddle;
	public GameObject laserEnd;

	private float _maxSize = 70f;
	
	// Update is called once per frame
	void Update ()
	{
		// raycast to the right
		Vector2 laserDirection = transform.right;
		RaycastHit2D hit = Physics2D.Raycast(transform.position, laserDirection, _maxSize);

		var actualLaserSize = _maxSize;

		if (hit.collider !=  null)
		{
			actualLaserSize = Vector2.Distance(hit.point, transform.position);

			Enemy enemy;
			if ((enemy = hit.transform.GetComponent<Enemy>()) != null)
				enemy.Damage(Time.deltaTime * 5f);
		}

		laserMiddle.transform.localScale = new Vector3(actualLaserSize, laserMiddle.transform.localScale.y, laserMiddle.transform.localScale.z);
		laserEnd.transform.localPosition = new Vector2(actualLaserSize, 0f);
	}
}
