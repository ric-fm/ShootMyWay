/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Weapon
{

	public int bulletAmount;
	public float bulletSpread;
	public float bulletSpeed;
	public float bulletLifeTime;

	public float coolDownTime;

	public int damage;

	private void Start()
	{
		CanShoot = true;
	}

	public override void Shoot()
	{
		float angle = Vector2.Angle(Vector2.right, transform.right);

		for (int i = 0; i < bulletAmount; i++)
		{

			Vector2 shootDirection = transform.right;
			shootDirection = transform.right + transform.up * (i - bulletAmount / 2) * bulletSpread;


			GameObject bulletGo = Instantiate(bulletTemplate, shootPoint.position, Quaternion.Euler(0, 0, angle));

			PlayerBullet bullet = bulletGo.GetComponent<PlayerBullet>();
			bullet.damage = damage;
			bullet.lifeTime = bulletLifeTime;

			bullet.Shoot(shootDirection, bulletSpeed * Time.deltaTime);

			if(coolDownTime > 0)
			{
				StartCoroutine(CoolDown());
			}
		}
	}

	IEnumerator CoolDown()
	{
		CanShoot = false;
		yield return new WaitForSeconds(coolDownTime);

		CanShoot = true;
	}
}
