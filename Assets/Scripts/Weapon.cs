/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public bool CanShoot { get; protected set; }
	public virtual void Shoot()
	{

	}
}
