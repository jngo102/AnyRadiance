using InControl;
using UnityEngine;

public class NativeInputModuleManager : MonoBehaviour
{
	private static NativeInputModuleManager _instance;

	private static bool _isUsedAtStart;

	private static bool _isUsed;

	public static bool IsUsed
	{
		get
		{
			return _isUsed;
		}
		set
		{
			ChangeIsUsed(value);
		}
	}

	public static bool IsRestartRequired => _isUsedAtStart != _isUsed;

	private void Awake()
	{
		if (_instance != null)
		{
			Object.Destroy(this);
		}
		else
		{
			_instance = this;
		}
	}

	private void OnDestroy()
	{
		if (_instance == this)
		{
			_instance = null;
		}
	}

	protected void OnEnable()
	{
		if (!(_instance != this))
		{
			_isUsedAtStart = Platform.Current.SharedData.GetInt("NativeInput", 1) > 0;
			InControlManager component = GetComponent<InControlManager>();
			if (component == null)
			{
				Debug.LogError("Unable to find input manager.");
				return;
			}
			if (InputManager.IsSetup)
			{
				Debug.LogError("Too late to enable native input module.");
				return;
			}
			component.enableXInput = _isUsedAtStart;
			component.enableNativeInput = _isUsedAtStart;
			component.nativeInputEnableXInput = _isUsedAtStart;
			_isUsed = _isUsedAtStart;
		}
	}

	private static void ChangeIsUsed(bool willUse)
	{
		if (_isUsed != willUse)
		{
			_isUsed = willUse;
			Platform.Current.SharedData.SetInt("NativeInput", _isUsed ? 1 : 0);
		}
	}
}
