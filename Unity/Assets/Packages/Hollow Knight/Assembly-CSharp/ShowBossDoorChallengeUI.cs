using HutongGames.PlayMaker;
using UnityEngine;

[ActionCategory("Hollow Knight")]
public class ShowBossDoorChallengeUI : FsmStateAction
{
	private static BossDoorChallengeUI spawnedUI;

	public FsmOwnerDefault targetDoor;

	public FsmGameObject prefab;

	public FsmEvent cancelEvent;

	public FsmEvent challengeEvent;

	public override void Reset()
	{
		targetDoor = null;
		prefab = null;
		challengeEvent = null;
		cancelEvent = null;
	}

	public override void OnEnter()
	{
		if (spawnedUI == null && (bool)prefab.Value)
		{
			GameObject gameObject = Object.Instantiate(prefab.Value);
			spawnedUI = gameObject.GetComponent<BossDoorChallengeUI>();
			gameObject.SetActive(value: false);
		}
		if ((bool)spawnedUI)
		{
			GameObject safe = targetDoor.GetSafe(this);
			BossSequenceDoor door = (safe ? safe.GetComponent<BossSequenceDoor>() : null);
			spawnedUI.Setup(door);
			spawnedUI.Show();
			BossDoorChallengeUI.HideEvent temp2 = null;
			temp2 = delegate
			{
				Fsm.Event(cancelEvent);
				spawnedUI.OnHidden -= temp2;
			};
			spawnedUI.OnHidden += temp2;
			BossDoorChallengeUI.BeginEvent temp = null;
			temp = delegate
			{
				Fsm.Event(challengeEvent);
				spawnedUI.OnBegin -= temp;
			};
			spawnedUI.OnBegin += temp;
		}
	}
}
