/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneEnemy : Enemy {

	public Transform target;

	Vector2 direction;

	Rigidbody2D rb;

	public float speed;

	public float homingSensitivity;

	public int damage;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void Update ()
	{
		direction = (target.transform.position - this.transform.position).normalized;

		float angle = Vector2.Angle(Vector2.right, direction);

		angle = direction.y < 0 ? -angle : angle;

		Vector3 relativePos = target.position - transform.position;
		Quaternion rotation = Quaternion.LookRotation(relativePos);

		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, homingSensitivity);

		rb.velocity = direction * speed * Time.deltaTime;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.gameObject.tag == "Player")
		{
			Health health = collision.collider.gameObject.GetComponent<Health>();
			health.Hit(damage);
			Destroy(gameObject);
		}
	}
}
