﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour 
{
	public GameObject BulletPrefab;
	public Transform BulletSpawn;

	void Update () 
	{
		if (!isLocalPlayer)
			return;

		if (Input.GetKeyDown(KeyCode.Space))
			CmdFire();

		float x = Input.GetAxis("Horizontal")*Time.deltaTime*150f;
		float z = Input.GetAxis("Vertical")*Time.deltaTime*3.0f;

		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);
	}

	[Command]
	void CmdFire()
	{
		GameObject bullet = (GameObject)Instantiate(BulletPrefab, BulletSpawn.position, BulletSpawn.rotation);
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward*6.0f;
		NetworkServer.Spawn(bullet);
		Destroy(bullet, 2);
	}

	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.blue;
	}
}
