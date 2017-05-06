﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour 
{
	public  const int maxHealth = 100;
	public int currentHealth = maxHealth;
	public RectTransform healthbar;

	public void TakeDamage(int amount)
	{
		currentHealth -= amount;

		if (currentHealth <= 0)
		{
			currentHealth = 0;
			Debug.Log("Dead");
		}

		healthbar.sizeDelta = new Vector2(currentHealth*2, healthbar.sizeDelta.y);
	}
}
