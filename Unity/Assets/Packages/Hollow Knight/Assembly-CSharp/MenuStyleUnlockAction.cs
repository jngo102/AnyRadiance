using HutongGames.PlayMaker;

[ActionCategory("Hollow Knight")]
public class MenuStyleUnlockAction : FsmStateAction
{
	public FsmString unlockKey;

	public override void Reset()
	{
		unlockKey = null;
	}

	public override void OnEnter()
	{
		if (!string.IsNullOrEmpty(unlockKey.Value))
		{
			MenuStyleUnlock.Unlock(unlockKey.Value);
		}
		Finish();
	}
}
