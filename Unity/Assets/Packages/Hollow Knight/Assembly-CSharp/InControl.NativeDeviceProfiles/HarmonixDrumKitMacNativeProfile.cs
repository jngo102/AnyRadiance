namespace InControl.NativeDeviceProfiles
{
	
	[Preserve]
	[NativeInputDeviceProfile]
	public class HarmonixDrumKitMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Harmonix Drum Kit";
			base.DeviceNotes = "Harmonix Drum Kit on Mac";
			base.Matchers = new InputDeviceMatcher[1]
			{
				new InputDeviceMatcher
				{
					VendorID = (ushort)7085,
					ProductID = (ushort)4408
				}
			};
		}
	}
}