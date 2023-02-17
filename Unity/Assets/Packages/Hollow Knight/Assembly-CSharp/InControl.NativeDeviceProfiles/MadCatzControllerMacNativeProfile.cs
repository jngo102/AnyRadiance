namespace InControl.NativeDeviceProfiles
{
	
	[Preserve]
	[NativeInputDeviceProfile]
	public class MadCatzControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Mad Catz Controller";
			base.DeviceNotes = "Mad Catz Controller on Mac";
			base.Matchers = new InputDeviceMatcher[4]
			{
				new InputDeviceMatcher
				{
					VendorID = (ushort)1848,
					ProductID = (ushort)18198
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)7085,
					ProductID = (ushort)63746
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)7085,
					ProductID = (ushort)61642
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)1848,
					ProductID = (ushort)672
				}
			};
		}
	}
}