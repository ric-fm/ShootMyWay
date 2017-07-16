/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{

	public bool destroyOnActivated = true;

	public List<Logic> logics;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			foreach (Logic logic in logics)
			{
				logic.Activate();
			}

			if (destroyOnActivated)
			{
				Destroy(gameObject);
			}
		}

	}
}
