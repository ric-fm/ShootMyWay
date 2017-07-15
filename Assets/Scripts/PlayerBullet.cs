/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet {

	public float lifeTime;

	public override void Shoot(Vector2 direction, float speed)
	{
		rb.velocity = direction * speed;
		StartCoroutine(Destroy());
	}

	public void Shoot(Vector2 direction, float speed, float lifeTime)
	{
		this.lifeTime = lifeTime;
		rb.velocity = direction * speed;
		StartCoroutine(Destroy());
	}

	IEnumerator Destroy()
	{
		yield return new WaitForSeconds(lifeTime);

		Destroy(gameObject);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		switch(collision.collider.tag)
		{
			case "Enemy":
				Health health = collision.collider.gameObject.GetComponent<Health>();
				if(health.Hit(damage))
				{
					Enemy enemy = collision.collider.gameObject.GetComponent<Enemy>();
					GameManager.Instance.EnemyKilled(enemy);
				}
				break;
			case "EnemyBullet":
				Destroy(collision.collider.gameObject);
			break;
			case "Sparry":
				Sparry sparry = collision.collider.gameObject.GetComponent<Sparry>();
				sparry.Hit();
				break;
		}
		
		Destroy(gameObject);
	}
}
