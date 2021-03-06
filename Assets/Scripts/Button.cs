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

	public bool locked;

	public bool DestroyOnUse = false;

	private void Start()
	{
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag != "Player" && collision.collider.tag != "Helmet")
		{
			return;
		}

		animator.SetTrigger("Press");

		GameManager.Instance.playerController.AddVelocity(impulse, transform.up);

		if (!locked)
		{
			GameManager.Instance.Slow(0.4f, 0.2f);
			SoundManager.Instance.PlaySingle(audioSource, pressSound);
			foreach (Logic logic in logics)
			{
				logic.Activate();
			}
		}
		if(DestroyOnUse)
		{
			GameObject.Destroy(gameObject);
		}

	}
}
