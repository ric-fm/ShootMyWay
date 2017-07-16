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

	AudioSource audioSource;
	public AudioClip pressSound;

	private void Start()
	{
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag != "Player")
		{
			return;
		}

		GameManager.Instance.Slow(0.4f, 0.2f);

		SoundManager.Instance.PlaySingle(audioSource, pressSound);
		animator.SetTrigger("Press");

		PlayerController playerController = collision.collider.gameObject.GetComponent<PlayerController>();
		playerController.AddVelocity(impulse, transform.up);

		foreach (Logic logic in logics)
		{
			logic.Activate();
		}

	}
}
