/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public int life;

	public bool godMode;

	public bool IsDead
	{
		get
		{
			return life <= 0;
		}
	}

	public bool Hit(int damage)
	{
		if(godMode || life <= 0)
		{
			return false;
		}
		life -= damage;

		if (life <= 0)
		{
			//Die();
			return true;
		}
		return false;
	}

	//public void Die()
	//{
	//	Destroy(gameObject);
	//}
}
