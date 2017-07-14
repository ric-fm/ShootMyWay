/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TestTint : MonoBehaviour {

	public enum ColorType
	{
		NONE,
		RED,
		GREEN,
		BLUE
	}

	public Color red;

	public Color green;

	public Color blue;

	SpriteRenderer sr;

	public ColorType type;

	void Start ()
	{
		sr = GetComponent<SpriteRenderer>();
	}
	
	void Update ()
	{
		switch(type)
		{
			case ColorType.RED:
				SetColor(red);
				break;
			case ColorType.GREEN:
				SetColor(green);
				break;

			case ColorType.BLUE:
				SetColor(blue);
				break;

			default:
				SetColor(new Color(1, 1, 1, 1));
				break;
		}
	}

	void SetColor(Color color)
	{
		sr.color = color;
	}
}
