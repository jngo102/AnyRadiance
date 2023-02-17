namespace InControl.NativeDeviceProfiles
{
	
	[Preserve]
	[NativeInputDeviceProfile]
	public class PDPBattlefieldXBoxOneControllerMacNativeProfile : XboxOneDriverMacNativeProfile
	{
		public override void Define()
		{
			base.Define();
			base.DeviceName = "PDP Battlefield XBox One Controller";
			base.DeviceNotes = "PDP Battlefield XBox One Controller on Mac";
			base.Matchers = new InputDeviceMatcher[1]
			{
				new InputDeviceMatcher
				{
					VendorID = (ushort)3695,
					ProductID = (ushort)356
				}
			};
		}
	}
}