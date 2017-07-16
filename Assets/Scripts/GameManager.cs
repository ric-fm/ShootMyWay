/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

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
	AudioListener listener;

	public Color noneColor;

	public Color redColor;

	public Color greenColor;

	public Color blueColor;


	public Transform persistentTransform;

	public bool timerOn;
	public float timer;

	public Text timerText;

	public bool fadeInActive;
	public bool fadeOutActive;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		cameraController = GameObject.FindObjectOfType<CameraController>();
		playerController = GameObject.FindObjectOfType<PlayerController>();
		listener = cameraController.gameObject.GetComponent<AudioListener>();
		enemyRecord = new EnemyRecord();

		if (fadeInActive)
		{
			StartCoroutine(StartLevelC());
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
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
		switch (enemy.colorType)
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
		cameraController.Shake(magnitude / distanceToPlayer, duration);
	}

	bool restart = false;
	public void RestartLevel()
	{
		if (!restart)
		{
			StopAllCoroutines();
			StartCoroutine(RestartLevelC());
		}
	}

	public Image fadeImage;
	public float fadeInSpeed;

	public float fadeOutSpeed;
	IEnumerator StartLevelC()
	{
		fadeImage.color = Color.black;
		Time.timeScale = 0.0f;
		AudioListener.volume = 0.0f;
		while (fadeImage.color.a > 0.3f)
		{
			float a = Mathf.Lerp(fadeImage.color.a, 0.0f, fadeInSpeed * Time.unscaledDeltaTime);
			fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, a);
			yield return null;
		}
		fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0.0f);
		Time.timeScale = 1.0f;
		AudioListener.volume = 1.0f;


	}

	IEnumerator RestartLevelC()
	{
		restart = true;

		if (fadeOutActive)
		{
			Time.timeScale = 0.0f;

			AudioListener.volume = 0.0f;

			while (fadeImage.color.a < 0.7f)
			{
				fadeImage.color = Color.Lerp(fadeImage.color, Color.black, fadeOutSpeed * Time.unscaledDeltaTime);

				yield return null;
			}

			restart = false;
			Time.timeScale = 1.0f;
			AudioListener.volume = 1.0f;
		}
		restart = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}


	bool canSlow = true;
	public void Sleep(float time)
	{
		if (canSlow)
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
		foreach (Transform child in persistentTransform.transform)
		{
			Destroy(child.gameObject);
		}
	}

	bool gameOver = false;

	public void GameOver()
	{
		if (!gameOver)
		{
			PauseTimer();
			StartCoroutine(GameOverC());
		}
	}

	public Text restartText;

	IEnumerator GameOverC()
	{
		gameOver = true;

		yield return new WaitForSecondsRealtime(3.0f);

		Debug.Log("Press R to restart");

		restartText.GetComponent<Animator>().SetTrigger("FadeIn");

		while (restartText.color.a < 0.8f)
		{
			restartText.color = Color.Lerp(restartText.color, Color.white, fadeInSpeed * Time.unscaledDeltaTime);

			yield return null;
		}

		restartText.GetComponent<Animator>().SetTrigger("Animate");
	}
}
