/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	public AudioClip explosionSound;

	public void Explode()
	{
		SoundManager.Instance.PlaySingleAtLocation(explosionSound, transform.position);
	}
}
