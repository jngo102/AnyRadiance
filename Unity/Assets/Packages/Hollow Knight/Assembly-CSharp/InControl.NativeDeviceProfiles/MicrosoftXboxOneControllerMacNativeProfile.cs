namespace InControl.NativeDeviceProfiles
{
	
	[Preserve]
	[NativeInputDeviceProfile]
	public class MicrosoftXboxOneControllerMacNativeProfile : XboxOneDriverMacNativeProfile
	{
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Microsoft Xbox One Controller";
			base.DeviceNotes = "Microsoft Xbox One Controller on Mac";
			base.Matchers = new InputDeviceMatcher[2]
			{
				new InputDeviceMatcher
				{
					VendorID = (ushort)1118,
					ProductID = (ushort)721
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)1118,
					ProductID = (ushort)733
				}
			};
		}
	}
}