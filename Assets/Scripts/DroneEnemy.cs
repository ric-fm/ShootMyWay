/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneEnemy : Enemy {

	public Transform target;

	Health health;

	Vector2 direction;

	Rigidbody2D rb;

	public float speed;

	public float homingSensitivity;

	public int damage;

	public float turnAngle;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		health = GetComponent<Health>();
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void Update ()
	{
		direction = (target.transform.position - this.transform.position).normalized;

		float angle = Vector2.Angle(Vector2.right, direction);

		angle = direction.y < 0 ? -angle : angle;

		Vector3 relativePos = target.position - transform.position;
		Quaternion rotation = Quaternion.LookRotation(relativePos);

		//transform.rotation = Quaternion.Slerp(transform.rotation, rotation, homingSensitivity);

		rb.velocity = direction * speed * Time.deltaTime;

		if(rb.velocity.x > 0)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,0,-turnAngle), homingSensitivity * Time.deltaTime);

		}
		else if(rb.velocity.x < 0)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, turnAngle), homingSensitivity * Time.deltaTime);

		}
		else
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), homingSensitivity * Time.deltaTime);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.gameObject.tag == "Player")
		{
			Health playerHealth = collision.collider.gameObject.GetComponent<Health>();
			playerHealth.Hit(damage);

			if(!health.godMode)
			{
				Destroy(gameObject);
			}
		}
	}
}
