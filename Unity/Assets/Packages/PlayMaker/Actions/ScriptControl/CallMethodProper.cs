// Warning: Some assembly references could not be resolved automatically. This might lead to incorrect decompilation of some parts,
// for ex. property getter/setter access. To get optimal decompilation results, please manually add the missing references to the list of loaded assemblies.
// HutongGames.PlayMaker.Actions.CallMethodProper
using System;
using System.Reflection;
using HutongGames.PlayMaker;
using UnityEngine;

[ActionCategory(ActionCategory.ScriptControl)]
public class CallMethodProper : FsmStateAction
{
	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("The game object that owns the Behaviour.")]
	public FsmOwnerDefault gameObject;

	[RequiredField]
	[UIHint(UIHint.Behaviour)]
	[HutongGames.PlayMaker.Tooltip("The Behaviour that contains the method to start as a coroutine.")]
	public FsmString behaviour;

	[UIHint(UIHint.Method)]
	[HutongGames.PlayMaker.Tooltip("Name of the method to call on the component")]
	public FsmString methodName;

	[HutongGames.PlayMaker.Tooltip("Method paramters. NOTE: these must match the method's signature!")]
	public FsmVar[] parameters;

	[ActionSection("Store Result")]
	[UIHint(UIHint.Variable)]
	[HutongGames.PlayMaker.Tooltip("Store the result of the method call.")]
	public FsmVar storeResult;

	private UnityEngine.Object cachedBehaviour;

	private Type cachedType;

	private MethodInfo cachedMethodInfo;

	private ParameterInfo[] cachedParameterInfo;

	private object[] parametersArray;

	private string errorString;

	private MonoBehaviour component;

	public override void OnEnter()
	{
		parametersArray = new object[parameters.Length];
		DoMethodCall();
		Finish();
	}

	private void DoMethodCall()
	{
		if (behaviour.Value == null)
		{
			Finish();
			return;
		}
		GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
		if (ownerDefaultTarget == null)
		{
			return;
		}
		component = ownerDefaultTarget.GetComponent(behaviour.Value) as MonoBehaviour;
		if (component == null)
		{
			LogWarning("CallMethodProper: " + ownerDefaultTarget.name + " missing behaviour: " + behaviour.Value);
			return;
		}
		if (cachedMethodInfo == null)
		{
			errorString = string.Empty;
			if (!DoCache())
			{
				Debug.LogError(errorString);
				Finish();
				return;
			}
		}
		object value = null;
		if (cachedParameterInfo.Length == 0)
		{
			value = cachedMethodInfo.Invoke(cachedBehaviour, null);
		}
		else
		{
			for (int i = 0; i < parameters.Length; i++)
			{
				FsmVar fsmVar = parameters[i];
				fsmVar.UpdateValue();
				parametersArray[i] = fsmVar.GetValue();
			}
			try
			{
				value = cachedMethodInfo.Invoke(cachedBehaviour, parametersArray);
			}
			catch (Exception ex)
			{
				Debug.LogError("CallMethodProper error on " + base.Fsm.OwnerName + " -> " + ex);
			}
		}
		if (storeResult.Type != VariableType.Unknown)
		{
			storeResult.SetValue(value);
		}
	}

	private bool DoCache()
	{
		cachedBehaviour = component;
		cachedType = component.GetType();
		cachedMethodInfo = cachedType.GetMethod(methodName.Value);
		if (cachedMethodInfo == null)
		{
			errorString = errorString + "Method Name is invalid: " + methodName.Value + "\n";
			Finish();
			return false;
		}
		cachedParameterInfo = cachedMethodInfo.GetParameters();
		return true;
	}
}
