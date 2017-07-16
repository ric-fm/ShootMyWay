/*
* Author: Ricardo Franco Martín
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPalette : MonoBehaviour
{

	MeshRenderer renderer;

	Texture texture;

	public Color desiredColor;

	public float changeColorInterval;
	public float colorLerpSpeed;

	public List<Color> palette;

	public float setOffsetTargetInterval;
	public float offsetLerpSpeed;

	Vector2 desiredOffset;

	public bool animateColor;

	public bool animateMovement;

	void Start()
	{
		renderer = GetComponent<MeshRenderer>();

		if (palette.Count > 0 && animateColor)
		{
			StartCoroutine(ChangeColor());
		}
		if (animateMovement)
		{
			StartCoroutine(SetOffsetTarget());
		}
	}

	void LateUpdate()
	{
		if (animateColor)
		{
			Color newColor = Color.Lerp(renderer.material.GetColor("_Color"), desiredColor, colorLerpSpeed * Time.deltaTime);

			renderer.material.SetColor("_Color", newColor);
		}

		if (animateMovement)
		{
			Vector2 newOffset = Vector2.Lerp(renderer.material.GetTextureOffset("_MainTex"), desiredOffset, offsetLerpSpeed * Time.deltaTime);

			renderer.material.SetTextureOffset("_MainTex", newOffset);
		}
	}

	IEnumerator ChangeColor()
	{
		while (true)
		{
			yield return new WaitForSecondsRealtime(changeColorInterval);

			desiredColor = palette[Random.Range(0, palette.Count - 1)];
		}
	}

	IEnumerator SetOffsetTarget()
	{
		while (true)
		{
			yield return new WaitForSecondsRealtime(setOffsetTargetInterval);

			desiredOffset = new Vector2(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

		}
	}
}
