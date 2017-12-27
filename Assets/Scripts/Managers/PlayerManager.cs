using System.Collections;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Windows.Speech;

public class PlayerManager : NetworkBehaviour
{

	[SerializeField] private const float MAX_HEALTH = 100f;
	[SerializeField] private Behaviour[] disableOnDeath;

	private bool[] wasEnabled;
	
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
		for (int i = 0; i < disableOnDeath.Length; i++)
		{
			disableOnDeath[i].enabled = false;
		}
		
		Collider collider = GetComponent<Collider>();

		if (collider != null)
		{
			collider.enabled = false;
		}
		//callrespawn

		StartCoroutine(Respawn());
	}

	private IEnumerator Respawn()
	{
		yield return new WaitForSeconds(3f);
		setDefaults();

		Transform spawnPoint = NetworkManager.singleton.GetStartPosition();
		transform.position = spawnPoint.position;
		transform.rotation = spawnPoint.rotation;
	}

	public void Setup()
	{
		wasEnabled = new bool[disableOnDeath.Length];
		for (int i = 0; i < wasEnabled.Length; i++)
		{
			wasEnabled[i] = disableOnDeath[i].enabled;
		}
		setDefaults();
	}

	private void setDefaults()
	{
		Debug.Log("Inside setDefaults.");
		_isDead = false;
		for (int i = 0; i < disableOnDeath.Length; i++)
		{
			disableOnDeath[i].enabled = wasEnabled[i];
		}

		Collider collider = GetComponent<Collider>();

		if (collider != null)
		{
			collider.enabled = true;
		}
		
		currentHealth = MAX_HEALTH;
		Debug.Log(transform.name + " now has " + currentHealth + " health");
	}
}
