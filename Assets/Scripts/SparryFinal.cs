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

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	public bool Hit()
	{
		if(canHit)
		{
			animator.SetTrigger("Hit");
			Debug.Log("Hit sparry final");
			GameManager.Instance.SparryFinalHit(this);
			StartCoroutine(Cooldown());
			return true;
		}
		return false;
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
