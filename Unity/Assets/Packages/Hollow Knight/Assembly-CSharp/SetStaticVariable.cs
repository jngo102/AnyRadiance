using HutongGames.PlayMaker;
using UnityEngine;

public class SetStaticVariable : FsmStateAction
{
	public FsmString variableName;

	public FsmVar setValue;

	public override void Reset()
	{
		variableName = null;
		setValue = null;
	}

	public override void OnEnter()
	{
		if (!variableName.IsNone && !setValue.IsNone)
		{
			_ = setValue.RealType;
			object valueFromFsmVar = PlayMakerUtils.GetValueFromFsmVar(base.Fsm, setValue);
			switch (setValue.Type)
			{
			case VariableType.Bool:
				StaticVariableList.SetValue(variableName.Value, (bool)valueFromFsmVar);
				break;
			case VariableType.Int:
				StaticVariableList.SetValue(variableName.Value, (int)valueFromFsmVar);
				break;
			case VariableType.Float:
				StaticVariableList.SetValue(variableName.Value, (float)valueFromFsmVar);
				break;
			case VariableType.String:
				StaticVariableList.SetValue(variableName.Value, (string)valueFromFsmVar);
				break;
			default:
				Debug.LogWarning($"Couldn't set static variable: {variableName.Value}, Variable type \"{setValue.Type.ToString()}\" not implemented!");
				break;
			}
		}
		else
		{
			Debug.LogWarning($"Couldn't set static variable: {variableName.Value}");
		}
		Finish();
	}
}
