using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BasicSpriteAnimator : MonoBehaviour
{
	public float fps = 30f;

	[Space]
	public Sprite[] frames;

	public bool startRandom = true;

	public bool looping = true;

	private SpriteRenderer rend;

	private Coroutine animRoutine;

	public float Length => (float)frames.Length / fps;

	private void Awake()
	{
		rend = GetComponent<SpriteRenderer>();
	}

	private void OnEnable()
	{
		if (frames.Length > 1)
		{
			animRoutine = StartCoroutine(Animate());
		}
	}

	private void OnDisable()
	{
		if (animRoutine != null)
		{
			StopCoroutine(animRoutine);
		}
	}

	private IEnumerator Animate()
	{
		int index = 0;
		if (startRandom)
		{
			index = Random.Range(0, frames.Length);
		}
		while (true)
		{
			if (rend.enabled)
			{
				rend.sprite = frames[index];
			}
			yield return new WaitForSeconds(1f / fps);
			index++;
			if (index >= frames.Length)
			{
				if (!looping)
				{
					break;
				}
				index = 0;
			}
		}
	}
}
