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

	private void Awake()
	{
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}

	private void Start()
	{
		cannon.target = target;

		StartCoroutine(CheckTargetForShoot());

	}

	private void Update()
	{
		if(target != null && CanReachTarget())
		{
			Vector2 direction = (target.transform.position - pivot.position).normalized;
			pivot.right = direction;
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
}
