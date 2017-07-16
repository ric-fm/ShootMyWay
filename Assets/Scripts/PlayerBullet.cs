/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{

	public float lifeTime;

	public float enemyImpulse;

	public AudioClip wallHitSound;
	public float wallSoundVolume = 1.0f;

	public AudioClip jailHitSound;
	public float jailSoundVolume = 1.0f;

	public AudioClip hitSparrySound;
	public float sparrySoundVolume = 1.0f;

	public AudioClip enemyHitSound;
	public float enemySoundVolume = 1.0f;

	public AudioClip bulletHitSound;
	public float bulletSoundVolume = 1.0f;


	public override void Shoot(Vector2 direction, float speed)
	{
		rb.velocity = direction * speed;
		transform.rotation = Quaternion.LookRotation(direction);
		StartCoroutine(Destroy());
	}

	public void Shoot(Vector2 direction, float speed, float lifeTime)
	{
		this.lifeTime = lifeTime;
		rb.velocity = direction * speed;
		//if (direction != Vector2.zero)
		//{
		//	float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		//	transform.rotation = Quaternion.AngleAxis(angle, Vector2.up);
		//}

		StartCoroutine(Destroy());
	}

	IEnumerator Destroy()
	{
		yield return new WaitForSeconds(lifeTime);

		Destroy(gameObject);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		switch (collision.collider.tag)
		{
			case "Enemy":
				Enemy enemy = collision.collider.gameObject.GetComponent<Enemy>();

				if (enemy.Hit(damage))
				{
					if (enemy.IsDead)
					{
						enemy.Kill();
						GameManager.Instance.EnemyKilled(enemy);
						GameManager.Instance.Slow(0.4f, 0.8f);

					}
					else
					{
						SoundManager.Instance.PlaySingleAtLocation(enemyHitSound, enemySoundVolume, transform.position);

						enemy.AddVelocity(enemyImpulse, transform.up);
						GameManager.Instance.Sleep(0.1f);
					}

				}
				break;
			case "EnemyBullet":
				Destroy(collision.collider.gameObject);
				SoundManager.Instance.PlaySingleAtLocation(bulletHitSound, bulletSoundVolume, transform.position);

				GameManager.Instance.Sleep(0.05f);
				break;
			case "Sparry":

				Sparry sparry = collision.collider.gameObject.GetComponent<Sparry>();
				GameManager.Instance.Sleep(0.1f);
				if (sparry.Hit())
				{
					SoundManager.Instance.PlaySingleAtLocation(hitSparrySound, sparrySoundVolume, transform.position);
				}
				break;
			case "Jail":
				SoundManager.Instance.PlaySingleAtLocation(jailHitSound, jailSoundVolume, transform.position);
				Destroy(collision.collider.gameObject);
				GameManager.Instance.Slow(0.6f, 0.2f);

				break;
			case "Wall":
				SoundManager.Instance.PlaySingleAtLocation(wallHitSound, wallSoundVolume, transform.position);

				break;
		}

		Destroy(gameObject);
	}

	public static IEnumerator WaitForRealTime(float delay)
	{
		while (true)
		{
			float pauseEndTime = Time.realtimeSinceStartup + delay;
			while (Time.realtimeSinceStartup < pauseEndTime)
			{
				yield return 0;
			}
			break;
		}
	}
}
