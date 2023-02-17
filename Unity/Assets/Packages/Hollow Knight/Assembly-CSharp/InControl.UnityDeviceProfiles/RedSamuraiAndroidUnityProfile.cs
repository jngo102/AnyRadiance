namespace InControl.UnityDeviceProfiles
{
	
	[Preserve]
	[UnityInputDeviceProfile]
	public class RedSamuraiAndroidUnityProfile : InputDeviceProfile
	{
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Red Samurai Wireless Controller";
			base.DeviceNotes = "Red Samurai Wireless Controller on Android";
			base.DeviceClass = InputDeviceClass.Controller;
			base.IncludePlatforms = new string[1] { "Android" };
			base.Matchers = new InputDeviceMatcher[1]
			{
				new InputDeviceMatcher
				{
					NameLiteral = "GS Gamepad"
				}
			};
			base.ButtonMappings = new InputControlMapping[12]
			{
				new InputControlMapping
				{
					Name = "1",
					Target = InputControlType.Action1,
					Source = InputDeviceProfile.Button(0)
				},
				new InputControlMapping
				{
					Name = "2",
					Target = InputControlType.Action2,
					Source = InputDeviceProfile.Button(1)
				},
				new InputControlMapping
				{
					Name = "3",
					Target = InputControlType.Action3,
					Source = InputDeviceProfile.Button(2)
				},
				new InputControlMapping
				{
					Name = "4",
					Target = InputControlType.Action4,
					Source = InputDeviceProfile.Button(3)
				},
				new InputControlMapping
				{
					Name = "Escape",
					Target = InputControlType.Select,
					Source = InputDeviceProfile.Button(11)
				},
				new InputControlMapping
				{
					Name = "Enter",
					Target = InputControlType.Start,
					Source = InputDeviceProfile.Button(10)
				},
				new InputControlMapping
				{
					Name = "L1",
					Target = InputControlType.LeftBumper,
					Source = InputDeviceProfile.Button(4)
				},
				new InputControlMapping
				{
					Name = "L2",
					Target = InputControlType.LeftTrigger,
					Source = InputDeviceProfile.Button(6)
				},
				new InputControlMapping
				{
					Name = "R1",
					Target = InputControlType.RightBumper,
					Source = InputDeviceProfile.Button(5)
				},
				new InputControlMapping
				{
					Name = "R2",
					Target = InputControlType.RightTrigger,
					Source = InputDeviceProfile.Button(7)
				},
				new InputControlMapping
				{
					Name = "Left Stick Button",
					Target = InputControlType.LeftStickButton,
					Source = InputDeviceProfile.Button(8)
				},
				new InputControlMapping
				{
					Name = "Right Stick Button",
					Target = InputControlType.RightStickButton,
					Source = InputDeviceProfile.Button(9)
				}
			};
			base.AnalogMappings = new InputControlMapping[12]
			{
				InputDeviceProfile.LeftStickLeftMapping(0),
				InputDeviceProfile.LeftStickRightMapping(0),
				InputDeviceProfile.LeftStickUpMapping(1),
				InputDeviceProfile.LeftStickDownMapping(1),
				InputDeviceProfile.RightStickLeftMapping(2),
				InputDeviceProfile.RightStickRightMapping(2),
				InputDeviceProfile.RightStickUpMapping(3),
				InputDeviceProfile.RightStickDownMapping(3),
				InputDeviceProfile.DPadLeftMapping(4),
				InputDeviceProfile.DPadRightMapping(4),
				InputDeviceProfile.DPadUpMapping(5),
				InputDeviceProfile.DPadDownMapping(5)
			};
		}
	}
}