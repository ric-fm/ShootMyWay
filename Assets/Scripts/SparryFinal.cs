/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparryFinal : MonoBehaviour {

	public Enemy.ColorType colorType;

	Animator animator;

	public List<SpriteRenderer> renderers;

	public float cooldownTime;

	bool canHit = true;

	public GameObject explosionTemplate;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	public bool Hit()
	{
		if(canHit)
		{
			animator.SetTrigger("Hit");
			GameManager.Instance.SparryFinalHit(this);
			StartCoroutine(Cooldown());
			return true;
		}
		return false;
	}

	public float shakeMagnitude;
	public float shakeDuration;
	public Color color;
	public void Explode()
	{
		//GameObject explosionGO = GameObject.Instantiate(explosionTemplate, transform.position, Quaternion.identity);

		//explosionGO.GetComponent<DestroyOnAnimationEnd>().Explode(color);

		GameManager.Instance.ShakeScreen(shakeMagnitude, shakeDuration, transform.position);

		GameManager.Instance.playerController.Kill();
		Destroy(gameObject);
	}

	IEnumerator Cooldown()
	{
		canHit = false;
		yield return new WaitForSeconds(cooldownTime);
		canHit = true;
	}

	void ChangeColor(Color color)
	{
		foreach(SpriteRenderer renderer in renderers)
		{
			renderer.color = color;
		}
	}
}
