using UnityEngine;

[RequireComponent(typeof(LightBlurredBackground))]
public class BlurManager : MonoBehaviour
{
	private ShaderQualities appliedShaderQuality;

	private LightBlurredBackground lightBlurredBackground;

	[SerializeField]
	private int baseHeight;

	[SerializeField]
	private int largeConsoleHeight;

	protected void Awake()
	{
		appliedShaderQuality = ShaderQualities.High;
		lightBlurredBackground = GetComponent<LightBlurredBackground>();
		int renderTextureHeight = baseHeight;
		if (Application.isConsolePlatform)
		{
			renderTextureHeight = largeConsoleHeight;
		}
		lightBlurredBackground.RenderTextureHeight = renderTextureHeight;
	}

	protected void Update()
	{
		GameManager unsafeInstance = GameManager.UnsafeInstance;
		if (!(unsafeInstance != null))
		{
			return;
		}
		ShaderQualities shaderQuality = unsafeInstance.gameSettings.shaderQuality;
		if (shaderQuality != appliedShaderQuality)
		{
			appliedShaderQuality = shaderQuality;
			if (shaderQuality <= ShaderQualities.Medium)
			{
				lightBlurredBackground.PassGroupCount = ((shaderQuality == ShaderQualities.Low) ? 1 : 2);
				lightBlurredBackground.enabled = true;
			}
			else
			{
				lightBlurredBackground.enabled = false;
			}
		}
	}
}
