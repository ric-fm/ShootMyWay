/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedBullet : MonoBehaviour {

	public Transform target;

	public float detectTargetInterval;

	public bool alive = true;

	public float turnSpeed;

	public float speed;
	public float homingSensitivity;

	Vector2 direction;

	Rigidbody2D rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	public void Shoot(Vector2 direction, float speed)
	{
		StartCoroutine(FollowTarget());
	}

	private void Update()
	{
		// TODO: Mejorar comportamiento de misil al seguir al target
		direction = (target.transform.position - this.transform.position).normalized;

		float angle = Vector2.Angle(Vector2.right, direction);

		angle = direction.y < 0 ? -angle : angle;


		Vector3 relativePos = target.position - transform.position;
		Quaternion rotation = Quaternion.LookRotation(relativePos);

		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, homingSensitivity);

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
		//Destroy(gameObject);
	}
}
