/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnAnimationEnd : MonoBehaviour {

	public float delay;

	public void Explode(Color color)
	{
		SpriteRenderer sR = GetComponent<SpriteRenderer>();
		sR.color = color;

		Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
	}
}
