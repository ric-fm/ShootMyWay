/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	Rigidbody2D rb;

	public float lifeTime;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	public void Shoot(Vector2 direction, float speed)
	{
		rb.velocity = direction * speed;
		StartCoroutine(Destroy());
	}

	IEnumerator Destroy()
	{
		yield return new WaitForSeconds(lifeTime);

		Destroy(gameObject);
	}
}
