/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour {
	
	void Update ()
	{
		transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}
}
