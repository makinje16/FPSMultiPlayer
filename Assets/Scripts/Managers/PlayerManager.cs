using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Windows.Speech;

public class PlayerManager : NetworkBehaviour
{

	[SerializeField] private float maxHealth = 100;

	[SyncVar] private float currentHealth;

	[SyncVar] private bool _isDead = false;

	
	
	public bool isDead
	{
		get { return _isDead; }
		protected set { _isDead = true; }
	}

	[ClientRpc]
	public void RpcDealDamage(float damage)
	{
		if (isDead)
			return;
		
		
		currentHealth -= damage;
		Debug.Log(transform.name + " now has " + currentHealth + " health");

		if (currentHealth <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		isDead = true;
		
		//disable components;
		Debug.Log(transform.name + " is DEAD");
		
		
		//callrespawn
	}

	private void Awake()
	{
		setDefaults();
	}

	public void setDefaults()
	{
		currentHealth = maxHealth;
		Debug.Log(transform.name + " now has " + currentHealth + " health");
	}
}
