/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLogic : Logic
{

	public Transform openedPoint;
	public Transform closedPoint;

	public bool isOpened = false;
	public float openSpeed;
	public float closedSpeed;

	bool isOpening;
	bool isClosing;

	MeshRenderer rend;

	Button button = null;

	private void Start()
	{
		rend = GetComponent<MeshRenderer>();
	}

	public override void Activate(Button button)
	{
		if (this.button == null || this.button == button)
		{
			base.Activate(button);
			this.button = button;
			Toggle();
		}
	}

	public override void Deactivate(Button button)
	{
		if (this.button == null || this.button == button)
		{
			base.Deactivate(button);
			this.button = null;
			Toggle();
		}
	}

	//private void Update()
	//{
	//	Vector2 target;
	//	if (isOpening)
	//	{
	//		target = openedPoint.position;
	//	}
	//	else
	//	{
	//		target = closedPoint.position;
	//	}
	//	if(isOpening || isClosing)
	//	{
	//		Vector2.MoveTowards(transform.position, target, 0.1f);
	//	}


	//}

	IEnumerator Open()
	{
		rend.enabled = false;
		yield return null;
	}

	IEnumerator Close()
	{
		rend.enabled = true;
		yield return null;
	}

	void Toggle()
	{
		if (isOpened)
		{
			isClosing = true;
			StopAllCoroutines();
			StartCoroutine(Close());
		}
		else
		{
			isOpening = true;
			StopAllCoroutines();
			StartCoroutine(Open());
		}
		isOpened = !isOpened;
	}
}
