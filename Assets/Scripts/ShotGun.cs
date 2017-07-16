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
	public float range;

	public int damage;

	Animator animator;

	AudioSource audioSource;
	public AudioClip shootSound;

	public float noiseDegrees;
	public float noiseFactor;
	public bool deviation = true;

	private void Start()
	{
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
		CanShoot = true;
	}

	public override void Shoot()
	{
		animator.SetTrigger("Shoot");




		for (int i = 0; i < bulletAmount; i++)
		{

			Vector2 shootDirection = transform.right;
			shootDirection = transform.right + transform.up * (i - bulletAmount / 2) * bulletSpread;

			if (deviation)
			{
				float rand_angle = Random.Range(-noiseDegrees, noiseDegrees);

				transform.Rotate(new Vector3(0, 0, rand_angle * noiseFactor * Time.deltaTime));
				shootDirection = transform.right;
			}

			float angle = -Mathf.Atan2(shootDirection.x, shootDirection.y) * Mathf.Rad2Deg;

			GameObject bulletGo = Instantiate(bulletTemplate, shootPoint.position, Quaternion.Euler(0, 0, angle));

			PlayerBullet bullet = bulletGo.GetComponent<PlayerBullet>();
			bullet.damage = damage;
			bullet.lifeTime = coolDownTime;

			SoundManager.Instance.PlaySingle(audioSource, shootSound);
			bullet.Shoot(shootDirection, bulletSpeed * Time.deltaTime, range);


			if (coolDownTime > 0)
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
