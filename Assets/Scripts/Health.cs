using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour  //that only works if the gameObject has a NetworkIdentity component
{
	public  const int maxHealth = 100;

	//SyncVar = this variable only exists on the server, and is updated on all clients at all time
	//Hook = the function to call when the syncvar changes -> if health changes, change healthbar on all clients.
	//The hook is called right before the value changes. It takes the new value as a parameter.
	[SyncVar (hook = "OnChangeHealth")] public int currentHealth = maxHealth;

	public RectTransform healthbar;
	public bool destroyOnDeath;
	private NetworkStartPosition[] spawnPoints;

	void Start()
	{
		if (isLocalPlayer)
			spawnPoints = FindObjectsOfType<NetworkStartPosition>();
	}

	public void TakeDamage(int amount)
	{
		//only the server can access this function
		if (!isServer)
			return;

		currentHealth -= amount;

		if (currentHealth <= 0)
		{
			if (destroyOnDeath)
				Destroy(gameObject);
			
			else
			{
				currentHealth = maxHealth;
				RpcSpawn();
			}
		}
	}

	void OnChangeHealth (int health)
	{
		healthbar.sizeDelta = new Vector2(health * 2, healthbar.sizeDelta.y);
	}

	[ClientRpc]
	void RpcSpawn () //has to start with "Rpc"
	{
		if (isLocalPlayer) //has to be initiated by the client, as NetworkTranform overrides server instructions
		{
			Vector3 spawnPoint = Vector3.zero;
			if (spawnPoints != null & spawnPoints.Length > 0)
				spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
			transform.position = spawnPoint;
		}
	}
}
