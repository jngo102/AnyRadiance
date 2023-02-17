namespace InControl
{
	
	public static class InputDeviceStyleExtensions
	{
		private const InputControlType defaultLeftCommandControl = InputControlType.Select;
	
		private const InputControlType defaultRightCommandControl = InputControlType.Start;
	
		public static InputControlType LeftCommandControl(this InputDeviceStyle deviceStyle)
		{
			switch (deviceStyle)
			{
			case InputDeviceStyle.Xbox360:
				return InputControlType.Back;
			case InputDeviceStyle.XboxOne:
			case InputDeviceStyle.XboxSeriesX:
				return InputControlType.View;
			case InputDeviceStyle.PlayStation2:
			case InputDeviceStyle.PlayStation3:
			case InputDeviceStyle.PlayStationVita:
				return InputControlType.Select;
			case InputDeviceStyle.PlayStation4:
				return InputControlType.Share;
			case InputDeviceStyle.PlayStation5:
				return InputControlType.Create;
			case InputDeviceStyle.Steam:
				return InputControlType.Back;
			case InputDeviceStyle.AppleMFi:
				return InputControlType.Menu;
			case InputDeviceStyle.AmazonFireTV:
				return InputControlType.Back;
			case InputDeviceStyle.NVIDIAShield:
				return InputControlType.Back;
			case InputDeviceStyle.NintendoNES:
			case InputDeviceStyle.NintendoSNES:
				return InputControlType.Select;
			case InputDeviceStyle.NintendoWii:
			case InputDeviceStyle.NintendoWiiU:
			case InputDeviceStyle.NintendoSwitch:
				return InputControlType.Minus;
			case InputDeviceStyle.GoogleStadia:
				return InputControlType.Options;
			case InputDeviceStyle.PlayStationMove:
			case InputDeviceStyle.Ouya:
			case InputDeviceStyle.Nintendo64:
			case InputDeviceStyle.NintendoGameCube:
				return InputControlType.None;
			default:
				return InputControlType.Select;
			}
		}
	
		public static InputControlType RightCommandControl(this InputDeviceStyle deviceStyle)
		{
			switch (deviceStyle)
			{
			case InputDeviceStyle.Xbox360:
				return InputControlType.Start;
			case InputDeviceStyle.XboxOne:
			case InputDeviceStyle.XboxSeriesX:
				return InputControlType.Menu;
			case InputDeviceStyle.PlayStation2:
			case InputDeviceStyle.PlayStation3:
			case InputDeviceStyle.PlayStationVita:
				return InputControlType.Start;
			case InputDeviceStyle.PlayStation4:
			case InputDeviceStyle.PlayStation5:
				return InputControlType.Options;
			case InputDeviceStyle.Steam:
				return InputControlType.Start;
			case InputDeviceStyle.AppleMFi:
				return InputControlType.Options;
			case InputDeviceStyle.AmazonFireTV:
				return InputControlType.Menu;
			case InputDeviceStyle.NVIDIAShield:
				return InputControlType.Start;
			case InputDeviceStyle.NintendoNES:
			case InputDeviceStyle.NintendoSNES:
			case InputDeviceStyle.Nintendo64:
			case InputDeviceStyle.NintendoGameCube:
				return InputControlType.Start;
			case InputDeviceStyle.NintendoWii:
			case InputDeviceStyle.NintendoWiiU:
			case InputDeviceStyle.NintendoSwitch:
				return InputControlType.Plus;
			case InputDeviceStyle.GoogleStadia:
				return InputControlType.Menu;
			case InputDeviceStyle.Ouya:
				return InputControlType.Menu;
			case InputDeviceStyle.PlayStationMove:
				return InputControlType.None;
			default:
				return InputControlType.Start;
			}
		}
	}
}