using System;
using UnityEngine;
using System.Collections;

public class Controller3DPort : MonoBehaviour
{
    public const float ROTATE_SPEED = 15f;

    public float movementSpeed = 5f;

    public CNAbstractController MovementJoystick;
	public CNAbstractController ShootingJoystick;

    private Transform _transformCache;
    private Transform _playerTransform;

    void Start()
    {
        // You can also move the character with an event system
        // You MUST CHOOSE one method and use ONLY ONE a frame
        // If you wan't the event based control, uncomment this line
        // MovementJoystick.JoystickMovedEvent += MoveWithEvent;

//		ShootingJoystick.JoystickMovedEvent += MoveWithEvent;
        _transformCache = GetComponent<Transform>();
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

		CommonMovementMethod(movement, rotation);
		CommonRotationMethod (rotation);
    }

    private void MoveWithEvent(Vector3 inputMovement)
    {
        var movement = new Vector3(
            inputMovement.x,
            0f,
            inputMovement.y);

		CommonRotationMethod (inputMovement);
    }

	private void CommonRotationMethod(Vector3 rotation)
	{
		//        movement = _mainCameraTransform.TransformDirection(movement);
		//        movement.y = 0f;
		//        movement.Normalize();
		
		//

		if (rotation != new Vector3 (0, 0, 0)) {
			FaceDirection(rotation);
			_playerTransform.gameObject.GetComponent<PlayerScript> ().Shoot ();
		} else {
			_playerTransform.gameObject.GetComponent<PlayerScript> ().NotShoot ();
		}
		//_playerTransform.gameObject.GetComponent<PlayerScript> ().SetTarget (_playerTransform.position + (movement));
		//_characterController.Move(movement * movementSpeed * Time.deltaTime);
	}

    private void CommonMovementMethod(Vector3 movement, Vector3 rotation)
    {
//        movement = _mainCameraTransform.TransformDirection(movement);
//        movement.y = 0f;
//        movement.Normalize();

		if (rotation == new Vector3 (0, 0, 0)) {
			//FaceDirection (movement);
		}

		_playerTransform.gameObject.GetComponent<PlayerScript> ().SetTarget (_playerTransform.position + (movement));
		//_characterController.Move(movement * movementSpeed * Time.deltaTime);
    }

    public void FaceDirection(Vector3 direction)
    {
        StopCoroutine("RotateCoroutine");
        StartCoroutine("RotateCoroutine", direction);
    }

    IEnumerator RotateCoroutine(Vector3 direction)
    {
		print ("Rotating");
        if (direction == Vector3.zero) yield break;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        do
        {
			_playerTransform.rotation = Quaternion.Lerp(_playerTransform.rotation, lookRotation, 500f);//Time.deltaTime * ROTATE_SPEED);
            yield return null;
        }
        while ((direction - _playerTransform.forward).sqrMagnitude > 0.2f);
    }

}
