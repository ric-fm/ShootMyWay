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
		Debug.Log("KillAll enemy destroyed");

		enemies.Remove(enemy);

		if (enemies.Count == 0)
		{
			Debug.Log("Completed");
			Completed();
		}
	}

	void Completed()
	{
		foreach (Logic logic in logics)
		{
			if(logic != null)
			{
				logic.Activate();
			}
		}
	}

	private void Update()
	{
		//if (Input.GetKeyDown(KeyCode.W))
		//{
		//	List<Enemy> enemyCopy = enemies.ToList();
		//	foreach (Enemy enemy in enemyCopy)
		//	{
		//		GameObject.Destroy(enemy.gameObject);
		//	}
		//}
	}
}
