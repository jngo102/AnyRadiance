using HutongGames.PlayMaker;
using UnityEngine;

public class GetPersistentBoolFromSaveData : FsmStateAction
{
	[CheckForComponent(typeof(PersistentBoolItem))]
	public FsmOwnerDefault Target;

	[HideIf("ShouldHideDirect")]
	public FsmString SceneName;

	[HideIf("ShouldHideDirect")]
	public FsmString ID;

	[UIHint(UIHint.Variable)]
	public FsmBool StoreValue;

	public bool ShouldHideDirect()
	{
		if (Target.OwnerOption != 0)
		{
			if (Target.GameObject != null)
			{
				return Target.GameObject.Value != null;
			}
			return false;
		}
		return true;
	}

	public override void Reset()
	{
		Target = new FsmOwnerDefault
		{
			OwnerOption = OwnerDefaultOption.SpecifyGameObject,
			GameObject = null
		};
		SceneName = null;
		ID = null;
		StoreValue = null;
	}

	public override void OnEnter()
	{
		GameObject safe = Target.GetSafe(this);
		string sceneName;
		string id;
		if (safe != null)
		{
			PersistentBoolData persistentBoolData = safe.GetComponent<PersistentBoolItem>().persistentBoolData;
			sceneName = persistentBoolData.sceneName;
			id = persistentBoolData.id;
		}
		else
		{
			sceneName = SceneName.Value;
			id = ID.Value;
		}
		PersistentBoolData persistentBoolData2 = SceneData.instance.FindMyState(new PersistentBoolData
		{
			id = id,
			sceneName = sceneName
		});
		StoreValue.Value = persistentBoolData2?.activated ?? false;
		Finish();
	}
}
