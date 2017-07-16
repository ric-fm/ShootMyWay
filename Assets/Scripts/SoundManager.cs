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

	//public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
	//public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.


	void Awake()
	{
			instance = this;
	}

	public void PlaySingle(AudioSource source, AudioClip clip)
	{
		source.clip = clip;
		source.Play();

	}


	//public void RandomizeSfx(params AudioClip[] clips)
	//{
	//	int randomIndex = Random.Range(0, clips.Length);


	//	float randomPitch = Random.Range(lowPitchRange, highPitchRange);
	//}
}
