/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public int life;

	public bool godMode;

	public void Hit(int damage)
	{
		if(godMode)
		{
			return;
		}
		life -= damage;

		if (life <= 0)
		{
			Die();
		}
	}

	public void Die()
	{
		Destroy(gameObject);
	}
}
