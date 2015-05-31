using System;
using UnityEngine;
using System.Collections;

public class Controller3DPort : MonoBehaviour
{
    public const float ROTATE_SPEED = 15f;
    public float movementSpeed = 5f;

    public CNAbstractController MovementJoystick;
	public CNAbstractController ShootingJoystick;

    private Transform _playerTransform;

    void Start()
    {
		_playerTransform = GetComponent<Transform>();
	}

    
    // Update is called once per frame
    void Update()
    {
        var movement = new Vector3(
            MovementJoystick.GetAxis("Horizontal"),
            0f,
            MovementJoystick.GetAxis("Vertical"));

		var rotation = new Vector3(
			ShootingJoystick.GetAxis("Horizontal"),
			0f,
			ShootingJoystick.GetAxis("Vertical"));

		CommonMovementMethod(movement);
		CommonRotationMethod (rotation);
    }

	private void CommonMovementMethod(Vector3 movement)
	{
		_playerTransform.gameObject.GetComponent<PlayerScript> ().SetTarget (_playerTransform.position + (movement));
	}

	private void CommonRotationMethod(Vector3 rotation)
	{
		if (rotation != new Vector3 (0, 0, 0)) {
			FaceDirection(rotation);
			_playerTransform.gameObject.GetComponent<PlayerScript> ().Shoot ();
		} else {
			StopCoroutine("RotateCoroutine");
			_playerTransform.gameObject.GetComponent<PlayerScript> ().NotShoot ();
		}
	}

    public void FaceDirection(Vector3 direction)
    {
        StopCoroutine("RotateCoroutine");
        StartCoroutine("RotateCoroutine", direction);
    }

    IEnumerator RotateCoroutine(Vector3 direction)
    {
        if (direction == Vector3.zero) yield break;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        do
        {
			_playerTransform.rotation = Quaternion.Lerp(_playerTransform.rotation, lookRotation, 500f);
            yield return null;
        }
        while ((direction - _playerTransform.forward).sqrMagnitude > 0.2f);
    }

}
