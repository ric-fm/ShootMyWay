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
				health.Hit(damage);
				break;
			case "EnemyBullet":
				Destroy(collision.collider.gameObject);
			break;
		}
		
		Destroy(gameObject);
	}
}
