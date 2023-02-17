using HutongGames.PlayMaker;
using UnityEngine;

public class GetStaticVariable : FsmStateAction
{
	public FsmString variableName;

	[UIHint(UIHint.Variable)]
	public FsmVar storeValue;

	public override void Reset()
	{
		variableName = null;
		storeValue = null;
	}

	public override void OnEnter()
	{
		if (!variableName.IsNone && !storeValue.IsNone && StaticVariableList.Exists(variableName.Value))
		{
			switch (storeValue.Type)
			{
			case VariableType.Bool:
				storeValue.SetValue(StaticVariableList.GetValue<bool>(variableName.Value));
				break;
			case VariableType.Int:
				storeValue.SetValue(StaticVariableList.GetValue<int>(variableName.Value));
				break;
			case VariableType.Float:
				storeValue.SetValue(StaticVariableList.GetValue<float>(variableName.Value));
				break;
			case VariableType.String:
				storeValue.SetValue(StaticVariableList.GetValue<string>(variableName.Value));
				break;
			default:
				Debug.LogWarning($"Couldn't get static variable: {variableName.Value}, Variable type \"{storeValue.Type.ToString()}\" not implemented!");
				break;
			}
		}
		else
		{
			Debug.LogWarning($"Couldn't get static variable: {variableName.Value}");
		}
		Finish();
	}
}
