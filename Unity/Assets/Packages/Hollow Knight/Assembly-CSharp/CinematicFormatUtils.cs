public static class CinematicFormatUtils
{
	public static string GetExtension(CinematicFormats format)
	{
		switch (format)
		{
		case CinematicFormats.MP4_H264_720_60_AAC_48000:
		case CinematicFormats.MP4_H264_1080_Any_AAC_48000:
			return ".mp4";
		case CinematicFormats.WEBM_VP8_1080_Any_Vorbis_48000:
			return ".webm";
		default:
			return "";
		}
	}
}
