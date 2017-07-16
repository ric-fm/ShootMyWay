/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyTextLogic : Logic
{

	public override void Activate()
	{
		base.Activate();

		GameManager.Instance.ShowDifficultyText(true);
	}

	public override void Deactivate()
	{
		base.Deactivate();

	}
}
