using System.Collections.Generic;
using HutongGames.PlayMaker;

[ActionCategory("Hollow Knight")]
public class ShowGodfinderIcon : FsmStateAction
{
	public FsmFloat delay;

	[ObjectType(typeof(BossScene))]
	public FsmObject unlockBossScene;

	public override void Reset()
	{
		delay = null;
	}

	public override void OnEnter()
	{
		GodfinderIcon.ShowIcon(delay.Value, unlockBossScene.Value as BossScene);
		if ((bool)unlockBossScene.Value && !GameManager.instance.playerData.GetVariable<List<string>>("unlockedBossScenes").Contains(unlockBossScene.Value.name))
		{
			GameManager.instance.playerData.GetVariable<List<string>>("unlockedBossScenes").Add(unlockBossScene.Value.name);
		}
		Finish();
	}
}
