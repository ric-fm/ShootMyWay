/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	private static SoundManager instance = null;
	public static SoundManager Instance
	{
		get
		{
			return instance;
		}
	}

	public GameObject sourceGOTemplate;


	void Awake()
	{
			instance = this;
	}

	public void PlaySingle(AudioSource source, AudioClip clip)
	{
		source.clip = clip;
		source.Play();

	}

	public void PlaySingleAtLocation(AudioClip clip, Vector2 position)
	{
		GameObject sourceGO = GameObject.Instantiate(sourceGOTemplate, position, Quaternion.identity);
		sourceGO.GetComponent<SoundGameObject>().Play(clip);
	}

	public void PlaySingleAtLocation(AudioClip clip, float volume, Vector2 position)
	{
		GameObject sourceGO = GameObject.Instantiate(sourceGOTemplate, position, Quaternion.identity);
		sourceGO.GetComponent<SoundGameObject>().Play(clip,volume);

	}


	//public void RandomizeSfx(params AudioClip[] clips)
	//{
	//	int randomIndex = Random.Range(0, clips.Length);


	//	float randomPitch = Random.Range(lowPitchRange, highPitchRange);
	//}
}
