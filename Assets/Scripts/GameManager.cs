/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

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


	public Transform persistentTransform;

	public bool timerOn;
	public float timer;

	public Text timerText;

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

		if (timerOn == true)
		{
			timer += Time.deltaTime;
		}

		//string minutes = Mathf.Floor(timer / 60).ToString("00");
		//string seconds = (timer % 60).ToString("00");

		timerText.text = string.Format("{0}:{1}", Mathf.Floor(timer / 60).ToString("00"), (timer % 60).ToString("00"));
	}

	public void StartTimer()
	{
		timer = 0.0f;
		timerOn = true;
	}

	public void PauseTimer()
	{
		timerOn = false;
	}

	public void ResumeTimer()
	{
		timerOn = true;
	}

	public void ResetTimer()
	{
		timerOn = false;
		timer = 0.0f;
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


	bool canSlow = true;
	public void Sleep(float time)
	{
		if(canSlow)
		{
			StartCoroutine(SlowC(0.0f, time));
		}
	}

	public void Slow(float scale, float time)
	{
		if (canSlow)
		{
			StartCoroutine(SlowC(scale, time));
		}
	}


	IEnumerator SlowC(float scale, float time)
	{
		canSlow = false;
		float endTime = Time.unscaledTime + time;

		float backupTimeScale = Time.timeScale;

		Time.timeScale = scale;

		yield return new WaitForSecondsRealtime(time);

		Time.timeScale = backupTimeScale;

		canSlow = true;
	}

	public void ClearPersistance()
	{
		foreach(Transform child in persistentTransform.transform)
		{
			Destroy(child.gameObject);
		}
	}
}
