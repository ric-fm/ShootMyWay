/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
	public bool oneUse = false;
	bool used = false;
	public bool deactivate = true;

	bool activated = false;
	public float delayTime;
	public float impulse;

	public List<Logic> logics;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(oneUse && used || activated)
		{
			return;
		}

		if (collision.collider.tag == "Player")
		{
			used = true;
			activated = true;

			PlayerController playerController = collision.collider.gameObject.GetComponent<PlayerController>();

			playerController.AddVelocity(impulse, transform.up);

			foreach (Logic logic in logics)
			{
				logic.Activate(this);
			}

			if(deactivate)
			{
				StartCoroutine(DeactivateOnDelay());
			}
		}
	}

	IEnumerator DeactivateOnDelay()
	{
		yield return new WaitForSeconds(delayTime);

		foreach(Logic logic in logics)
		{
			logic.Deactivate(this);
		}
		activated = false;
	}
}
