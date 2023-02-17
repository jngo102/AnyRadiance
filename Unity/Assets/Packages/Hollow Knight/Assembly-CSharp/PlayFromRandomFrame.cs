using UnityEngine;

[RequireComponent(typeof(tk2dSpriteAnimator))]
public class PlayFromRandomFrame : MonoBehaviour
{
	[Tooltip("Number of frames in animation.")]
	public int frameCount;

	private tk2dSpriteAnimator animator;

	private void Start()
	{
		int frame = Random.Range(0, frameCount);
		animator = GetComponent<tk2dSpriteAnimator>();
		animator.PlayFromFrame(frame);
	}
}
