/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


#region Movement
	public Vector2 MaxSpeed = new Vector2(20, 20);

	public Vector2 CurrentSpeed;

	public float ImpulseSpeed;

	Rigidbody2D rb;

	#endregion

#region Shoot

	public Transform shootPoint;
	public GameObject bulletTemplate;

	Vector2 targetPoint;

	public Weapon weapon;

	public CameraController screenShakeController;

	public float shakeMagnitudeOnShoot;
	public float shakeDurationOnShoot;

	EnemyRecord enemyRecord;

	#endregion

	Animator animator;


	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
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
		animator.SetTrigger("Shoot");

		Vector2 force = -direction * ImpulseSpeed * Time.deltaTime;
		//rb.AddForce(force , ForceMode2D.Impulse);
		rb.velocity = force;

		weapon.Shoot();


		screenShakeController.Shake(shakeMagnitudeOnShoot, shakeDurationOnShoot);
	}

	public void AddVelocity(float velocity, Vector2 direction)
	{
		rb.velocity = velocity * direction * Time.deltaTime;
	}
}
