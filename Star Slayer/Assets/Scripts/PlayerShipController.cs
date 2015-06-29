using UnityEngine;
using System.Collections;

public class PlayerShipController : MonoBehaviour
{
	public Transform weapon;
	private Weapon _weaponController;

	public int health = 100;
	public int shield = 50;
	public int energy = 100;

	public int shieldRegen = 2;

	private float _currentHealth, _currentShield, _currentEnergy;
	public float healthRatio { get { return _currentHealth == 0 ? 0 : _currentHealth / (float) health; } }
	public float shieldRatio { get { return _currentShield == 0 ? 0 : _currentShield / (float) shield; } }
	public float energyRatio { get { return _currentEnergy == 0 ? 0 : _currentEnergy / (float) energy; } }

	public Vector2 direction { get { return _direction; } }
	private Vector2 _direction = new Vector2();
	[Header("Movement")]
	public float moveSpeed = 25f;
	public bool allowRotation = true;
	public float maxRotation = 15f;

	private Vector3 screenSW;
	private Vector3 screenNE;
	
	private GameObject _shieldObject;
	private float _shieldFadeTime = .25f;
	private bool _shieldFading = false;

	void Start ()
	{
		// find the Shield gameobject
		//_shieldObject = transform.Find("Shield").gameObject;
		//_shieldObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);

		// bottom left
		screenSW = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.localPosition.z));
		// top right
		screenNE = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.localPosition.z));

		_currentHealth = health;
		_currentShield = shield;
		_currentEnergy = energy;

		if (weapon != null)
			_weaponController = weapon.GetComponent<Weapon> ();
	}

	void Update ()
	{
		_direction.x = Input.GetAxis("Horizontal");
		_direction.y = Input.GetAxis("Vertical");

		// override if keyboard input
		if (Input.GetKeyDown (KeyCode.A))
			_direction.x = -1f;
		if (Input.GetKeyDown (KeyCode.D))
			_direction.x = 1f;
		if (Input.GetKeyDown (KeyCode.W))
			_direction.y = 1f;
		if (Input.GetKeyDown (KeyCode.S))
			_direction.y = -1f;
		
		Vector3 moveDirection = new Vector3(_direction.x, _direction.y, 0).normalized * moveSpeed * Time.deltaTime;
		transform.Translate( moveDirection);

		// keep the ship within bounds
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, screenSW.x, screenNE.x),
		                                 Mathf.Clamp(transform.position.y, screenSW.y, screenNE.y),
		                                 transform.position.z);
		if (!_shieldFading)
			_currentShield = Mathf.Min(shield, _currentShield + shieldRegen * Time.deltaTime);

		// rotate the ship
		if (allowRotation)
		{
			if (_direction.y == 0f)
				transform.rotation = Quaternion.identity;
			else
				transform.rotation = Quaternion.AngleAxis(direction.y * maxRotation, Vector3.forward);
		}
	}

	public void Damage(int amount, Transform other)
	{
		// animate the shield
		if (_currentShield > 0)
			ShowShield(other);

		// decrement the shield
		float leftOver = _currentShield - amount;
		_currentShield -= amount;
		if (_currentShield < 0) _currentShield = 0;

		// decrement the health
		if (leftOver < 0)
			_currentHealth -= Mathf.Abs(leftOver);
	}

	private void ShowShield(Transform other)
	{
		Vector3 direction = other.position - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		_shieldObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		_shieldObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
		if (!_shieldFading)
			StartCoroutine("FadeShield");
	}

	IEnumerator FadeShield()
	{
		_shieldFading = true;
		while (_shieldObject.GetComponent<SpriteRenderer>().color.a > 0)
		{
			Color color = _shieldObject.GetComponent<SpriteRenderer>().color;
			color.a -= Time.deltaTime / _shieldFadeTime;
			_shieldObject.GetComponent<SpriteRenderer>().color = color;
			yield return null;
		}
		_shieldFading = false;
	}
}
