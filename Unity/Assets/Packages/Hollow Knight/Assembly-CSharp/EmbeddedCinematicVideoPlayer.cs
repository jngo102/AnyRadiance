using System;
using System.IO;
using UnityEngine;
using UnityEngine.Video;

public abstract class EmbeddedCinematicVideoPlayer : CinematicVideoPlayer
{
	private VideoPlayer videoPlayer;

	private Texture originalMainTexture;

	private const string TexturePropertyName = "_MainTex";

	private bool isPlayEnqueued;

	public override float Volume
	{
		get
		{
			if (base.Config.AudioSource != null)
			{
				return base.Config.AudioSource.volume;
			}
			return 1f;
		}
		set
		{
			if (base.Config.AudioSource != null)
			{
				base.Config.AudioSource.volume = value;
			}
		}
	}

	public override bool IsLoading => false;

	public override bool IsLooping
	{
		get
		{
			if (videoPlayer != null)
			{
				return videoPlayer.isLooping;
			}
			return false;
		}
		set
		{
			if (videoPlayer != null)
			{
				videoPlayer.isLooping = value;
			}
		}
	}

	public override bool IsPlaying
	{
		get
		{
			if (videoPlayer != null && videoPlayer.isPrepared)
			{
				return videoPlayer.isPlaying;
			}
			return isPlayEnqueued;
		}
	}

	public EmbeddedCinematicVideoPlayer(CinematicVideoPlayerConfig config)
		: base(config)
	{
		originalMainTexture = config.MeshRenderer.material.GetTexture("_MainTex");
		videoPlayer = config.MeshRenderer.gameObject.AddComponent<VideoPlayer>();
		videoPlayer.playOnAwake = false;
		videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
		videoPlayer.SetTargetAudioSource(0, config.AudioSource);
		videoPlayer.renderMode = VideoRenderMode.MaterialOverride;
		videoPlayer.targetMaterialRenderer = config.MeshRenderer;
		videoPlayer.targetMaterialProperty = "_MainTex";
		if (File.Exists(GetAbsolutePath()))
		{
			videoPlayer.url = new Uri(GetAbsolutePath()).AbsoluteUri;
		}
		else
		{
			VideoClip embeddedVideoClip = config.VideoReference.EmbeddedVideoClip;
			videoPlayer.clip = embeddedVideoClip;
		}
		videoPlayer.prepareCompleted += OnPrepareCompleted;
		videoPlayer.Prepare();
	}

	protected abstract string GetAbsolutePath();

	public override void Dispose()
	{
		base.Dispose();
		if (videoPlayer != null)
		{
			videoPlayer.Stop();
			UnityEngine.Object.Destroy(videoPlayer);
			videoPlayer = null;
			MeshRenderer meshRenderer = base.Config.MeshRenderer;
			if (meshRenderer != null)
			{
				meshRenderer.material.SetTexture("_MainTex", originalMainTexture);
			}
		}
	}

	public override void Play()
	{
		if (videoPlayer != null && videoPlayer.isPrepared)
		{
			videoPlayer.Play();
		}
		isPlayEnqueued = true;
	}

	public override void Stop()
	{
		if (videoPlayer != null)
		{
			videoPlayer.Stop();
		}
		isPlayEnqueued = false;
	}

	private void OnPrepareCompleted(VideoPlayer source)
	{
		if (source == videoPlayer && videoPlayer != null && isPlayEnqueued)
		{
			videoPlayer.Play();
			isPlayEnqueued = false;
		}
	}
}
