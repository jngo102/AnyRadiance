using System;

public struct PlayTime
{
	public float RawTime;

	private TimeSpan time => TimeSpan.FromSeconds(RawTime);

	public float Hours => (float)Math.Floor(time.TotalHours);

	public float Minutes => time.Minutes;

	public float Seconds => time.Seconds;

	public bool HasHours => time.TotalHours >= 1.0;

	public bool HasMinutes => time.TotalMinutes >= 1.0;
}
