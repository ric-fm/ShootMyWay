/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KillAllObjective : MonoBehaviour
{

	public List<Enemy> enemies;

	public List<Logic> logics;


	private void Start()
	{
		GameManager.Instance.OnEnemyDestroyed += EnemyDestroyed;
	}

	void EnemyDestroyed(Enemy enemy)
	{
		if (!enemies.Contains(enemy))
		{
			return;
		}

		enemies.Remove(enemy);

		if (enemies.Count == 0)
		{
			Completed();
		}
	}

	void Completed()
	{
		foreach (Logic logic in logics)
		{
			if (logic != null)
			{
				logic.Activate();
			}
		}
		GameManager.Instance.OnEnemyDestroyed -= EnemyDestroyed;

	}

	//private void Update()
	//{
	//	if (Input.GetKeyDown(KeyCode.W))
	//	{
	//		List<Enemy> enemyCopy = enemies.ToList();
	//		foreach (Enemy enemy in enemyCopy)
	//		{
	//			//GameObject.Destroy(enemy.gameObject);
	//			enemy.Kill();
	//		}
	//	}
	//}
}
