/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : Logic {

	public List<Enemy> enemies;

	private void Start()
	{
		foreach (Enemy enemy in enemies)
		{
			enemy.gameObject.SetActive(false);
		}
	}

	public override void Activate()
	{
		base.Activate();

		Spawn();
	}

	void Spawn()
	{
		foreach(Enemy enemy in enemies)
		{
			enemy.gameObject.SetActive(true);
		}
	}
}
