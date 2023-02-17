using UnityEngine;

public static class DirectionUtils
{
	public const int Right = 0;

	public const int Up = 1;

	public const int Left = 2;

	public const int Down = 3;

	public static int GetCardinalDirection(float degrees)
	{
		return NegSafeMod(Mathf.RoundToInt(degrees / 90f), 4);
	}

	public static int NegSafeMod(int val, int len)
	{
		return (val % len + len) % len;
	}

	public static int GetX(int cardinalDirection)
	{
		return (cardinalDirection % 4) switch
		{
			0 => 1, 
			2 => -1, 
			_ => 0, 
		};
	}

	public static int GetY(int cardinalDirection)
	{
		return (cardinalDirection % 4) switch
		{
			1 => 1, 
			3 => -1, 
			_ => 0, 
		};
	}
}
