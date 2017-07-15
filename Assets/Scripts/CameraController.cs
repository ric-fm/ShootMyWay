/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float smoothTime = 0.125f;
	public Transform target;
	Vector3 desiredPosition;

	//public float depthOffset;

	void Start ()
	{
	}
	
	void FixedUpdate ()
	{
		Vector3 currentVelocity = Vector2.zero;
		desiredPosition = Vector3.SmoothDamp(transform.position, target.position, ref currentVelocity, smoothTime);

		desiredPosition.z = transform.position.z;

		transform.position = desiredPosition;
	}

	public void Shake(float magnitude, float duration)
	{
		StopAllCoroutines();
		StartCoroutine(PlayShake(magnitude, duration));
	}

	IEnumerator PlayShake(float magnitude, float duration)
	{

		float elapsed = 0.0f;

		//Vector3 originalCamPos = transform.position;

		while (elapsed < duration)
		{

			elapsed += Time.deltaTime;

			float percentComplete = elapsed / duration;
			float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

			// map value to [-1, 1]
			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			x *= magnitude * damper;
			y *= magnitude * damper;

			x += desiredPosition.x;
			y += desiredPosition.y;

			transform.position = new Vector3(x, y, desiredPosition.z);

			yield return null;
		}

		transform.position = desiredPosition;
	}
}
