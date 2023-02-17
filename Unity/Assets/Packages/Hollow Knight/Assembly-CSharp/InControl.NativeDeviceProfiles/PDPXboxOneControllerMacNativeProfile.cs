namespace InControl.NativeDeviceProfiles
{
	
	[Preserve]
	[NativeInputDeviceProfile]
	public class PDPXboxOneControllerMacNativeProfile : XboxOneDriverMacNativeProfile
	{
		public override void Define()
		{
			base.Define();
			base.DeviceName = "PDP Xbox One Controller";
			base.DeviceNotes = "PDP Xbox One Controller on Mac";
			base.Matchers = new InputDeviceMatcher[17]
			{
				new InputDeviceMatcher
				{
					VendorID = (ushort)3695,
					ProductID = (ushort)676
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)3695,
					ProductID = (ushort)715
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)3695,
					ProductID = (ushort)314
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)3695,
					ProductID = (ushort)354
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)9414,
					ProductID = (ushort)22042
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)3695,
					ProductID = (ushort)353
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)3695,
					ProductID = (ushort)355
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)3695,
					ProductID = (ushort)683
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)3695,
					ProductID = (ushort)352
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)3695,
					ProductID = (ushort)680
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)3695,
					ProductID = (ushort)674
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)3695,
					ProductID = (ushort)347
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)3695,
					ProductID = (ushort)677
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)3695,
					ProductID = (ushort)685
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)3695,
					ProductID = (ushort)704
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)3695,
					ProductID = (ushort)679
				},
				new InputDeviceMatcher
				{
					VendorID = (ushort)3695,
					ProductID = (ushort)678
				}
			};
		}
	}
}