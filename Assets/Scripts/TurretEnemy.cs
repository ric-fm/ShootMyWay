/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Enemy {

	public Transform target;

	public Transform pivot;

	public float CheckDistance;

	public float CheckInterval;

	public TurretCanon cannon;

	bool alive = true;

	protected override void Awake()
	{
		base.Awake();

		target = GameObject.FindObjectOfType<PlayerController>().transform;

		cannon.target = target;
	}

	protected override void Start()
	{
		base.Start();

		StartCoroutine(CheckTargetForShoot());
	}

	private void Update()
	{
		if(target != null && CanReachTarget())
		{
			Vector2 direction = (target.transform.position - pivot.position).normalized;
			pivot.up = direction;
		}
	}

	bool CanReachTarget()
	{
		return Vector2.Distance(target.position, pivot.position) < CheckDistance;
	}

	IEnumerator CheckTargetForShoot()
	{
		while(alive)
		{
			if(CanReachTarget())
			{
				cannon.Shoot();
			}
			yield return new WaitForSeconds(CheckInterval);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.gameObject.tag == "Player")
		{
			GameManager.Instance.playerController.AddVelocity(impulseOnContact, (GameManager.Instance.playerController.transform.position - transform.position).normalized);
			GameManager.Instance.playerController.Hit(damageOnContact);
			Kill();
		}
		if (collision.collider.gameObject.tag == "Helmet")
		{
			//GameManager.Instance.playerController.AddVelocity(impulseOnContact, -(GameManager.Instance.playerController.transform.position - transform.position).normalized);

			if(GameManager.Instance.playerController.CurrentStat != PlayerController.StatType.RANGE)
			{
				GameManager.Instance.playerController.Hit(damageOnContact);
			}
			else
			{
				GameManager.Instance.playerController.HitHelmet(damageOnContact);
			}
			Hit(1);
			//Kill();
		}
	}
}
