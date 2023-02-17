using HutongGames.PlayMaker;
using UnityEngine;

[ActionCategory("Hollow Knight")]
public class ShowBossChallengeUI : FsmStateAction
{
	private static GameObject spawnedUI;

	public FsmGameObject prefab;

	public FsmString bossNameSheet;

	public FsmString bossNameKey;

	public FsmString descriptionSheet;

	public FsmString descriptionKey;

	public FsmEvent levelSelectedEvent;

	public override void Reset()
	{
		prefab = null;
		bossNameSheet = null;
		bossNameKey = null;
		descriptionSheet = null;
		descriptionKey = null;
		levelSelectedEvent = null;
	}

	public override void OnEnter()
	{
		if (spawnedUI == null && (bool)prefab.Value)
		{
			spawnedUI = Object.Instantiate(prefab.Value);
			spawnedUI.SetActive(value: false);
		}
		if ((bool)spawnedUI)
		{
			GameObject gameObject = spawnedUI;
			gameObject.transform.position = prefab.Value.transform.position;
			gameObject.SetActive(value: true);
			BossChallengeUI ui = gameObject.GetComponent<BossChallengeUI>();
			if ((bool)ui)
			{
				BossStatue componentInParent = base.Owner.GetComponentInParent<BossStatue>();
				BossChallengeUI.HideEvent temp2 = null;
				temp2 = delegate
				{
					Finish();
					ui.OnCancel -= temp2;
				};
				ui.OnCancel += temp2;
				BossChallengeUI.LevelSelectedEvent temp = null;
				temp = delegate
				{
					Fsm.Event(levelSelectedEvent);
					ui.OnLevelSelected -= temp;
				};
				ui.OnLevelSelected += temp;
				ui.Setup(componentInParent, bossNameSheet.Value, bossNameKey.Value, descriptionSheet.Value, descriptionKey.Value);
				return;
			}
		}
		Finish();
	}
}
