/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySensible : MonoBehaviour {

	Rigidbody2D rb;

	float initialGravityScale;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();

		initialGravityScale = rb.gravityScale;
	}

	public void SetGravityScale(float value)
	{
		rb.gravityScale = value;
	}

	public void RestoreGravityScale()
	{
		rb.gravityScale = initialGravityScale;
	}
}
