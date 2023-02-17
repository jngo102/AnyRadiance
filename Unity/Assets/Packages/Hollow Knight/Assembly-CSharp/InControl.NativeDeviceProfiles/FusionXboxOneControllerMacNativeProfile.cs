namespace InControl.NativeDeviceProfiles
{
	
	[Preserve]
	[NativeInputDeviceProfile]
	public class FusionXboxOneControllerMacNativeProfile : XboxOneDriverMacNativeProfile
	{
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Fusion Xbox One Controller";
			base.DeviceNotes = "Fusion Xbox One Controller on Mac";
			base.Matchers = new InputDeviceMatcher[2]
			{
				new InputDeviceMatcher
				{
					VendorID = (ushort)9414,
					ProductID = (ushort)21786
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)9414,
					ProductID = (ushort)22042
				}
			};
		}
	}
}