using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{

	[SerializeField] private float speed = 5f;
	[SerializeField] private float lookSensitvity = 3f;
	
	private PlayerMovement _movement;

	private void Start()
	{
		_movement = GetComponent<PlayerMovement>();
		string ID = "Player " + GetComponent<NetworkIdentity>().netId;
		gameObject.name = ID;
	}

	private void Update()
	{
		//calculate movement velocity as 3d Vector

		float xMovement = Input.GetAxisRaw("Horizontal");
		float zMovement = Input.GetAxisRaw("Vertical");

		Vector3 moveHorizontal = transform.right * xMovement;
		Vector3 moveVertical = transform.forward * zMovement;

		Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;

		_movement.Move(velocity);
		
		//calculate rotation as a 3d vector for turning

		float _yRotation = Input.GetAxisRaw("Mouse X");

		Vector3 _rotation = new Vector3(0f, _yRotation, 0f) * lookSensitvity;
		
		//applyRoation

		_movement.Rotate(_rotation);

		//Camera rotation
		float xRotation = Input.GetAxisRaw("Mouse Y");

		Vector3 cameraRotation = new Vector3(xRotation, 0f, 0f) * lookSensitvity;

		_movement.RotateCamera(cameraRotation);

	}
}
