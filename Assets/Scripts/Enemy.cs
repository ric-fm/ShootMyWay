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
		GREN
	}

	public ColorType colorType;
}
