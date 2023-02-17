using HutongGames.PlayMaker;

[ActionCategory("Hollow Knight")]
public class CameraFreezeInPlace : FsmStateAction
{
	public FsmBool freezeTargetAlso;

	public override void Reset()
	{
		freezeTargetAlso = true;
	}

	public override void OnEnter()
	{
		if ((bool)GameManager.instance.cameraCtrl)
		{
			GameManager.instance.cameraCtrl.FreezeInPlace(freezeTargetAlso.Value);
		}
		Finish();
	}
}
