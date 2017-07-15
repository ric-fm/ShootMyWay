/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCanon : Weapon {

	[HideInInspector]
	public Transform target;

	bool shoot = false;

	public float shotSpeed;

	public override void Shoot()
	{
		shoot = true;
	}

	private void LateUpdate()
	{
		if(shoot)
		{
			shoot = false;

			GameObject bulletGo = Instantiate(bulletTemplate, shootPoint.position, transform.rotation);

			Missile bullet = bulletGo.GetComponent<Missile>();

			bullet.target = target;

			bullet.Shoot(transform.up, shotSpeed);
		}
	}
}
