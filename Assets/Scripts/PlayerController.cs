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

	PlayerStats stats;

	public int killedStatAmount = 1;


	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		stats = new PlayerStats();
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


	public void EnemyKilled(Enemy enemy)
	{
		switch (enemy.colorType)
		{
			case Enemy.ColorType.NONE:
				break;

			case Enemy.ColorType.RED:
				stats.IncreasePowerCount(killedStatAmount);
				stats.DecreaseCooldownCount(killedStatAmount);
				stats.DecreaseRangeCount(killedStatAmount);
				break;

			case Enemy.ColorType.GREN:
				stats.IncreaseRangeCount(killedStatAmount);
				stats.DecreasePowerCount(killedStatAmount);
				stats.DecreaseCooldownCount(killedStatAmount);
				break;

			case Enemy.ColorType.BLUE:
				stats.IncreaseCooldownCount(killedStatAmount);
				stats.DecreasePowerCount(killedStatAmount);
				stats.DecreaseRangeCount(killedStatAmount);
				break;
		}
	}
}
