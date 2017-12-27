using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

	
	[SerializeField] private LayerMask mask;
	
	public PlayerWeapon weapon;
	public Transform shootPoint;
	
	private void Start()
	{

	}

	private void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			Shoot();
		}
	}

	[Client]
	private void Shoot()
	{
		RaycastHit hit;
		if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, weapon.range, mask))
		{
			if (hit.collider.tag == "Player")
			{
				CmdPlayerShot(hit.collider.name, weapon.damage);
			}
		}
	}

	[Command]
	public void CmdPlayerShot(string _playerID, float damage)
	{
		Debug.Log(_playerID + " has been shot and is taking " + damage + " damage.");
		var playerHit = GameHandler.getPlayer(_playerID);
		playerHit.RpcDealDamage(weapon.damage);
	}
}
