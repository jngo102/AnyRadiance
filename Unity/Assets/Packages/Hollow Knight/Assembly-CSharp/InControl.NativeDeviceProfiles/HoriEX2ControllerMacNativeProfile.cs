namespace InControl.NativeDeviceProfiles
{
	
	[Preserve]
	[NativeInputDeviceProfile]
	public class HoriEX2ControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Hori EX2 Controller";
			base.DeviceNotes = "Hori EX2 Controller on Mac";
			base.Matchers = new InputDeviceMatcher[3]
			{
				new InputDeviceMatcher
				{
					VendorID = (ushort)3853,
					ProductID = (ushort)13
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)7085,
					ProductID = (ushort)62721
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)9414,
					ProductID = (ushort)21760
				}
			};
		}
	}
}