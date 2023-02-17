using UnityEngine;

public class ChainSequence : SkippableSequence
{
	public delegate void TransitionedToNextSequenceDelegate();

	[SerializeField]
	private SkippableSequence[] sequences;

	private int currentSequenceIndex;

	private float fadeByController;

	private bool isSkipped;

	private SkippableSequence CurrentSequence
	{
		get
		{
			if (currentSequenceIndex < 0 || currentSequenceIndex >= sequences.Length)
			{
				return null;
			}
			return sequences[currentSequenceIndex];
		}
	}

	public bool IsCurrentSkipped
	{
		get
		{
			if (CurrentSequence != null)
			{
				return CurrentSequence.IsSkipped;
			}
			return false;
		}
	}

	public override bool IsSkipped => isSkipped;

	public override bool IsPlaying
	{
		get
		{
			if (currentSequenceIndex < sequences.Length - 1)
			{
				return true;
			}
			if (CurrentSequence == null)
			{
				return false;
			}
			return CurrentSequence.IsPlaying;
		}
	}

	public override float FadeByController
	{
		get
		{
			return fadeByController;
		}
		set
		{
			fadeByController = Mathf.Clamp01(value);
			for (int i = 0; i < sequences.Length; i++)
			{
				sequences[i].FadeByController = fadeByController;
			}
		}
	}

	public event TransitionedToNextSequenceDelegate TransitionedToNextSequence;

	protected void Awake()
	{
		fadeByController = 1f;
	}

	protected void Update()
	{
		if (CurrentSequence != null && !CurrentSequence.IsPlaying && !isSkipped)
		{
			Next();
		}
	}

	public override void Begin()
	{
		isSkipped = false;
		currentSequenceIndex = -1;
		Next();
	}

	private void Next()
	{
		SkippableSequence currentSequence = CurrentSequence;
		if (currentSequence != null)
		{
			currentSequence.gameObject.SetActive(value: false);
		}
		currentSequenceIndex++;
		if (!isSkipped)
		{
			if (CurrentSequence != null)
			{
				CurrentSequence.gameObject.SetActive(value: true);
				CurrentSequence.Begin();
			}
			if (this.TransitionedToNextSequence != null)
			{
				this.TransitionedToNextSequence();
			}
		}
	}

	public override void Skip()
	{
		isSkipped = true;
		for (int i = 0; i < sequences.Length; i++)
		{
			sequences[i].Skip();
		}
	}

	public void SkipSingle()
	{
		if (CurrentSequence != null)
		{
			CurrentSequence.Skip();
		}
	}
}
