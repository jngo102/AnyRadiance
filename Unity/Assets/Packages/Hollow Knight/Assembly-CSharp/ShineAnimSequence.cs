using System;
using System.Collections;
using UnityEngine;

public class ShineAnimSequence : MonoBehaviour
{
	[Serializable]
	public class ShineObject
	{
		public SpriteRenderer renderer;

		public Sprite[] shineSprites;

		public float fps = 12f;

		public IEnumerator ShineAnim()
		{
			if ((bool)renderer && shineSprites.Length != 0)
			{
				Sprite initialSprite = renderer.sprite;
				WaitForSeconds wait = new WaitForSeconds(1f / fps);
				Sprite[] array = shineSprites;
				foreach (Sprite sprite in array)
				{
					renderer.sprite = sprite;
					yield return wait;
				}
				renderer.sprite = initialSprite;
			}
		}
	}

	public ShineObject[] shineObjects;

	public float shineDelay = 0.5f;

	public float sequencePauseTime = 3f;

	private void Start()
	{
		StartCoroutine(ShineSequence());
	}

	private IEnumerator ShineSequence()
	{
		while (true)
		{
			yield return new WaitForSeconds(sequencePauseTime);
			ShineObject[] array = shineObjects;
			foreach (ShineObject shineObject in array)
			{
				if (shineObject.renderer.gameObject.activeInHierarchy)
				{
					StartCoroutine(shineObject.ShineAnim());
				}
				yield return new WaitForSeconds(shineDelay);
			}
		}
	}
}
