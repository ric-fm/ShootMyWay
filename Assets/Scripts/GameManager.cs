/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private static GameManager instance;

	public static GameManager Instance
	{
		get
		{
			return instance;
		}
	}

	EnemyRecord enemyRecord;

	PlayerController playerController;

	public Color noneColor;

	public Color redColor;

	public Color greenColor;

	public Color blueColor;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		playerController = GameObject.FindObjectOfType<PlayerController>();
		enemyRecord = new EnemyRecord();
	}

	public void EnemyKilled(Enemy enemy)
	{
		switch(enemy.colorType)
		{
			case Enemy.ColorType.NONE:
				++enemyRecord.None;
				break;

			case Enemy.ColorType.RED:
				++enemyRecord.Red;
				break;

			case Enemy.ColorType.GREN:
				++enemyRecord.Green;
				break;

			case Enemy.ColorType.BLUE:
				++enemyRecord.Blue;
				break;
		}

		playerController.EnemyKilled(enemy);
	}
}
