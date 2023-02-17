using HutongGames.PlayMaker;
using UnityEngine;

[ActionCategory("Hollow Knight")]
public class GetConstantsValue : FsmStateAction
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
		if (!variableName.IsNone && !storeValue.IsNone)
		{
			switch (storeValue.Type)
			{
			case VariableType.Bool:
				storeValue.SetValue(Constants.GetConstantValue<bool>(variableName.Value));
				break;
			case VariableType.Int:
				storeValue.SetValue(Constants.GetConstantValue<int>(variableName.Value));
				break;
			case VariableType.Float:
				storeValue.SetValue(Constants.GetConstantValue<float>(variableName.Value));
				break;
			case VariableType.String:
				storeValue.SetValue(Constants.GetConstantValue<string>(variableName.Value));
				break;
			default:
				Debug.LogWarning($"Couldn't get constant value: {variableName.Value}, Variable type \"{storeValue.Type.ToString()}\" not implemented!");
				break;
			}
		}
		else
		{
			Debug.LogWarning($"Couldn't get constant value: {variableName.Value}, please ensure variableName and storeValue are not empty.");
		}
		Finish();
	}
}
