using UnityEngine;
using UnityEngine.UI;

public class GameCameraTextureDisplay : MonoBehaviour
{
	public static GameCameraTextureDisplay Instance;

	private RenderTexture texture;

	public RawImage image;

	public Image altImage;

	private void Awake()
	{
		Instance = this;
	}

	private void LateUpdate()
	{
		if (!image)
		{
			return;
		}
		if ((bool)GameManager.instance && GameManager.instance.sceneName != "Menu_Title")
		{
			image.texture = texture;
			if (image.texture == null)
			{
				image.enabled = false;
			}
			else
			{
				image.enabled = true;
			}
		}
		else
		{
			image.enabled = false;
		}
		if ((bool)altImage)
		{
			altImage.enabled = !image.enabled;
		}
	}

	public void UpdateDisplay(RenderTexture source, Material material)
	{
		if (base.gameObject.activeInHierarchy)
		{
			if (texture == null)
			{
				texture = new RenderTexture(source.width, source.height, source.depth);
			}
			Graphics.Blit(source, Instance.texture, material);
		}
	}
}
