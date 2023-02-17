public class UnsupportedVibrationEmission : VibrationEmission
{
	private VibrationTarget target;

	private bool isLooping;

	private string tag;

	public override VibrationTarget Target
	{
		get
		{
			return target;
		}
		set
		{
			target = value;
		}
	}

	public override bool IsLooping
	{
		get
		{
			return isLooping;
		}
		set
		{
			isLooping = value;
		}
	}

	public override bool IsPlaying => false;

	public override string Tag
	{
		get
		{
			return tag;
		}
		set
		{
			tag = value ?? "";
		}
	}

	public UnsupportedVibrationEmission(VibrationTarget target, bool isLooping, string tag)
	{
		this.target = target;
		this.isLooping = isLooping;
		this.tag = tag;
	}

	public override void Stop()
	{
	}
}
