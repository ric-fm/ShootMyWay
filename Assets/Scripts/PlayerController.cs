/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Vector2 MaxSpeed = new Vector2(20, 20);

	public Vector2 CurrentSpeed;

	public float ImpulseSpeed;

	public Transform shootPoint;
	public GameObject bulletTemplate;

	Rigidbody2D rb;

	Vector2 targetPoint;

	public Weapon weapon;


	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
	{
		Vector2 targetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		Vector2 direction = (targetPoint - (Vector2)transform.position).normalized;
		transform.right = direction;

		if (Input.GetButtonDown("Fire1") && weapon.CanShoot)
		{
			Fire(direction);
		}
		
	}


	void Fire(Vector2 direction)
	{
		Vector2 force = -direction * ImpulseSpeed * Time.deltaTime;
		//rb.AddForce(force , ForceMode2D.Impulse);
		rb.velocity = force;

		weapon.Shoot();
	}

	public void AddVelocity(float velocity, Vector2 direction)
	{
		rb.velocity = velocity * direction * Time.deltaTime;
	}
}
