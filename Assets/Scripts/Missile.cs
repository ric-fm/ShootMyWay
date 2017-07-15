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

	public override void Shoot(Vector2 direction, float speed)
	{
		//StartCoroutine(FollowTarget());
	}

	private void Update()
	{
		// TODO: Mejorar comportamiento de misil al seguir al target
		direction = (target.transform.position - this.transform.position).normalized;

		float angle = Vector2.Angle(Vector2.up, direction);

		angle = direction.x > 0 ? -angle : angle;


		Vector3 relativePos = target.position - transform.position;
		Quaternion rotation = Quaternion.LookRotation(relativePos);

		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,0, angle), homingSensitivity * Time.deltaTime);

		rb.velocity = direction * speed * Time.deltaTime;
	}

	IEnumerator FollowTarget()
	{
		while(alive)
		{
			Debug.Log("detect");
			//direction = (target.transform.position - this.transform.position).normalized;
			//direction = transform.InverseTransformPoint(target.position);

			

			//transform.Rotate(Vector3.forward, turnSpeed * Time.deltaTime * angle);

			//var targetRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.right);
			//transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);


			//Vector3 relativePos = target.position - transform.position;
			//Quaternion rotation = Quaternion.LookRotation(relativePos);

			//transform.rotation = Quaternion.Slerp(transform.rotation, rotation, homingSensitivity);

			//transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);

			yield return new WaitForSeconds(detectTargetInterval);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag.Equals("Player"))
		{
			Health health = collision.collider.gameObject.GetComponent<Health>();
			health.Hit(damage);
			StopAllCoroutines();
			Destroy(gameObject);
		}
	}
}
