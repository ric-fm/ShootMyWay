/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Bullet {

	public Transform target;

	public float detectTargetInterval;

	public bool alive = true;

	public float turnSpeed;

	public float speed;
	public float homingSensitivity;

	Vector2 direction;

	public float noiseFactor = 10;
	public float noiseDegrees = 15.0f;

	public override void Shoot(Vector2 direction, float speed)
	{
		this.direction = direction;
		this.speed = speed;
	}

	private void Update()
	{
		Deviate();

		rb.velocity = direction * speed * Time.deltaTime;
	}

	void Deviate()
	{
		float rand_angle = Random.Range(-noiseDegrees, noiseDegrees);

		transform.Rotate( new Vector3(0, 0, rand_angle * noiseFactor * Time.deltaTime));
		direction = transform.up;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag.Equals("Player"))
		{
			Health health = collision.collider.gameObject.GetComponent<Health>();
			health.Hit(damage);
			StopAllCoroutines();
		}

		Destroy(gameObject);
	}
}
