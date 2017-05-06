using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCube : MonoBehaviour 
{
	public void TurnWhite ()
	{
		GetComponent<Renderer>().material.color = Color.white;
	}

	public void TurnBlack ()
	{
		GetComponent<Renderer>().material.color = Color.black;
	}

	void OnMouseOver ()
	{
		if (Input.GetMouseButtonDown(0))
		{
			
		}
	}
}
