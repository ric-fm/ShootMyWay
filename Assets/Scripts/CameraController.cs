/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float smoothTime = 0.125f;
	public Transform target;

	//public float depthOffset;

	void Start ()
	{
	}
	
	void FixedUpdate ()
	{
		Vector3 currentVelocity = Vector2.zero;
		Vector3 desiredPosition = Vector3.SmoothDamp(transform.position, target.position, ref currentVelocity, smoothTime);

		desiredPosition.z = transform.position.z;

		transform.position = desiredPosition;
	}
}
