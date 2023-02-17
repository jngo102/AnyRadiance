using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class CinematicSequence : SkippableSequence
{
	private AudioSource audioSource;

	[SerializeField]
	private AudioMixerSnapshot atmosSnapshot;

	[SerializeField]
	private float atmosSnapshotTransitionDuration;

	[SerializeField]
	private CinematicVideoReference videoReference;

	[SerializeField]
	private bool isLooping;

	[SerializeField]
	private MeshRenderer targetRenderer;

	[SerializeField]
	private MeshRenderer blankerRenderer;

	private CinematicVideoPlayer videoPlayer;

	private bool didPlay;

	private bool isSkipped;

	private int framesSinceBegan;

	private float fadeByController;

	public CinematicVideoPlayer VideoPlayer => videoPlayer;

	public override bool IsSkipped => isSkipped;

	public bool IsLooping
	{
		get
		{
			if (videoPlayer != null)
			{
				return videoPlayer.IsLooping;
			}
			return isLooping;
		}
		set
		{
			if (videoPlayer != null)
			{
				videoPlayer.IsLooping = value;
			}
			isLooping = value;
		}
	}

	public override bool IsPlaying
	{
		get
		{
			bool flag = framesSinceBegan < 10 || !didPlay;
			if (!isSkipped)
			{
				if (!flag)
				{
					if (videoPlayer != null)
					{
						return videoPlayer.IsPlaying;
					}
					return false;
				}
				return true;
			}
			return false;
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
			fadeByController = value;
			if (videoPlayer != null)
			{
				videoPlayer.Volume = fadeByController;
			}
			UpdateBlanker(1f - fadeByController);
		}
	}

	protected void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		fadeByController = 1f;
	}

	protected void OnDestroy()
	{
		if (videoPlayer != null)
		{
			videoPlayer.Dispose();
			videoPlayer = null;
		}
	}

	protected void Update()
	{
		if (videoPlayer == null)
		{
			return;
		}
		framesSinceBegan++;
		videoPlayer.Update();
		if (!videoPlayer.IsLoading && !didPlay)
		{
			didPlay = true;
			if (atmosSnapshot != null)
			{
				atmosSnapshot.TransitionTo(atmosSnapshotTransitionDuration);
			}
			Debug.LogFormat(this, "Started cinematic '{0}'", videoReference.name);
			videoPlayer.Play();
		}
		if (!videoPlayer.IsPlaying && !videoPlayer.IsLoading && framesSinceBegan >= 10)
		{
			Debug.LogFormat(this, "Stopped cinematic '{0}'", videoReference.name);
			videoPlayer.Dispose();
			videoPlayer = null;
			targetRenderer.enabled = false;
		}
		else if (isSkipped)
		{
			Debug.LogFormat(this, "Skipped cinematic '{0}'", videoReference.name);
			videoPlayer.Stop();
		}
	}

	public override void Begin()
	{
		if (videoPlayer != null && videoPlayer.IsPlaying)
		{
			Debug.LogErrorFormat(this, "Can't play a cinematic sequence that is already playing");
			return;
		}
		if (videoPlayer != null)
		{
			videoPlayer.Dispose();
			videoPlayer = null;
			targetRenderer.enabled = false;
		}
		targetRenderer.enabled = true;
		videoPlayer = CinematicVideoPlayer.Create(new CinematicVideoPlayerConfig(videoReference, targetRenderer, audioSource, CinematicVideoFaderStyles.Black, GameManager.instance.GetImplicitCinematicVolume()));
		videoPlayer.IsLooping = isLooping;
		videoPlayer.Volume = fadeByController;
		isSkipped = false;
		framesSinceBegan = 0;
		UpdateBlanker(1f - fadeByController);
		Debug.LogFormat(this, "Started cinematic '{0}'", videoReference.name);
	}

	private void UpdateBlanker(float alpha)
	{
		if (alpha > Mathf.Epsilon)
		{
			if (!blankerRenderer.enabled)
			{
				blankerRenderer.enabled = true;
			}
			blankerRenderer.material.color = new Color(0f, 0f, 0f, alpha);
		}
		else if (blankerRenderer.enabled)
		{
			blankerRenderer.enabled = false;
		}
	}

	public override void Skip()
	{
		isSkipped = true;
	}
}
