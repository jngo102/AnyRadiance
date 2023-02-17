using System;
using System.Collections.Generic;
using UnityEngine;

public class RemapTK2DSpriteAnimator : MonoBehaviour
{
	[Serializable]
	public struct AnimationRemap
	{
		public string sourceClip;

		public string targetClip;
	}

	public tk2dSpriteAnimator sourceAnimator;

	public tk2dSpriteAnimator targetAnimator;

	public bool syncFrames = true;

	public List<AnimationRemap> animationsList = new List<AnimationRemap>();

	private Dictionary<string, string> animations = new Dictionary<string, string>();

	private bool shouldAnimate;

	private string lastSourceClip;

	private void Start()
	{
		if ((bool)sourceAnimator && (bool)targetAnimator)
		{
			shouldAnimate = true;
		}
		foreach (AnimationRemap animations in animationsList)
		{
			this.animations[animations.sourceClip] = animations.targetClip;
		}
	}

	private void Update()
	{
		if (!shouldAnimate)
		{
			return;
		}
		string text = sourceAnimator.CurrentClip.name;
		if (text != lastSourceClip)
		{
			lastSourceClip = text;
			if (animations.ContainsKey(text))
			{
				targetAnimator.PlayFromFrame(animations[text], syncFrames ? sourceAnimator.CurrentFrame : 0);
			}
		}
	}
}
