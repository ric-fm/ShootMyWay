/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	protected Rigidbody2D rb;

	public int damage;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	public virtual void Shoot(Vector2 direction, float speed)
	{
	}
}
