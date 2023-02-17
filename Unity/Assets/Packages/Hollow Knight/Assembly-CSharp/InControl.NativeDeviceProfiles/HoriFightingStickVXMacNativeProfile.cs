namespace InControl.NativeDeviceProfiles
{
	
	[Preserve]
	[NativeInputDeviceProfile]
	public class HoriFightingStickVXMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Hori Fighting Stick VX";
			base.DeviceNotes = "Hori Fighting Stick VX on Mac";
			base.Matchers = new InputDeviceMatcher[2]
			{
				new InputDeviceMatcher
				{
					VendorID = (ushort)7085,
					ProductID = (ushort)62723
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)9414,
					ProductID = (ushort)21762
				}
			};
		}
	}
}