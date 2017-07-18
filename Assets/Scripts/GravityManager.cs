/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GravityManager : MonoBehaviour {

	private GravityManager instance;
	public static GravityManager Instance
	{
		get
		{
			return Instance;
		}
	}

	List<GravitySensible> sensibles;

	Vector2 initialGravityDirection;

	public bool invertGravity = false;
	public bool invertGravityAxis = false;

	public bool toggleGravity = false;
	bool gravity0 = false;

	private void Awake()
	{
		instance = this;
	}

	void Start ()
	{
		initialGravityDirection = Physics2D.gravity;
		sensibles = GameObject.FindObjectsOfType<GravitySensible>().ToList();
	}

	private void Update()
	{
		if(invertGravity)
		{
			invertGravity = false;

			SetGravityDirection(-Physics2D.gravity);
		}
		if(invertGravityAxis)
		{
			invertGravityAxis = false;

			SetGravityDirection(new Vector2(Physics2D.gravity.y, Physics2D.gravity.x));
		}
		if(toggleGravity)
		{
			toggleGravity = false;
			if(gravity0)
			{
				RestoreGravityScale();
			}
			else
			{
				SetGravityScale(0);
			}
		}
	}

	public void SetGravityScale(float value)
	{
		foreach(GravitySensible sensible in sensibles)
		{
			sensible.SetGravityScale(value);
		}
	}

	public void SetGravityDirection(Vector2 direction)
	{
		Physics2D.gravity = direction;
	}

	public void RestoreGravityScale()
	{
		foreach (GravitySensible sensible in sensibles)
		{
			sensible.RestoreGravityScale();
		}
	}

	public void RestoreGravityDirection(Vector2 direction)
	{
		Physics2D.gravity = initialGravityDirection;
	}
}
