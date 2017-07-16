/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : Logic {

	public List<Enemy> enemies;

	AudioSource source;

	public AudioClip spawnSound;

	private void Start()
	{
		source = GetComponent<AudioSource>();
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
		SoundManager.Instance.PlaySingle(source, spawnSound);
		foreach(Enemy enemy in enemies)
		{
			enemy.gameObject.SetActive(true);
		}
	}
}
