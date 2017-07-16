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
					}
					else
					{
						enemy.AddVelocity(enemyImpulse, transform.up);
					}

					GameManager.Instance.Sleep(0.1f);
				}
				break;
			case "EnemyBullet":
				Destroy(collision.collider.gameObject);
				GameManager.Instance.Sleep(0.05f);
				break;
			case "Sparry":
				Sparry sparry = collision.collider.gameObject.GetComponent<Sparry>();
				GameManager.Instance.Sleep(0.1f);

				sparry.Hit();
				break;
			case "Jail":
				Destroy(collision.collider.gameObject);
				GameManager.Instance.Slow(0.6f, 0.2f);

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
