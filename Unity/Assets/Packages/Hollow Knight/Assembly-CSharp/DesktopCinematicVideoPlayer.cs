using System.IO;
using UnityEngine;

public class DesktopCinematicVideoPlayer : EmbeddedCinematicVideoPlayer
{
	public DesktopCinematicVideoPlayer(CinematicVideoPlayerConfig config)
		: base(config)
	{
	}

	protected override string GetAbsolutePath()
	{
		CinematicFormats format;
		switch (Application.platform)
		{
		case RuntimePlatform.WindowsPlayer:
		case RuntimePlatform.WindowsEditor:
			format = CinematicFormats.MP4_H264_1080_Any_AAC_48000;
			break;
		default:
			format = CinematicFormats.WEBM_VP8_1080_Any_Vorbis_48000;
			break;
		}
		return Path.GetFullPath(Path.Combine(Application.streamingAssetsPath, base.Config.VideoReference.VideoFileName + CinematicFormatUtils.GetExtension(format)));
	}
}
