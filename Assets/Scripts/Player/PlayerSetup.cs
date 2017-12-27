using System;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerManager))]
public class PlayerSetup : NetworkBehaviour
{
	[SerializeField] private Behaviour[] componentsToDisable;

	private Camera sceneCamera;
	
	private void Start()
	{
		if (!isLocalPlayer)
		{
			for (int i = 0; i < componentsToDisable.Length; ++i)
			{
				componentsToDisable[i].enabled = false;
			}
		}
		else
		{
			sceneCamera = Camera.main;
			if (sceneCamera)
			{
				sceneCamera.gameObject.SetActive(false);
			}
			Camera.main.gameObject.SetActive(false);
		}

		GetComponent<PlayerManager>().Setup();
	}

	public override void OnStartClient()
	{
		base.OnStartClient();
		string _netID = GetComponent<NetworkIdentity>().netId.ToString();
		PlayerManager manager = GetComponent<PlayerManager>();
		GameHandler.registerPlayer(_netID, manager);
		GetComponent<PlayerManager>().Setup();
	}

	private void OnDisable()
	{
		if (sceneCamera != null)
		{
			sceneCamera.gameObject.SetActive(true);
		}
		GameHandler.unregisterPlayer(transform.name);
	}
}
