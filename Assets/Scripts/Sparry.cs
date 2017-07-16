/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparry : MonoBehaviour {

	public Enemy.ColorType colorType;

	Animator animator;

	public List<SpriteRenderer> renderers;

	public float cooldownTime;

	bool canHit = true;

	private void Start()
	{
		animator = GetComponent<Animator>();
		ChangeColorType(colorType);
	}

	public bool Hit()
	{
		if(canHit)
		{
			animator.SetTrigger("Hit");
			GameManager.Instance.SparryHit(this);
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

	void ChangeColorType(Enemy.ColorType colorType)
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
}
