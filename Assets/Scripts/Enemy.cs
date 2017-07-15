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

	protected virtual void Awake()
	{
		sR = GetComponent<SpriteRenderer>();
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
		GameObject explosionGO = GameObject.Instantiate(explosionTemplate, transform.position, Quaternion.identity);

		explosionGO.GetComponent<DestroyOnAnimationEnd>().Explode(sR.color);

		Destroy(gameObject);
	}
}
