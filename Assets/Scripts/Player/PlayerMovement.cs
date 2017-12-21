using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{

	[SerializeField] private Camera cam;
	
	private Vector3 velocity = Vector3.zero;
	private Vector3 _rotation = Vector3.zero;
	private Vector3 _cameraRotation = Vector3.zero;
	
	private Rigidbody _rigidbody;

	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	public void Move(Vector3 _velocity)
	{
		velocity = _velocity;
	}

	private void FixedUpdate()
	{
		PerformMovement();
		PerformRotation();
	}

	void PerformMovement()
	{
		if (velocity != Vector3.zero)
		{
			_rigidbody.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
		}
	}

	public void Rotate(Vector3 rotation)
	{
		_rotation = rotation;
	}

	void PerformRotation()
	{
		_rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(_rotation));

		if (cam != null)
		{
			cam.transform.Rotate(_cameraRotation * -1);
		}
	}

	public void RotateCamera(Vector3 cameraRotation)
	{
		_cameraRotation = cameraRotation;
	}
	
	
}
