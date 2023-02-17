using System;
using GlobalEnums;
using UnityEngine;
using UnityEngine.Events;

public class PerBuildTypeOnEnableResponse : MonoBehaviour
{
	[Serializable]
	private struct Tk2dSpriteColorResponse
	{
		public tk2dSprite Sprite;

		public Color Color;
	}

	[SerializeField]
	private BuildTypes[] buildTypes;

	[Space]
	public UnityEvent OnIsBuildType;

	[SerializeField]
	private Tk2dSpriteColorResponse[] isBuildTypeColor;

	[Space]
	public UnityEvent OnNotBuildType;

	[SerializeField]
	private Tk2dSpriteColorResponse[] notBuildTypeColor;

	private void OnEnable()
	{
		BuildTypes buildTypes = BuildTypes.Regular;
		BuildTypes[] array = this.buildTypes;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == buildTypes)
			{
				DoEvent(value: true);
				return;
			}
		}
		DoEvent(value: false);
	}

	private void DoEvent(bool value)
	{
		Tk2dSpriteColorResponse[] array;
		if (value)
		{
			OnIsBuildType?.Invoke();
			array = isBuildTypeColor;
			for (int i = 0; i < array.Length; i++)
			{
				Tk2dSpriteColorResponse tk2dSpriteColorResponse = array[i];
				if ((bool)tk2dSpriteColorResponse.Sprite)
				{
					tk2dSpriteColorResponse.Sprite.color = tk2dSpriteColorResponse.Color;
				}
			}
			return;
		}
		OnNotBuildType?.Invoke();
		array = notBuildTypeColor;
		for (int i = 0; i < array.Length; i++)
		{
			Tk2dSpriteColorResponse tk2dSpriteColorResponse2 = array[i];
			if ((bool)tk2dSpriteColorResponse2.Sprite)
			{
				tk2dSpriteColorResponse2.Sprite.color = tk2dSpriteColorResponse2.Color;
			}
		}
	}
}
