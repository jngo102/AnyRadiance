using HutongGames.PlayMaker;
using UnityEngine;

public class EnemyDreamnailReaction : MonoBehaviour
{
	private enum States
	{
		Suppressed,
		Ready,
		CoolingDown
	}

	private const int RegularMPGain = 33;

	private const int BoostedMPGain = 66;

	private const float AttackMagnitude = 2f;

	private const float CooldownDuration = 0.2f;

	[SerializeField]
	private int convoAmount;

	[SerializeField]
	private string convoTitle;

	[SerializeField]
	private bool startSuppressed;

	[SerializeField]
	private bool noSoul;

	[SerializeField]
	private GameObject dreamImpactPrefab;

	public bool allowUseChildColliders;

	private States state;

	private float cooldownTimeRemaining;

	protected void Reset()
	{
		convoAmount = 8;
		convoTitle = "GENERIC";
		startSuppressed = false;
	}

	protected void Start()
	{
		state = ((!startSuppressed) ? States.Ready : States.Suppressed);
	}

	public void RecieveDreamImpact()
	{
		if (state == States.Ready)
		{
			if (!noSoul)
			{
				int amount = (GameManager.instance.playerData.GetBool("equippedCharm_30") ? 66 : 33);
				HeroController.instance.AddMPCharge(amount);
			}
			ShowConvo();
			if (dreamImpactPrefab != null)
			{
				dreamImpactPrefab.Spawn().transform.position = base.transform.position;
			}
			Recoil component = GetComponent<Recoil>();
			if (component != null)
			{
				bool flag = HeroController.instance.transform.localScale.x <= 0f;
				component.RecoilByDirection((!flag) ? 2 : 0, 2f);
			}
			SpriteFlash component2 = base.gameObject.GetComponent<SpriteFlash>();
			if (component2 != null)
			{
				component2.flashDreamImpact();
			}
			state = States.CoolingDown;
			cooldownTimeRemaining = 0.2f;
		}
	}

	public void MakeReady()
	{
		if (state == States.Suppressed)
		{
			state = States.Ready;
		}
	}

	public void SetConvoTitle(string title)
	{
		convoTitle = title;
	}

	private void ShowConvo()
	{
		PlayMakerFSM playMakerFSM = PlayMakerFSM.FindFsmOnGameObject(FsmVariables.GlobalVariables.GetFsmGameObject("Enemy Dream Msg").Value, "Display");
		playMakerFSM.FsmVariables.GetFsmInt("Convo Amount").Value = convoAmount;
		playMakerFSM.FsmVariables.GetFsmString("Convo Title").Value = convoTitle;
		playMakerFSM.SendEvent("DISPLAY ENEMY DREAM");
	}

	protected void Update()
	{
		if (state == States.CoolingDown)
		{
			cooldownTimeRemaining -= Time.deltaTime;
			if (cooldownTimeRemaining <= 0f)
			{
				state = States.Ready;
			}
		}
	}
}
