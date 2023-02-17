namespace InControl.NativeDeviceProfiles
{
	
	[Preserve]
	[NativeInputDeviceProfile]
	public class LogitechF310ControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Logitech F310 Controller";
			base.DeviceNotes = "Logitech F310 Controller on Mac";
			base.Matchers = new InputDeviceMatcher[2]
			{
				new InputDeviceMatcher
				{
					VendorID = (ushort)1133,
					ProductID = (ushort)49693
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)1133,
					ProductID = (ushort)49686
				}
			};
		}
	}
}