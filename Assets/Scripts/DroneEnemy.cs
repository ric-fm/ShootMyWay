/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneEnemy : Enemy
{

	public Transform target;
	Vector2 targetPoint;

	Vector2 direction;

	Rigidbody2D rb;

	public float speed;

	public float homingSensitivity;

	public float turnAngle;

	public float CheckDistance;

	public float balanceFactor;

	public float velocityTurnLimit;

	public float chooseRancomTargetPointInterval;
	public float randomTargetOffset;

	bool hasTarget = false;
	bool hasRandomTarget = false;
	bool alive = true;

	

	public float maxVelocity;

	protected override void Awake()
	{
		base.Awake();

		rb = GetComponent<Rigidbody2D>();
		target = GameObject.FindObjectOfType<PlayerController>().transform;
		targetPoint = transform.position;
		//SelectRandomTarget();
	}

	protected override void Start()
	{
		base.Start();

	}

	void Update()
	{
		if (CanReachTarget())
		{
			targetPoint = target.position;
			if (!hasTarget)
			{
				hasTarget = true;
				hasRandomTarget = false;
				StopAllCoroutines();
			}

			direction = (target.transform.position - this.transform.position).normalized;

			Vector3 relativePos = target.position - transform.position;
			Quaternion rotation = Quaternion.LookRotation(relativePos);
			rb.velocity = direction * speed * Time.deltaTime;
		}
		else if(hasRandomTarget)
		{
			direction = (targetPoint - (Vector2)this.transform.position).normalized;
			Vector2 desiredVelocity = direction * speed * Time.deltaTime;

			rb.velocity = Vector2.Lerp(rb.velocity, desiredVelocity, balanceFactor * Time.deltaTime);
		}
		else
		{
			if (hasTarget || !hasRandomTarget)
			{
				hasTarget = false;
				StartCoroutine(ChooseRandomTargetPoint());
			}
			//rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, balanceFactor * Time.deltaTime);
			rb.velocity = Vector2.ClampMagnitude(Vector2.Lerp(rb.velocity, Vector2.zero, balanceFactor * Time.deltaTime), maxVelocity);
		}
		Animate();
	}

	void Animate()
	{
		if (rb.velocity.x > velocityTurnLimit)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, -turnAngle), homingSensitivity * Time.deltaTime);

		}
		else if (rb.velocity.x < -velocityTurnLimit)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, turnAngle), homingSensitivity * Time.deltaTime);

		}
		else
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), homingSensitivity * Time.deltaTime);
		}
	}

	bool CanReachTarget()
	{
		return Vector2.Distance(target.position, transform.position) < CheckDistance;
	}

	IEnumerator ChooseRandomTargetPoint()
	{
		while (alive)
		{
			hasRandomTarget = true;
			yield return new WaitForSeconds(chooseRancomTargetPointInterval);

			SelectRandomTarget();
		}
	}

	void SelectRandomTarget()
	{
		float randX = Random.Range(transform.position.x - randomTargetOffset, transform.position.x + randomTargetOffset);
		float randY = Random.Range(transform.position.y - randomTargetOffset, transform.position.y + randomTargetOffset);
		new Vector2(randX, randY);
		targetPoint = new Vector2(randX, randY);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.gameObject.tag == "Player")
		{
			
			GameManager.Instance.playerController.AddVelocity(impulseOnContact, (GameManager.Instance.playerController.transform.position - transform.position).normalized);

			GameManager.Instance.playerController.Hit(damageOnContact);
			Kill();
		}
		else if (collision.collider.gameObject.tag == "Helmet")
		{
			GameManager.Instance.playerController.AddVelocity(impulseOnContact, (GameManager.Instance.playerController.transform.position - transform.position).normalized);

			if (GameManager.Instance.playerController.CurrentStat != PlayerController.StatType.RANGE)
			{
				GameManager.Instance.playerController.Hit(damageOnContact);
			}
			Kill();
		}
		else
		{
			SelectRandomTarget();
		}
	}

	public override void AddVelocity(float velocity, Vector2 direction)
	{
		rb.velocity = Vector2.ClampMagnitude(rb.velocity + velocity * direction * Time.deltaTime, maxVelocity);
	}
}
