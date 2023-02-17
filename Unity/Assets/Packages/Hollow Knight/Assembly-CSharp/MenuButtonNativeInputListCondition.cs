using InControl;

public class MenuButtonNativeInputListCondition : MenuButtonListCondition
{
	public override bool IsFulfilled()
	{
		return NativeInputDeviceManager.CheckPlatformSupport(null);
	}
}
