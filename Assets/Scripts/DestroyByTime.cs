/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour {

	public float time;

	void Start ()
	{
		StartCoroutine(Destroy());
	}
	
	IEnumerator Destroy()
	{
		yield return new WaitForSeconds(time);

		Destroy(gameObject);
	}
}
