namespace InControl.NativeDeviceProfiles
{
	
	[Preserve]
	[NativeInputDeviceProfile]
	public class MicrosoftXboxControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Microsoft Xbox Controller";
			base.DeviceNotes = "Microsoft Xbox Controller on Mac";
			base.Matchers = new InputDeviceMatcher[7]
			{
				new InputDeviceMatcher
				{
					VendorID = ushort.MaxValue,
					ProductID = ushort.MaxValue
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)1118,
					ProductID = (ushort)649
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)1118,
					ProductID = (ushort)648
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)1118,
					ProductID = (ushort)645
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)1118,
					ProductID = (ushort)514
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)1118,
					ProductID = (ushort)647
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)1118,
					ProductID = (ushort)648
				}
			};
		}
	}
}