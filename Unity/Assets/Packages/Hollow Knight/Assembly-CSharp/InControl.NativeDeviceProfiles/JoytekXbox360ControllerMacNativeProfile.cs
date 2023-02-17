namespace InControl.NativeDeviceProfiles
{
	
	[Preserve]
	[NativeInputDeviceProfile]
	public class JoytekXbox360ControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Joytek Xbox 360 Controller";
			base.DeviceNotes = "Joytek Xbox 360 Controller on Mac";
			base.Matchers = new InputDeviceMatcher[1]
			{
				new InputDeviceMatcher
				{
					VendorID = (ushort)5678,
					ProductID = (ushort)48879
				}
			};
		}
	}
}