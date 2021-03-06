/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public enum ColorType
	{
		NONE,
		RED,
		BLUE,
		GREEN
	}

	public ColorType colorType;

	SpriteRenderer sR;

	public GameObject explosionTemplate;

	public float shakeMagnitudeOnDie;
	public float shakeDurationOnDie;

	Animator animator;

	Health health;

	public int damageOnContact;

	public float impulseOnContact;

	public AudioClip deadSound;
	public float deadSoundVolume = 1.0f;

	public bool IsDead
	{
		get
		{
			return health.IsDead;
		}
	}

	protected virtual void Awake()
	{
		sR = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		health = GetComponent<Health>();
	}

	protected virtual void Start()
	{
		ChangeColorType(colorType);
	}

	void ChangeColor(Color color)
	{
		sR.color = color;
	}

	public void ChangeColorType(ColorType colorType)
	{
		this.colorType = colorType;
		switch (colorType)
		{
			case Enemy.ColorType.NONE:
				ChangeColor(GameManager.Instance.noneColor);
				break;

			case Enemy.ColorType.RED:
				ChangeColor(GameManager.Instance.redColor);
				break;

			case Enemy.ColorType.GREEN:
				ChangeColor(GameManager.Instance.greenColor);
				break;

			case Enemy.ColorType.BLUE:
				ChangeColor(GameManager.Instance.blueColor);
				break;
		}
	}

	public virtual void Kill()
	{
		GameManager.Instance.EnemyDestroyed(this);

		GameObject explosionGO = GameObject.Instantiate(explosionTemplate, transform.position, Quaternion.identity);

		explosionGO.GetComponent<DestroyOnAnimationEnd>().Explode(sR.color);

		GameManager.Instance.ShakeScreen(shakeMagnitudeOnDie, shakeDurationOnDie, transform.position);

		if(deadSound != null)
		{
			SoundManager.Instance.PlaySingleAtLocation(deadSound, deadSoundVolume, transform.position);
		}

		Destroy(gameObject);
	}

	public virtual bool Hit(int damage)
	{
		if(health.canHit)
		{
			health.Hit(damage);
			//if(!health.IsDead)
			//{
			//	//animator.SetTrigger("Hit");
			//}
			return true;
		}
		return false;
	}

	public virtual void AddVelocity(float velocity, Vector2 direction)
	{

	}

	//private void OnDestroy()
	//{
	//	//GameManager.Instance.EnemyDestroyed(this);
	//}
}
