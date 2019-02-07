/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLogic : Logic {


	public Text text;

	public override void Activate()
	{
		base.Activate();

		Toggle();
	}

	void Toggle()
	{
		if(text.gameObject.activeSelf)
		{
			text.gameObject.SetActive(false);
		}
		else
		{
			text.gameObject.SetActive(true);
		}
	}
}
