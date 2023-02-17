namespace InControl.NativeDeviceProfiles
{
	
	[Preserve]
	[NativeInputDeviceProfile]
	public class RazerStrikeControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Razer Strike Controller";
			base.DeviceNotes = "Razer Strike Controller on Mac";
			base.Matchers = new InputDeviceMatcher[1]
			{
				new InputDeviceMatcher
				{
					VendorID = (ushort)5769,
					ProductID = (ushort)1
				}
			};
		}
	}
}