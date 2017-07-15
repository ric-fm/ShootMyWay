/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerStats {

	public int powerCount;
	public int cooldownCount;
	public int rangeCount;

	public int maxPowerCount;
	public int maxCooldownCount;
	public int maxRangeCount;

	public void IncreasePowerCount(int amount)
	{
		Mathf.Clamp(++powerCount, 0, maxPowerCount);
		DecreasePowerCount(amount);
	}

	public void DecreasePowerCount(int amount)
	{
		Mathf.Clamp(--powerCount, 0, maxPowerCount);
	}

	public void IncreaseCooldownCount(int amount)
	{
		Mathf.Clamp(++cooldownCount, 0, maxCooldownCount);
	}

	public void DecreaseCooldownCount(int amount)
	{
		Mathf.Clamp(--cooldownCount, 0, maxCooldownCount);
	}

	public void IncreaseRangeCount(int amount)
	{
		Mathf.Clamp(++rangeCount, 0, maxRangeCount);
	}

	public void DecreaseRangeCount(int amount)
	{
		Mathf.Clamp(--rangeCount, 0, maxRangeCount);
	}
}
