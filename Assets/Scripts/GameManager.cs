/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

	CameraController cameraController;
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
		cameraController = GameObject.FindObjectOfType<CameraController>();
		playerController = GameObject.FindObjectOfType<PlayerController>();
		enemyRecord = new EnemyRecord();
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.R))
		{
			RestartLevel();
		}
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

			case Enemy.ColorType.GREEN:
				++enemyRecord.Green;
				break;

			case Enemy.ColorType.BLUE:
				++enemyRecord.Blue;
				break;
		}

		playerController.EnemyKilled(enemy);
	}

	public void SparryHit(Sparry sparry)
	{
		switch (sparry.colorType)
		{
			case Enemy.ColorType.NONE:
				++enemyRecord.None;
				break;

			case Enemy.ColorType.RED:
				++enemyRecord.Red;
				break;

			case Enemy.ColorType.GREEN:
				++enemyRecord.Green;
				break;

			case Enemy.ColorType.BLUE:
				++enemyRecord.Blue;
				break;
		}

		playerController.SparryHit(sparry);
	}

	public void ShakeScreen(float magnitude, float duration)
	{
		cameraController.Shake(magnitude, duration);
	}

	public void ShakeScreen(float magnitude, float duration, Vector2 position)
	{
		float distanceToPlayer = ((Vector2)playerController.transform.position - position).magnitude;
		cameraController.Shake(magnitude/distanceToPlayer, duration);
	}

	public void RestartLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
