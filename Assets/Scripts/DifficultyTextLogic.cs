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

		GameManager.Instance.ShowDifficultyAndLifeText(true);
		//GameManager.Instance.ShowDifficultyText(true);
		//GameManager.Instance.ShowLivesText(true);
	}

	public override void Deactivate()
	{
		base.Deactivate();

	}
}
