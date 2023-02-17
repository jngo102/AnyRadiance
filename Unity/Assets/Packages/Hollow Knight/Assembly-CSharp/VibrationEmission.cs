public abstract class VibrationEmission
{
	public abstract VibrationTarget Target { get; set; }

	public abstract bool IsLooping { get; set; }

	public abstract string Tag { get; set; }

	public abstract bool IsPlaying { get; }

	public abstract void Stop();
}
