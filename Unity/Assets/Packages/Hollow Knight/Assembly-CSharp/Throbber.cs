using UnityEngine;
using UnityEngine.UI;

public class Throbber : MonoBehaviour
{
	[SerializeField]
	private Image image;

	[SerializeField]
	private Sprite[] sprites;

	[SerializeField]
	private float frameRate;

	private float frameTimer;

	private int frameIndex;

	protected void Update()
	{
		float num = Mathf.Min(1f / 60f, Time.unscaledDeltaTime);
		if (sprites.Length != 0)
		{
			frameTimer += num * frameRate;
			int num2 = (int)frameTimer;
			if (num2 > 0)
			{
				frameTimer -= num2;
				frameIndex = (frameIndex + num2) % sprites.Length;
				image.sprite = sprites[frameIndex];
			}
		}
	}
}
