/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{

	public enum StatType
	{
		NONE,
		POWER,
		COOLDOWN,
		RANGE
	}

	#region Movement
	public Vector2 MaxSpeed = new Vector2(20, 20);

	public Vector2 CurrentSpeed;

	public float ImpulseSpeed;

	Rigidbody2D rb;

	#endregion

	#region Shoot

	public ShotGun shotgun;

	public CameraController screenShakeController;

	public float shakeMagnitudeOnShoot;
	public float shakeDurationOnShoot;

	#endregion

	Animator animator;

	//SpriteRenderer sR;

	public List<SpriteRenderer> renderers;

	public StatType CurrentStat;

	public PlayerStats normalStats;
	public PlayerStats poweredStats;

	public PlayerStats currentStats;

	Color currentColor;

	public float changeColorFactor = 1.0f;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		//sR = GetComponent<SpriteRenderer>();

		currentStats = normalStats;

		currentColor = GameManager.Instance.noneColor;
		ChangeColor();
	}

	void Update()
	{
		Vector2 targetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		Vector2 direction = (targetPoint - (Vector2)transform.position).normalized;
		transform.right = direction;

		if (Input.GetButtonDown("Fire1") && shotgun.CanShoot)
		{
			Fire(direction);
		}

		//sR.color = currentColor;
		ChangeColor();

		shotgun.coolDownTime = currentStats.CoolDown;
		shotgun.range = currentStats.Range;
	}

	void Fire(Vector2 direction)
	{
		animator.SetTrigger("Shoot");

		//Vector2 force = -direction * ImpulseSpeed * Time.deltaTime;
		Vector2 force = -direction * currentStats.Power * Time.deltaTime;
		//rb.AddForce(force , ForceMode2D.Impulse);
		rb.velocity = force;

		shotgun.Shoot();


		screenShakeController.Shake(shakeMagnitudeOnShoot, shakeDurationOnShoot);
	}

	void ChangeColor()
	{
		foreach(SpriteRenderer renderer in renderers)
		{
			renderer.color = Color.Lerp(renderer.color, currentColor, changeColorFactor * Time.deltaTime);
		}
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
				ResetBoosts();
				break;

			case Enemy.ColorType.RED:
				SetPowerBoost();
				break;

			case Enemy.ColorType.GREEN:
				SetRangeBoost();
				break;

			case Enemy.ColorType.BLUE:
				SetCoolDownBoost();
				break;
		}
	}

	void ResetBoosts()
	{
		Debug.Log("Reset boost");

		CurrentStat = StatType.NONE;
		currentColor = GameManager.Instance.noneColor;


		currentStats.Power = normalStats.Power;
		currentStats.Range = normalStats.Range;
		currentStats.CoolDown = normalStats.CoolDown;
	}

	void SetPowerBoost()
	{
		Debug.Log("Power boost");
		CurrentStat = StatType.POWER;

		currentColor = GameManager.Instance.redColor;

		currentStats.Power = poweredStats.Power;
		currentStats.Range = normalStats.Range;
		currentStats.CoolDown = normalStats.CoolDown;
	}

	void SetRangeBoost()
	{
		Debug.Log("Range boost");

		CurrentStat = StatType.RANGE;

		currentColor = GameManager.Instance.greenColor;

		currentStats.Range = poweredStats.Range;
		currentStats.Power = normalStats.Power;
		currentStats.CoolDown = normalStats.CoolDown;
	}

	void SetCoolDownBoost()
	{
		Debug.Log("Cooldown boost");

		CurrentStat = StatType.COOLDOWN;

		currentColor = GameManager.Instance.blueColor;

		currentStats.CoolDown = poweredStats.CoolDown;
		currentStats.Power = normalStats.Power;
		currentStats.Range = normalStats.Range;
	}
}
