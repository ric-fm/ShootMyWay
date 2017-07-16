/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Bullet {

	public Transform target;

	public float detectTargetInterval;

	public bool alive = true;

	public float turnSpeed;

	public float speed;
	public float homingSensitivity;

	public float shakeMagnitudeOnExplode;
	public float shakeDurationOnExplode;

	Vector2 direction;

	public float noiseFactor = 10;
	public float noiseDegrees = 15.0f;

	public GameObject explosionTemplate;

	public override void Shoot(Vector2 direction, float speed)
	{
		this.direction = direction;
		this.speed = speed;
	}

	private void Update()
	{
		Deviate();

		rb.velocity = direction * speed * Time.deltaTime;
	}

	void Deviate()
	{
		float rand_angle = Random.Range(-noiseDegrees, noiseDegrees);

		transform.Rotate( new Vector3(0, 0, rand_angle * noiseFactor * Time.deltaTime));
		direction = transform.up;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag.Equals("Player"))
		{
			GameManager.Instance.playerController.Hit(damage);
			StopAllCoroutines();
		}
		if (collision.collider.gameObject.tag == "Helmet")
		{
			if (GameManager.Instance.playerController.CurrentStat != PlayerController.StatType.RANGE)
			{
				GameManager.Instance.playerController.Hit(damage);
			}
			//Kill();
		}

		Destroy(gameObject);

		GameObject explosionGO = GameObject.Instantiate(explosionTemplate, transform.position, Quaternion.identity);
		explosionGO.transform.localScale = Vector2.one * 0.8f;

		explosionGO.GetComponent<DestroyOnAnimationEnd>().Explode(GameManager.Instance.noneColor);

		GameManager.Instance.ShakeScreen(shakeMagnitudeOnExplode, shakeDurationOnExplode, transform.position);
	}
}
