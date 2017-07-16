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

	public Transform tail;

	Health health;

	public int damageOnWallContact;
	public float impulseOnWallContact;

	public float maxVelocity;

	public List<GameObject> deadParts;

	public bool isDead;

	public AudioClip hitSound;
	public AudioClip helmetSound;
	public AudioClip deadSound;
	AudioSource source;

	public int poweredDamage;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		health = GetComponent<Health>();
		source = GetComponent<AudioSource>();
		//sR = GetComponent<SpriteRenderer>();

		currentStats = normalStats;

		currentColor = GameManager.Instance.noneColor;
		ChangeColor();
	}

	bool fired = false;
	Vector2 shootDirection;

	void Update()
	{
		//if(Time.timeScale == 0.0f)
		//{
		//	return;
		//}
		if (isDead)
		{
			return;
		}
		Vector2 targetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		shootDirection = (targetPoint - (Vector2)transform.position).normalized;
		transform.right = shootDirection;

		if (Input.GetButtonDown("Fire1") && shotgun.CanShoot && !fired)
		{
			fired = true;

		}

		//sR.color = currentColor;
		ChangeColor();

		shotgun.coolDownTime = currentStats.CoolDown;
		shotgun.range = currentStats.Range;

		AnimateTail();
	}

	private void FixedUpdate()
	{
		if (fired)
		{
			fired = false;
			Fire(shootDirection);
		}
		rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
	}

	void AnimateTail()
	{
		float angle = transform.rotation.eulerAngles.z;

		float speed = rb.velocity.magnitude;

		float sign = Mathf.Sign(transform.InverseTransformDirection(rb.velocity).x);

		angle += 0.1f * speed * sign;

		angle = Mathf.Lerp(tail.rotation.eulerAngles.z, angle, Time.deltaTime);

		tail.Rotate(new Vector3(0, 0, angle * Time.deltaTime));
	}

	void Fire(Vector2 direction)
	{
		animator.SetTrigger("Shoot");

		//Vector2 force = -direction * ImpulseSpeed * Time.deltaTime;
		Vector2 force = -direction * currentStats.Power * Time.fixedDeltaTime/* * Time.deltaTime*/;
		//rb.AddForce(force , ForceMode2D.Impulse);
		rb.velocity = force;

		shotgun.Shoot();


		GameManager.Instance.ShakeScreen(shakeMagnitudeOnShoot, shakeDurationOnShoot);
	}

	void ChangeColor()
	{
		foreach (SpriteRenderer renderer in renderers)
		{
			renderer.color = Color.Lerp(renderer.color, currentColor, changeColorFactor * Time.deltaTime);
		}
	}

	public void AddVelocity(float velocity, Vector2 direction)
	{
		rb.velocity = Vector2.ClampMagnitude(rb.velocity + velocity * direction * Time.deltaTime, maxVelocity);
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
		shotgun.color = currentColor;
	}

	public void SparryHit(Sparry sparry)
	{
		string infoText = "";
		switch (sparry.colorType)
		{
			case Enemy.ColorType.NONE:
				ResetBoosts();
				infoText = "RESET BOOSTS";
				break;

			case Enemy.ColorType.RED:
				SetPowerBoost();
				infoText = "SHOOT POWER";

				break;

			case Enemy.ColorType.GREEN:
				SetRangeBoost();
				infoText = "SHOOT RANGE + HELMET SHIELD";

				break;

			case Enemy.ColorType.BLUE:
				SetCoolDownBoost();
				infoText = "SHOOT FIRE RATE";

				break;
		}
		shotgun.color = currentColor;
		GameManager.Instance.ShowInfoText(infoText);
	}

	void ResetBoosts()
	{
		CurrentStat = StatType.NONE;
		currentColor = GameManager.Instance.noneColor;


		currentStats.Power = normalStats.Power;
		currentStats.Range = normalStats.Range;
		currentStats.CoolDown = normalStats.CoolDown;
	}

	void SetPowerBoost()
	{
		CurrentStat = StatType.POWER;

		currentColor = GameManager.Instance.redColor;

		currentStats.Power = poweredStats.Power;
		currentStats.Range = normalStats.Range;
		currentStats.CoolDown = normalStats.CoolDown;
	}

	void SetRangeBoost()
	{
		CurrentStat = StatType.RANGE;

		currentColor = GameManager.Instance.greenColor;

		currentStats.Range = poweredStats.Range;
		currentStats.Power = normalStats.Power;
		currentStats.CoolDown = normalStats.CoolDown;
	}

	void SetCoolDownBoost()
	{
		CurrentStat = StatType.COOLDOWN;

		currentColor = GameManager.Instance.blueColor;

		currentStats.CoolDown = poweredStats.CoolDown;
		currentStats.Power = normalStats.Power;
		currentStats.Range = normalStats.Range;
	}

	public bool wallInvulnerable;

	public virtual bool Hit(int damage)
	{
		if (wallInvulnerable)
		{
			return false;
		}
		if (health.canHit)
		{
			SoundManager.Instance.PlaySingle(source, hitSound);
			health.Hit(damage);
			GameManager.Instance.ShowLives();
			//if (!health.IsDead)
			//{
			//	//animator.SetTrigger("Hit");
			//}
			return true;
		}
		return false;
	}

	public virtual void HitHelmet(int damage)
	{
		SoundManager.Instance.PlaySingle(source, helmetSound);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "Wall")
		{
			bool onHelmet = false;
			if (collision.otherCollider.gameObject.tag == "Helmet")
			{
				if(CurrentStat == StatType.RANGE)
				{
					onHelmet = true;
					GameManager.Instance.playerController.HitHelmet(0);
				}
			}
			else
			{

			}

			if (!onHelmet)
			{
				Vector2 impulseDirection = Vector3.Reflect(rb.velocity, collision.contacts[0].normal);

				if (Hit(damageOnWallContact))
				{
					if (health.IsDead)
					{
						Kill();
					}
				}

			}
			AddVelocity(impulseOnWallContact, (transform.position - collision.collider.transform.position).normalized);

		}
		else if (collision.collider.tag == "Sparry")
		{
			AddVelocity(impulseOnWallContact, (transform.position - collision.collider.transform.position).normalized);
		}

	}

	public void Kill()
	{
		if (!isDead)
		{
			StartCoroutine(DeathAnimation());
		}
	}

	public float deadPartImpulse = 200;
	public float deathSlowScale = 0.4f;
	public float deathSlowTime = 5.0f;

	IEnumerator DeathAnimation()
	{
		isDead = true;

		SoundManager.Instance.PlaySingleAtLocation(deadSound, transform.position);
		GameManager.Instance.Slow(deathSlowScale, deathSlowTime);

		foreach (GameObject deadPart in deadParts)
		{
			deadPart.transform.parent = GameManager.Instance.persistentTransform;
			Vector2 force = new Vector2(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)) * Random.Range(-deadPartImpulse, deadPartImpulse) * Time.deltaTime;
			deadPart.GetComponent<Rigidbody2D>().AddForce(force);
		}

		gameObject.SetActive(false);

		GameManager.Instance.GameOver();

		yield return null;
	}
}
