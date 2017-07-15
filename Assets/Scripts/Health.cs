/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public int life;

	public bool godMode;

	public float cooldownTime;

	public bool canHit = true;

	public bool IsDead
	{
		get
		{
			return life <= 0;
		}
	}

	Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	public void Hit(int damage)
	{
		if(godMode || !canHit || life <= 0)
		{
			return;
		}
		life -= damage;

		if (life <= 0)
		{
			return;
		}

		if(animator != null)
		{
			animator.SetTrigger("Hit");
		}

		StartCoroutine(CoolDown());
	}

	IEnumerator CoolDown()
	{
		canHit = false;
		yield return new WaitForSeconds(cooldownTime);
		canHit = true;
	}
}
