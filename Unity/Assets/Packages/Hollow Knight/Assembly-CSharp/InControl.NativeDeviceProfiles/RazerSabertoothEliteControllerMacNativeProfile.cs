namespace InControl.NativeDeviceProfiles
{
	
	[Preserve]
	[NativeInputDeviceProfile]
	public class RazerSabertoothEliteControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Razer Sabertooth Elite Controller";
			base.DeviceNotes = "Razer Sabertooth Elite Controller on Mac";
			base.Matchers = new InputDeviceMatcher[2]
			{
				new InputDeviceMatcher
				{
					VendorID = (ushort)5769,
					ProductID = (ushort)65024
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)9414,
					ProductID = (ushort)23812
				}
			};
		}
	}
}