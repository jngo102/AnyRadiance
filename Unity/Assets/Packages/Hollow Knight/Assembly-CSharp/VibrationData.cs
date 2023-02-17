using System;
using UnityEngine;

[Serializable]
public struct VibrationData
{
	[SerializeField]
	private LowFidelityVibrations lowFidelityVibration;

	[SerializeField]
	private TextAsset highFidelityVibration;

	[SerializeField]
	private GamepadVibration gamepadVibration;

	public LowFidelityVibrations LowFidelityVibration => lowFidelityVibration;

	public TextAsset HighFidelityVibration => highFidelityVibration;

	public GamepadVibration GamepadVibration => gamepadVibration;

	public static VibrationData Create(LowFidelityVibrations lowFidelityVibration = LowFidelityVibrations.None, TextAsset highFidelityVibration = null, GamepadVibration gamepadVibration = null)
	{
		VibrationData result = default(VibrationData);
		result.lowFidelityVibration = lowFidelityVibration;
		result.highFidelityVibration = highFidelityVibration;
		result.gamepadVibration = gamepadVibration;
		return result;
	}
}
