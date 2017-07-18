/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

	MeshRenderer renderer;

	Texture texture;

	public Color color;

	public float changeColorInterval;
	public float colorLerpSpeed;

	public Vector2 redRange;
	public Vector2 greenRange;
	public Vector2 blueRange;

	public float setOffsetTargetInterval;
	public float offsetLerpSpeed;

	Vector2 offset;

	void Start ()
	{
		renderer = GetComponent<MeshRenderer>();

		StartCoroutine(ChangeColor());
		StartCoroutine(SetOffsetTarget());
	}
	
	void LateUpdate ()
	{
		//renderer.material.SetTextureOffset("_MainTex", renderer.material.GetTextureOffset("_MainTex") + colorLerpSpeed * Time.deltaTime);

		Color newColor = Color.Lerp(renderer.material.GetColor("_Color"), color, colorLerpSpeed * Time.deltaTime);

		renderer.material.SetColor("_Color", newColor);

		Vector2 newOffset = Vector2.Lerp(renderer.material.GetTextureOffset("_MainTex"), offset, offsetLerpSpeed * Time.deltaTime);

		renderer.material.SetTextureOffset("_MainTex", newOffset);
	}

	IEnumerator ChangeColor()
	{
		while(true)
		{
			yield return new WaitForSecondsRealtime(changeColorInterval);

			color = new Color(Random.Range(redRange.x, redRange.y), Random.Range(greenRange.x, greenRange.y), Random.Range(blueRange.x, blueRange.y));
		}
	}

	IEnumerator SetOffsetTarget()
	{
		while(true)
		{
			yield return new WaitForSecondsRealtime(setOffsetTargetInterval);

			offset = new Vector2(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

		}
	}
}
