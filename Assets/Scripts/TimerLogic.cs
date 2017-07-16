/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerLogic : Logic {

	public override void Activate()
	{
		base.Activate();
		GameManager.Instance.StartTimer();
	}
}
