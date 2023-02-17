namespace InControl.NativeDeviceProfiles
{
	
	[Preserve]
	[NativeInputDeviceProfile]
	public class RazerOnzaTEControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Razer Onza TE Controller";
			base.DeviceNotes = "Razer Onza TE Controller on Mac";
			base.Matchers = new InputDeviceMatcher[2]
			{
				new InputDeviceMatcher
				{
					VendorID = (ushort)7085,
					ProductID = (ushort)64768
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)5769,
					ProductID = (ushort)64768
				}
			};
		}
	}
}