/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLogic : Logic {


	Button button;
	void Start ()
	{
		button = gameObject.GetComponent<Button>();
	}

	public override void Activate()
	{
		base.Activate();

		Toggle();
	}

	//public override void Deactivate()
	//{
	//	base.Deactivate();

	//	Toggle();
	//}

	void Toggle()
	{
		button.locked = !button.locked;
	}
}
