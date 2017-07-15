/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCanon : Weapon {

	[HideInInspector]
	public Transform target;

	public override void Shoot()
	{
		GameObject bulletGo = Instantiate(bulletTemplate, shootPoint.position, Quaternion.Euler(transform.right));

		Missile bullet = bulletGo.GetComponent<Missile>();

		bullet.target = target;

		bullet.Shoot(transform.right, 0);
	}
}
