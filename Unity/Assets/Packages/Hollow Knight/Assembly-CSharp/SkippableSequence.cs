using UnityEngine;

public abstract class SkippableSequence : MonoBehaviour
{
	public abstract bool IsPlaying { get; }

	public abstract bool IsSkipped { get; }

	public abstract float FadeByController { get; set; }

	public abstract void Begin();

	public abstract void Skip();
}
