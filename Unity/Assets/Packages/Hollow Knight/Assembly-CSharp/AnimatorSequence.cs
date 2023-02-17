using UnityEngine;

public class AnimatorSequence : SkippableSequence
{
	[SerializeField]
	private Animator animator;

	[SerializeField]
	private string animatorStateName;

	[SerializeField]
	private float normalizedFinishTime;

	private float fadeByController;

	private bool isSkipped;

	public override bool IsPlaying
	{
		get
		{
			if (animator.isActiveAndEnabled)
			{
				return animator.GetCurrentAnimatorStateInfo(0).normalizedTime < Mathf.Min(normalizedFinishTime, 1f - Mathf.Epsilon);
			}
			return false;
		}
	}

	public override bool IsSkipped => isSkipped;

	public override float FadeByController
	{
		get
		{
			return fadeByController;
		}
		set
		{
			fadeByController = value;
		}
	}

	protected void Awake()
	{
		fadeByController = 1f;
	}

	protected void Update()
	{
		if (animator.isActiveAndEnabled && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= Mathf.Min(normalizedFinishTime, 1f - Mathf.Epsilon))
		{
			animator.gameObject.SetActive(value: false);
		}
	}

	public override void Begin()
	{
		animator.gameObject.SetActive(value: true);
		animator.Play(animatorStateName, 0, 0f);
	}

	public override void Skip()
	{
		isSkipped = true;
		animator.Update(1000f);
	}
}
