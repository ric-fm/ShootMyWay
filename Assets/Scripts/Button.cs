/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
	public float impulse;

	public List<Logic> logics;

	Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag != "Player")
		{
			return;
		}

		animator.SetTrigger("Press");

		PlayerController playerController = collision.collider.gameObject.GetComponent<PlayerController>();
		playerController.AddVelocity(impulse, transform.up);

		foreach (Logic logic in logics)
		{
			logic.Activate();
		}
	}
}
