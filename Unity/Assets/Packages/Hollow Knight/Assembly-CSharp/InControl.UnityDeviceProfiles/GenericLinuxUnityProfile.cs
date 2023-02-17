namespace InControl.UnityDeviceProfiles
{
	
	[Preserve]
	[UnityInputDeviceProfile]
	public class GenericLinuxUnityProfile : InputDeviceProfile
	{
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Generic Controller";
			base.DeviceNotes = "Generic Controller on Linux";
			base.DeviceClass = InputDeviceClass.Controller;
			base.IncludePlatforms = new string[1] { "Linux" };
			base.Matchers = new InputDeviceMatcher[1]
			{
				new InputDeviceMatcher
				{
					NamePattern = "."
				}
			};
			base.MinUnityVersion = new VersionInfo(5, 0, 0, 0);
			base.ButtonMappings = new InputControlMapping[15]
			{
				new InputControlMapping
				{
					Name = "A",
					Target = InputControlType.Action1,
					Source = InputDeviceProfile.Button(0)
				},
				new InputControlMapping
				{
					Name = "B",
					Target = InputControlType.Action2,
					Source = InputDeviceProfile.Button(1)
				},
				new InputControlMapping
				{
					Name = "X",
					Target = InputControlType.Action3,
					Source = InputDeviceProfile.Button(2)
				},
				new InputControlMapping
				{
					Name = "Y",
					Target = InputControlType.Action4,
					Source = InputDeviceProfile.Button(3)
				},
				new InputControlMapping
				{
					Name = "Left Bumper",
					Target = InputControlType.LeftBumper,
					Source = InputDeviceProfile.Button(4)
				},
				new InputControlMapping
				{
					Name = "Right Bumper",
					Target = InputControlType.RightBumper,
					Source = InputDeviceProfile.Button(5)
				},
				new InputControlMapping
				{
					Name = "Back",
					Target = InputControlType.Back,
					Source = InputDeviceProfile.Button(6)
				},
				new InputControlMapping
				{
					Name = "Start",
					Target = InputControlType.Start,
					Source = InputDeviceProfile.Button(7)
				},
				new InputControlMapping
				{
					Name = "Menu",
					Target = InputControlType.Menu,
					Source = InputDeviceProfile.Button(8)
				},
				new InputControlMapping
				{
					Name = "Left Stick Button",
					Target = InputControlType.LeftStickButton,
					Source = InputDeviceProfile.Button(9)
				},
				new InputControlMapping
				{
					Name = "Right Stick Button",
					Target = InputControlType.RightStickButton,
					Source = InputDeviceProfile.Button(10)
				},
				new InputControlMapping
				{
					Name = "DPad Left",
					Target = InputControlType.DPadLeft,
					Source = InputDeviceProfile.Button(11),
					Invert = true
				},
				new InputControlMapping
				{
					Name = "DPad Right",
					Target = InputControlType.DPadRight,
					Source = InputDeviceProfile.Button(12)
				},
				new InputControlMapping
				{
					Name = "DPad Up",
					Target = InputControlType.DPadUp,
					Source = InputDeviceProfile.Button(13),
					Invert = true
				},
				new InputControlMapping
				{
					Name = "DPad Down",
					Target = InputControlType.DPadDown,
					Source = InputDeviceProfile.Button(14)
				}
			};
			base.AnalogMappings = new InputControlMapping[14]
			{
				InputDeviceProfile.LeftStickLeftMapping(0),
				InputDeviceProfile.LeftStickRightMapping(0),
				InputDeviceProfile.LeftStickUpMapping(1),
				InputDeviceProfile.LeftStickDownMapping(1),
				InputDeviceProfile.RightStickLeftMapping(3),
				InputDeviceProfile.RightStickRightMapping(3),
				InputDeviceProfile.RightStickUpMapping(4),
				InputDeviceProfile.RightStickDownMapping(4),
				InputDeviceProfile.DPadLeftMapping(6),
				InputDeviceProfile.DPadRightMapping(6),
				InputDeviceProfile.DPadUpMapping(7),
				InputDeviceProfile.DPadDownMapping(7),
				new InputControlMapping
				{
					Name = "Left Trigger",
					Target = InputControlType.LeftTrigger,
					Source = InputDeviceProfile.Analog(2)
				},
				new InputControlMapping
				{
					Name = "Right Trigger",
					Target = InputControlType.RightTrigger,
					Source = InputDeviceProfile.Analog(5)
				}
			};
		}
	}
}