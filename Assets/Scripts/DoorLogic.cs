/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLogic : Logic
{
	public bool isOpened = false;

	bool isOpening;
	bool isClosing;

	public List<MeshRenderer> renderers;

	public override void Activate()
	{
		base.Activate();

		Debug.Log("Activate door " + name);

		Toggle();
	}

	public override void Deactivate()
	{
		base.Deactivate();

		Debug.Log("Deactivate door " + name);

		Toggle();
	}

	//public override void Activate(Button button)
	//{
	//	if (this.button == null || this.button == button)
	//	{
	//		base.Activate(button);
	//		this.button = button;
	//		Toggle();
	//	}
	//}

	//public override void Deactivate(Button button)
	//{
	//	if (this.button == null || this.button == button)
	//	{
	//		base.Deactivate(button);
	//		this.button = null;
	//		Toggle();
	//	}
	//}

	IEnumerator Open()
	{
		foreach(MeshRenderer renderer in renderers)
		{
			renderer.enabled = false;
		}

		yield return null;
	}

	IEnumerator Close()
	{
		foreach (MeshRenderer renderer in renderers)
		{
			renderer.enabled = true;
		}

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
