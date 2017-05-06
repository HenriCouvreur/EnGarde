using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour 
{
	public GameObject enemyPrefab;
	public int numberOfEnemies;

	public override void OnStartServer () //plays only on the server, when it starts
	{
		for (int i = 0; i<numberOfEnemies; i++)
		{
			Vector3 spawnPosition = new Vector3(Random.Range(-8.0f,8.0f), 0, Random.Range(-8.0f, 8.0f));
			Quaternion spawnRotation = Quaternion.Euler(0, Random.Range(0f, 180f), 0);

			GameObject enemy = (GameObject) Instantiate(enemyPrefab, spawnPosition, spawnRotation); //Instanciates the thing on the local client
			NetworkServer.Spawn(enemy); //Sends the enemy to the server, that will instanciate it to all clients
		}
	}
}
