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

	public List<SpriteRenderer> renderers;
	public List<Collider2D> colliders;

	public bool startOpened;

	AudioSource source;

	public AudioClip openSound;
	public AudioClip closeSound;

	private void Start()
	{
		source = GetComponent<AudioSource>();
		if(startOpened)
		{
			Open(false);
		}
	}

	public override void Activate()
	{
		base.Activate();

		Toggle();
	}

	public override void Deactivate()
	{
		base.Deactivate();

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

	void Open(bool sound = true)
	{

		if(sound)
			SoundManager.Instance.PlaySingle(source, openSound);

		foreach(SpriteRenderer renderer in renderers)
		{
			renderer.enabled = false;
		}

		foreach(Collider2D coll in colliders)
		{
			coll.enabled = false;
		}
		isOpened = true;

		//yield return null;
	}

	void Close()
	{

		foreach (SpriteRenderer renderer in renderers)
		{
			renderer.enabled = true;
		}

		foreach (Collider2D coll in colliders)
		{
			coll.enabled = true;
		}
		isOpened = false;

		//yield return null;
	}

	void Toggle()
	{
		if (isOpened)
		{
			isClosing = true;
			//StopAllCoroutines();
			//StartCoroutine(Close());
			Close();
		}
		else
		{
			isOpening = true;
			//StopAllCoroutines();
			//StartCoroutine(Open());
			Open();
		}
		//isOpened = !isOpened;
	}
}
