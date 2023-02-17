using System;
using HutongGames.PlayMaker;
using UnityEngine;
using UnityEngine.UI;

public class PlayMakerUGuiComponentProxy : MonoBehaviour
{
	public enum ActionType
	{
		SendFsmEvent,
		SetFsmVariable
	}

	public enum PlayMakerProxyEventTarget
	{
		Owner,
		GameObject,
		BroadCastAll,
		FsmComponent
	}

	public enum PlayMakerProxyVariableTarget
	{
		Owner,
		GameObject,
		GlobalVariable,
		FsmComponent
	}

	[Serializable]
	public struct FsmVariableSetup
	{
		public PlayMakerProxyVariableTarget target;

		public GameObject gameObject;

		public PlayMakerFSM fsmComponent;

		public int fsmIndex;

		public int variableIndex;

		public VariableType variableType;

		public string variableName;
	}

	[Serializable]
	public struct FsmEventSetup
	{
		public PlayMakerProxyEventTarget target;

		public GameObject gameObject;

		public PlayMakerFSM fsmComponent;

		public string customEventName;

		public string builtInEventName;
	}

	public bool debug;

	private string error;

	public OwnerDefaultOption UiTargetOption;

	public GameObject UiTarget;

	public ActionType action;

	public FsmVariableSetup fsmVariableSetup;

	private FsmFloat fsmFloatTarget;

	private FsmBool fsmBoolTarget;

	private FsmVector2 fsmVector2Target;

	private FsmString fsmStringTarget;

	public FsmEventSetup fsmEventSetup;

	private FsmEventTarget fsmEventTarget;

	private bool WatchInputField;

	private InputField inputField;

	private string lastInputFieldValue;

	private void Start()
	{
		if (action == ActionType.SetFsmVariable)
		{
			SetupVariableTarget();
		}
		else
		{
			SetupEventTarget();
		}
		SetupUiListeners();
	}

	private void Update()
	{
		if (WatchInputField && inputField != null && !inputField.text.Equals(lastInputFieldValue))
		{
			lastInputFieldValue = inputField.text;
			SetFsmVariable(lastInputFieldValue);
		}
	}

	private void SetupEventTarget()
	{
		if (fsmEventTarget == null)
		{
			fsmEventTarget = new FsmEventTarget();
		}
		if (fsmEventSetup.target == PlayMakerProxyEventTarget.BroadCastAll)
		{
			fsmEventTarget.target = FsmEventTarget.EventTarget.BroadcastAll;
			fsmEventTarget.excludeSelf = false;
		}
		else if (fsmEventSetup.target == PlayMakerProxyEventTarget.FsmComponent)
		{
			fsmEventTarget.target = FsmEventTarget.EventTarget.FSMComponent;
			fsmEventTarget.fsmComponent = fsmEventSetup.fsmComponent;
		}
		else if (fsmEventSetup.target == PlayMakerProxyEventTarget.GameObject)
		{
			fsmEventTarget.target = FsmEventTarget.EventTarget.GameObject;
			fsmEventTarget.gameObject = new FsmOwnerDefault();
			fsmEventTarget.gameObject.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
			fsmEventTarget.gameObject.GameObject.Value = fsmEventSetup.gameObject;
		}
		else if (fsmEventSetup.target == PlayMakerProxyEventTarget.Owner)
		{
			fsmEventTarget.ResetParameters();
			fsmEventTarget.target = FsmEventTarget.EventTarget.GameObject;
			fsmEventTarget.gameObject = new FsmOwnerDefault();
			fsmEventTarget.gameObject.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
			fsmEventTarget.gameObject.GameObject.Value = base.gameObject;
		}
	}

	private void SetupVariableTarget()
	{
		if (fsmVariableSetup.target == PlayMakerProxyVariableTarget.GlobalVariable)
		{
			if (fsmVariableSetup.variableType == VariableType.Bool)
			{
				fsmBoolTarget = FsmVariables.GlobalVariables.FindFsmBool(fsmVariableSetup.variableName);
			}
			else if (fsmVariableSetup.variableType == VariableType.Float)
			{
				fsmFloatTarget = FsmVariables.GlobalVariables.FindFsmFloat(fsmVariableSetup.variableName);
			}
			else if (fsmVariableSetup.variableType == VariableType.Vector2)
			{
				fsmVector2Target = FsmVariables.GlobalVariables.FindFsmVector2(fsmVariableSetup.variableName);
			}
			else if (fsmVariableSetup.variableType == VariableType.String)
			{
				fsmStringTarget = FsmVariables.GlobalVariables.FindFsmString(fsmVariableSetup.variableName);
			}
		}
		else if (fsmVariableSetup.target == PlayMakerProxyVariableTarget.FsmComponent)
		{
			if (fsmVariableSetup.fsmComponent != null)
			{
				if (fsmVariableSetup.variableType == VariableType.Bool)
				{
					fsmBoolTarget = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmBool(fsmVariableSetup.variableName);
				}
				else if (fsmVariableSetup.variableType == VariableType.Float)
				{
					fsmFloatTarget = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmFloat(fsmVariableSetup.variableName);
				}
				else if (fsmVariableSetup.variableType == VariableType.Vector2)
				{
					fsmVector2Target = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmVector2(fsmVariableSetup.variableName);
				}
				else if (fsmVariableSetup.variableType == VariableType.String)
				{
					fsmStringTarget = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmString(fsmVariableSetup.variableName);
				}
			}
			else
			{
				Debug.LogError("set to target a FsmComponent but fsmEventTarget.target is null");
			}
		}
		else if (fsmVariableSetup.target == PlayMakerProxyVariableTarget.Owner)
		{
			if (fsmVariableSetup.gameObject != null)
			{
				if (fsmVariableSetup.fsmComponent != null)
				{
					if (fsmVariableSetup.variableType == VariableType.Bool)
					{
						fsmBoolTarget = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmBool(fsmVariableSetup.variableName);
					}
					else if (fsmVariableSetup.variableType == VariableType.Float)
					{
						fsmFloatTarget = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmFloat(fsmVariableSetup.variableName);
					}
					else if (fsmVariableSetup.variableType == VariableType.Vector2)
					{
						fsmVector2Target = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmVector2(fsmVariableSetup.variableName);
					}
					else if (fsmVariableSetup.variableType == VariableType.String)
					{
						fsmStringTarget = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmString(fsmVariableSetup.variableName);
					}
				}
			}
			else
			{
				Debug.LogError("set to target Owbner but fsmEventTarget.target is null");
			}
		}
		else
		{
			if (fsmVariableSetup.target != PlayMakerProxyVariableTarget.GameObject)
			{
				return;
			}
			if (fsmVariableSetup.gameObject != null)
			{
				if (fsmVariableSetup.fsmComponent != null)
				{
					if (fsmVariableSetup.variableType == VariableType.Bool)
					{
						fsmBoolTarget = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmBool(fsmVariableSetup.variableName);
					}
					else if (fsmVariableSetup.variableType == VariableType.Float)
					{
						fsmFloatTarget = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmFloat(fsmVariableSetup.variableName);
					}
					else if (fsmVariableSetup.variableType == VariableType.Vector2)
					{
						fsmVector2Target = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmVector2(fsmVariableSetup.variableName);
					}
					else if (fsmVariableSetup.variableType == VariableType.String)
					{
						fsmStringTarget = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmString(fsmVariableSetup.variableName);
					}
				}
			}
			else
			{
				Debug.LogError("set to target a Gameobject but fsmEventTarget.target is null");
			}
		}
	}

	private void SetupUiListeners()
	{
		if (UiTarget.GetComponent<Button>() != null)
		{
			UiTarget.GetComponent<Button>().onClick.AddListener(OnClick);
		}
		if (UiTarget.GetComponent<Toggle>() != null)
		{
			UiTarget.GetComponent<Toggle>().onValueChanged.AddListener(OnValueChanged);
			if (action == ActionType.SetFsmVariable)
			{
				SetFsmVariable(UiTarget.GetComponent<Toggle>().isOn);
			}
		}
		if (UiTarget.GetComponent<Slider>() != null)
		{
			UiTarget.GetComponent<Slider>().onValueChanged.AddListener(OnValueChanged);
			if (action == ActionType.SetFsmVariable)
			{
				SetFsmVariable(UiTarget.GetComponent<Slider>().value);
			}
		}
		if (UiTarget.GetComponent<Scrollbar>() != null)
		{
			UiTarget.GetComponent<Scrollbar>().onValueChanged.AddListener(OnValueChanged);
			if (action == ActionType.SetFsmVariable)
			{
				SetFsmVariable(UiTarget.GetComponent<Scrollbar>().value);
			}
		}
		if (UiTarget.GetComponent<ScrollRect>() != null)
		{
			UiTarget.GetComponent<ScrollRect>().onValueChanged.AddListener(OnValueChanged);
			if (action == ActionType.SetFsmVariable)
			{
				SetFsmVariable(UiTarget.GetComponent<ScrollRect>().normalizedPosition);
			}
		}
		if (UiTarget.GetComponent<InputField>() != null)
		{
			UiTarget.GetComponent<InputField>().onEndEdit.AddListener(onEndEdit);
			if (action == ActionType.SetFsmVariable)
			{
				WatchInputField = true;
				inputField = UiTarget.GetComponent<InputField>();
				lastInputFieldValue = "";
			}
		}
	}

	protected void OnClick()
	{
		if (debug)
		{
			Debug.Log("OnClick");
		}
		FsmEventData eventData = new FsmEventData();
		FirePlayMakerEvent(eventData);
	}

	protected void OnValueChanged(bool value)
	{
		if (debug)
		{
			Debug.Log("OnValueChanged(bool): " + value);
		}
		if (action == ActionType.SendFsmEvent)
		{
			FsmEventData fsmEventData = new FsmEventData();
			fsmEventData.BoolData = value;
			FirePlayMakerEvent(fsmEventData);
		}
		else
		{
			SetFsmVariable(value);
		}
	}

	protected void OnValueChanged(float value)
	{
		if (debug)
		{
			Debug.Log("OnValueChanged(float): " + value);
		}
		if (action == ActionType.SendFsmEvent)
		{
			FsmEventData fsmEventData = new FsmEventData();
			fsmEventData.FloatData = value;
			FirePlayMakerEvent(fsmEventData);
		}
		else
		{
			SetFsmVariable(value);
		}
	}

	protected void OnValueChanged(Vector2 value)
	{
		if (debug)
		{
			Vector2 vector = value;
			Debug.Log("OnValueChanged(vector2): " + vector.ToString());
		}
		if (action == ActionType.SendFsmEvent)
		{
			FsmEventData fsmEventData = new FsmEventData();
			fsmEventData.Vector2Data = value;
			FirePlayMakerEvent(fsmEventData);
		}
		else
		{
			SetFsmVariable(value);
		}
	}

	protected void onEndEdit(string value)
	{
		if (debug)
		{
			Debug.Log("onEndEdit(string): " + value);
		}
		if (action == ActionType.SendFsmEvent)
		{
			FsmEventData fsmEventData = new FsmEventData();
			fsmEventData.StringData = value;
			FirePlayMakerEvent(fsmEventData);
		}
		else
		{
			SetFsmVariable(value);
		}
	}

	private void SetFsmVariable(Vector2 value)
	{
		if (fsmVector2Target != null)
		{
			if (debug)
			{
				string text = base.name;
				Vector2 vector = value;
				Debug.Log("PlayMakerUGuiComponentProxy on " + text + ": Fsm Vector2 set to " + vector.ToString());
			}
			fsmVector2Target.Value = value;
		}
		else
		{
			Debug.LogError("PlayMakerUGuiComponentProxy on " + base.name + ": Fsm Vector2 MISSING !!", base.gameObject);
		}
	}

	private void SetFsmVariable(bool value)
	{
		if (fsmBoolTarget != null)
		{
			if (debug)
			{
				Debug.Log("PlayMakerUGuiComponentProxy on " + base.name + ": Fsm Bool set to " + value);
			}
			fsmBoolTarget.Value = value;
		}
		else
		{
			Debug.LogError("PlayMakerUGuiComponentProxy on " + base.name + ": Fsm Bool MISSING !!", base.gameObject);
		}
	}

	private void SetFsmVariable(float value)
	{
		if (fsmFloatTarget != null)
		{
			if (debug)
			{
				Debug.Log("PlayMakerUGuiComponentProxy on " + base.name + ": Fsm Float set to " + value);
			}
			fsmFloatTarget.Value = value;
		}
		else
		{
			Debug.LogError("PlayMakerUGuiComponentProxy on " + base.name + ": Fsm Float MISSING !!", base.gameObject);
		}
	}

	private void SetFsmVariable(string value)
	{
		if (fsmStringTarget != null)
		{
			if (debug)
			{
				Debug.Log("PlayMakerUGuiComponentProxy on " + base.name + ": Fsm String set to " + value);
			}
			fsmStringTarget.Value = value;
		}
		else
		{
			Debug.LogError("PlayMakerUGuiComponentProxy on " + base.name + ": Fsm String MISSING !!", base.gameObject);
		}
	}

	private void FirePlayMakerEvent(FsmEventData eventData)
	{
		if (eventData != null)
		{
			Fsm.EventData = eventData;
		}
		fsmEventTarget.excludeSelf = false;
		if (PlayMakerUGuiSceneProxy.fsm == null)
		{
			Debug.LogError("Missing 'PlayMaker UGui' prefab in scene");
			return;
		}
		Fsm fsm = PlayMakerUGuiSceneProxy.fsm.Fsm;
		if (debug)
		{
			Debug.Log("Fire event: " + GetEventString());
		}
		fsm.Event(fsmEventTarget, GetEventString());
	}

	public bool DoesTargetImplementsEvent()
	{
		string eventString = GetEventString();
		if (fsmEventSetup.target == PlayMakerProxyEventTarget.BroadCastAll)
		{
			return FsmEvent.IsEventGlobal(eventString);
		}
		if (fsmEventSetup.target == PlayMakerProxyEventTarget.FsmComponent)
		{
			return PlayMakerUtils.DoesFsmImplementsEvent(fsmEventSetup.fsmComponent, eventString);
		}
		if (fsmEventSetup.target == PlayMakerProxyEventTarget.GameObject)
		{
			return PlayMakerUtils.DoesGameObjectImplementsEvent(fsmEventSetup.gameObject, eventString);
		}
		if (fsmEventSetup.target == PlayMakerProxyEventTarget.Owner)
		{
			return PlayMakerUtils.DoesGameObjectImplementsEvent(base.gameObject, eventString);
		}
		return false;
	}

	private string GetEventString()
	{
		if (!string.IsNullOrEmpty(fsmEventSetup.customEventName))
		{
			return fsmEventSetup.customEventName;
		}
		return fsmEventSetup.builtInEventName;
	}
}
