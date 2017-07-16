/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

	SpriteRenderer sR;

	public Vector2 speed;

	void Start ()
	{
		sR = GetComponent<SpriteRenderer>();
	}
	
	void Update ()
	{
		Debug.Log(sR.sprite.textureRectOffset);

		//sR.sprite. += speed * Time.deltaTime;
	}
}
