namespace InControl.NativeDeviceProfiles
{
	
	[Preserve]
	[NativeInputDeviceProfile]
	public class PowerAMiniXboxOneControllerMacNativeProfile : XboxOneDriverMacNativeProfile
	{
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Power A Mini Xbox One Controller";
			base.DeviceNotes = "Power A Mini Xbox One Controller on Mac";
			base.Matchers = new InputDeviceMatcher[1]
			{
				new InputDeviceMatcher
				{
					VendorID = (ushort)9414,
					ProductID = (ushort)21562
				}
			};
		}
	}
}