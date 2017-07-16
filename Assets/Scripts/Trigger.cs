/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

	public bool destroyOnActivated = true;

	public Logic logic;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("Trigger");
		if (collision.gameObject.tag == "Player")
		{
			Debug.Log("Trigger with player");
			logic.Activate();

			if (destroyOnActivated)
			{
				Destroy(gameObject);
			}
		}

	}
}
