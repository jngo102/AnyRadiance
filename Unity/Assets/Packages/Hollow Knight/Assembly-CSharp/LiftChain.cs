using UnityEngine;

[DisallowMultipleComponent]
public class LiftChain : MonoBehaviour
{
	private tk2dSpriteAnimator[] spriteAnimators;

	private int currentDirection;

	protected void Awake()
	{
		spriteAnimators = GetComponentsInChildren<tk2dSpriteAnimator>();
		currentDirection = 0;
	}

	public void GoDown()
	{
		Debug.LogFormat(this, "Chain {0} going down.", base.name);
		for (int i = 0; i < spriteAnimators.Length; i++)
		{
			tk2dSpriteAnimator tk2dSpriteAnimator2 = spriteAnimators[i];
			tk2dSpriteAnimator2.Resume();
			if (currentDirection != -1)
			{
				tk2dSpriteAnimator2.Play("Chain Down");
			}
		}
		currentDirection = -1;
	}

	public void GoUp()
	{
		Debug.LogFormat(this, "Chain {0} going up.", base.name);
		for (int i = 0; i < spriteAnimators.Length; i++)
		{
			tk2dSpriteAnimator tk2dSpriteAnimator2 = spriteAnimators[i];
			tk2dSpriteAnimator2.Resume();
			if (currentDirection != 1)
			{
				tk2dSpriteAnimator2.Play("Chain Up");
			}
		}
		currentDirection = 1;
	}

	public void Stop()
	{
		Debug.LogFormat(this, "Chain {0} stopping.", base.name);
		for (int i = 0; i < spriteAnimators.Length; i++)
		{
			spriteAnimators[i].Pause();
		}
	}
}
