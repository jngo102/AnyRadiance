using System.Collections;
using GlobalEnums;
using InControl;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
	private static CheatManager instance;

	private bool wasEverOpened;

	private bool isOpen;

	private int selectedButtonIndex;

	private int lastSelectDelta;

	private float selectCooldown;

	private const float KeyRepeatInterval = 0.2f;

	private bool isQuickHealEnabled;

	private bool isRegenerating;

	private bool isInstaDeathEnabled;

	private bool isInstaKillEnabled;

	private int safetyCounter;

	private const int SafetyAmount = 10;

	private string transitionRobotStartScene;

	private float slowOpenLeftStickTimer;

	private float slowOpenRightStickTimer;

	public static bool IsCheatsEnabled
	{
		get
		{
			if (Application.platform == RuntimePlatform.Switch || Application.platform == RuntimePlatform.PS4 || Application.platform == RuntimePlatform.XboxOne || Application.platform == RuntimePlatform.WindowsEditor)
			{
				if (!Debug.isDebugBuild)
				{
					return CommandLineArguments.EnableDeveloperCheats;
				}
				return true;
			}
			return false;
		}
	}

	public static bool IsInstaKillEnabled
	{
		get
		{
			if ((bool)instance)
			{
				return instance.isInstaKillEnabled;
			}
			return false;
		}
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void Init()
	{
		if (IsCheatsEnabled)
		{
			Object.DontDestroyOnLoad(new GameObject("CheatManager", typeof(CheatManager)));
			PerformanceHUD.Init();
		}
	}

	protected IEnumerator Start()
	{
		instance = this;
		while (true)
		{
			yield return new WaitForSeconds(4f);
			if (!isRegenerating)
			{
				continue;
			}
			GameManager unsafeInstance = GameManager.UnsafeInstance;
			if (unsafeInstance != null)
			{
				HeroController hero_ctrl = unsafeInstance.hero_ctrl;
				if (hero_ctrl != null)
				{
					hero_ctrl.AddHealth(Mathf.Clamp(unsafeInstance.playerData.GetInt("maxHealth") - unsafeInstance.playerData.GetInt("health"), 0, 1));
					hero_ctrl.AddMPCharge(Mathf.Clamp(unsafeInstance.playerData.GetInt("maxMP") - unsafeInstance.playerData.GetInt("MPCharge"), 0, 20));
				}
			}
		}
	}

	private void OnDestroy()
	{
		if (instance == this)
		{
			instance = null;
		}
	}

	protected void Update()
	{
		if (InputManager.ActiveDevice.LeftStickButton.IsPressed || Input.GetKey(KeyCode.Home))
		{
			slowOpenLeftStickTimer += Time.unscaledDeltaTime;
		}
		else
		{
			slowOpenLeftStickTimer = 0f;
		}
		if (InputManager.ActiveDevice.RightStickButton.IsPressed || Input.GetKey(KeyCode.End))
		{
			slowOpenRightStickTimer += Time.unscaledDeltaTime;
		}
		else
		{
			slowOpenRightStickTimer = 0f;
		}
		bool flag = slowOpenLeftStickTimer > 5f && slowOpenRightStickTimer > 5f;
		bool flag2 = InputManager.ActiveDevice.LeftStickButton.WasPressed || Input.GetKeyDown(KeyCode.Home);
		if ((wasEverOpened && flag2) || (!wasEverOpened && flag))
		{
			ToggleCheatMenu();
			if (PerformanceHUD.Shared != null)
			{
				PerformanceHUD.Shared.enabled = true;
			}
		}
		if (isQuickHealEnabled && (InputManager.ActiveDevice.RightStickButton.WasPressed || Input.GetKeyDown(KeyCode.End)))
		{
			Restore();
		}
		if (isInstaDeathEnabled && InputManager.ActiveDevice.RightStickButton.WasPressed)
		{
			Kill();
		}
		selectCooldown -= Time.unscaledDeltaTime;
		if (string.IsNullOrEmpty(transitionRobotStartScene))
		{
			return;
		}
		HeroController heroController = HeroController.instance;
		GameManager unsafeInstance = GameManager.UnsafeInstance;
		if (!(heroController != null) || !(unsafeInstance != null) || heroController.transitionState != 0 || !(Time.timeScale > Mathf.Epsilon))
		{
			return;
		}
		if (unsafeInstance.sceneName == transitionRobotStartScene)
		{
			if (!heroController.cState.recoilingLeft)
			{
				heroController.RecoilLeft();
			}
		}
		else if (!heroController.cState.recoilingRight)
		{
			heroController.RecoilRight();
		}
	}

	private void ToggleCheatMenu()
	{
		isOpen = !isOpen;
		safetyCounter = 0;
		if (isOpen)
		{
			wasEverOpened = true;
		}
	}

	protected void OnGUI()
	{
		if (!isOpen)
		{
			return;
		}
		int buttonIndex = 0;
		if (CheatButton(ref buttonIndex, "Resume"))
		{
			isOpen = false;
		}
		if (CheatButton(ref buttonIndex, "Restore HP/MP"))
		{
			Restore();
			isOpen = false;
		}
		if (CheatButton(ref buttonIndex, (isRegenerating ? "Disable" : "Enable") + " Regen"))
		{
			isRegenerating = !isRegenerating;
			isOpen = false;
		}
		if (CheatButton(ref buttonIndex, "Get Geo"))
		{
			GetGeo(100);
		}
		if (PerformanceHUD.Shared != null && CheatButton(ref buttonIndex, "Hide Performance HUD"))
		{
			PerformanceHUD.Shared.enabled = false;
			isOpen = false;
		}
		if (SafetyCheatButton(ref buttonIndex, "All Stags"))
		{
			OpenStagStations();
		}
		if (CheatButton(ref buttonIndex, (IsInstaKillEnabled ? "Disable" : "Enable") + " Insta Kill"))
		{
			isInstaKillEnabled = !isInstaKillEnabled;
		}
		PlayerData playerData = PlayerData.instance;
		if (playerData != null)
		{
			if (CheatButton(ref buttonIndex, (playerData.GetBool("isInvincible") ? "Disable" : "Enable") + " Invincibility"))
			{
				playerData.SetBool("isInvincible", !playerData.GetBool("isInvincible"));
			}
			if (CheatButton(ref buttonIndex, (playerData.GetBool("invinciTest") ? "Disable" : "Enable") + " Test Invincibility"))
			{
				playerData.SetBool("invinciTest", !playerData.GetBool("invinciTest"));
			}
			if (SafetyCheatButton(ref buttonIndex, "Fireball"))
			{
				if (!playerData.GetBool("hasSpell"))
				{
					playerData.SetBool("hasSpell", true);
				}
				playerData.SetInt("fireballLevel", Mathf.Min(playerData.GetInt("fireballLevel") + 1, 2));
			}
			if (SafetyCheatButton(ref buttonIndex, "Quake"))
			{
				if (!playerData.GetBool("hasSpell"))
				{
					playerData.SetBool("hasSpell", true);
				}
				playerData.SetInt("quakeLevel", Mathf.Min(playerData.GetInt("quakeLevel") + 1, 2));
			}
			if (SafetyCheatButton(ref buttonIndex, "Scream"))
			{
				if (!playerData.GetBool("hasSpell"))
				{
					playerData.SetBool("hasSpell", true);
				}
				playerData.SetInt("screamLevel", Mathf.Min(playerData.GetInt("screamLevel") + 1, 2));
			}
			if (SafetyCheatButton(ref buttonIndex, "Double Jump"))
			{
				playerData.SetBool("hasDoubleJump", true);
			}
			if (SafetyCheatButton(ref buttonIndex, "All Charms"))
			{
				playerData.SetBool("hasCharm", true);
				playerData.SetBool("gotCharm_1", true);
				playerData.SetBool("gotCharm_2", true);
				playerData.SetBool("gotCharm_3", true);
				playerData.SetBool("gotCharm_4", true);
				playerData.SetBool("gotCharm_5", true);
				playerData.SetBool("gotCharm_6", true);
				playerData.SetBool("gotCharm_7", true);
				playerData.SetBool("gotCharm_8", true);
				playerData.SetBool("gotCharm_9", true);
				playerData.SetBool("gotCharm_10", true);
				playerData.SetBool("gotCharm_11", true);
				playerData.SetBool("gotCharm_12", true);
				playerData.SetBool("gotCharm_13", true);
				playerData.SetBool("gotCharm_14", true);
				playerData.SetBool("gotCharm_15", true);
				playerData.SetBool("gotCharm_16", true);
				playerData.SetBool("gotCharm_17", true);
				playerData.SetBool("gotCharm_18", true);
				playerData.SetBool("gotCharm_19", true);
				playerData.SetBool("gotCharm_20", true);
				playerData.SetBool("gotCharm_21", true);
				playerData.SetBool("gotCharm_22", true);
				playerData.SetBool("gotCharm_23", true);
				playerData.SetBool("gotCharm_24", true);
				playerData.SetBool("gotCharm_25", true);
				playerData.SetBool("gotCharm_26", true);
				playerData.SetBool("gotCharm_27", true);
				playerData.SetBool("gotCharm_28", true);
				playerData.SetBool("gotCharm_29", true);
				playerData.SetBool("gotCharm_30", true);
				playerData.SetBool("gotCharm_31", true);
				playerData.SetBool("gotCharm_32", true);
				playerData.SetBool("gotCharm_33", true);
				playerData.SetBool("gotCharm_34", true);
				playerData.SetBool("gotCharm_35", true);
				playerData.SetBool("gotCharm_36", true);
				playerData.SetBool("gotCharm_37", true);
				playerData.SetBool("gotCharm_38", true);
				playerData.SetBool("gotCharm_39", true);
				playerData.SetBool("gotCharm_40", true);
				playerData.SetInt("royalCharmState", 3);
				playerData.SetInt("charmSlots", 3);
			}
			if (SafetyCheatButton(ref buttonIndex, "Dream Orbs"))
			{
				playerData.SetInt("dreamOrbs", playerData.GetInt("dreamOrbs") + 1000);
			}
			if (SafetyCheatButton(ref buttonIndex, "Dreamnail"))
			{
				if (!playerData.GetBool("hasDreamNail"))
				{
					playerData.SetBool("hasDreamNail", true);
				}
				else
				{
					playerData.SetBool("dreamNailUpgraded", true);
				}
			}
			if (SafetyCheatButton(ref buttonIndex, "All Map"))
			{
				playerData.SetBool("hasMap", true);
				playerData.SetBool("mapDirtmouth", true);
				playerData.SetBool("mapCrossroads", true);
				playerData.SetBool("mapGreenpath", true);
				playerData.SetBool("mapFogCanyon", false);
				playerData.SetBool("mapRoyalGardens", true);
				playerData.SetBool("mapFungalWastes", false);
				playerData.SetBool("mapCity", true);
				playerData.SetBool("mapWaterways", false);
				playerData.SetBool("mapMines", true);
				playerData.SetBool("mapDeepnest", true);
				playerData.SetBool("mapCliffs", true);
				playerData.SetBool("mapOutskirts", true);
				playerData.SetBool("mapRestingGrounds", true);
				playerData.SetBool("mapAbyss", true);
				playerData.SetBool("openedMapperShop", true);
			}
			if (SafetyCheatButton(ref buttonIndex, "All Key Items"))
			{
				if (playerData.GetBool("hasDash"))
				{
					playerData.SetBool("hasShadowDash", true);
					playerData.SetBool("canShadowDash", true);
				}
				playerData.SetBool("hasDash", true);
				playerData.SetBool("canDash", true);
				playerData.SetBool("hasWalljump", true);
				playerData.SetBool("canWallJump", true);
				playerData.SetBool("hasSuperDash", true);
				playerData.SetBool("hasDreamNail", true);
				playerData.SetBool("dreamNailUpgraded", true);
				playerData.SetBool("hasLantern", true);
				playerData.SetBool("hasAcidArmour", true);
				playerData.SetBool("hasTramPass", true);
				playerData.SetBool("hasLoveKey", true);
				playerData.SetBool("hasWhiteKey", true);
				playerData.SetBool("hasSlykey", true);
				playerData.SetBool("hasKingsBrand", true);
				playerData.SetInt("simpleKeys", 5);
			}
			if (SafetyCheatButton(ref buttonIndex, "All Nail Arts"))
			{
				playerData.SetBool("hasNailArt", true);
				playerData.SetBool("hasDashSlash", true);
				playerData.SetBool("hasCyclone", true);
				playerData.SetBool("hasUpwardSlash", true);
				playerData.SetBool("hasAllNailArts", true);
			}
			if (SafetyCheatButton(ref buttonIndex, "Get Pale Ores"))
			{
				playerData.SetInt("ore", playerData.GetInt("ore") + 1);
			}
			if (SafetyCheatButton(ref buttonIndex, "Unlock Colosseums"))
			{
				playerData.SetBool("colosseumBronzeOpened", true);
				playerData.SetBool("colosseumSilverOpened", true);
				playerData.SetBool("colosseumGoldOpened", true);
			}
			if (SafetyCheatButton(ref buttonIndex, "Unlock Steel Soul"))
			{
				Platform.Current.EncryptedSharedData.SetInt("PermaDeath_Unlock", 1);
				Platform.Current.EncryptedSharedData.Save();
			}
			if (SafetyCheatButton(ref buttonIndex, (playerData.GetInt("permadeathMode") == 0) ? "Enable Steel Soul" : "Disable Steel Soul"))
			{
				playerData.SetInt("permadeathMode", (playerData.GetInt("permadeathMode") == 0) ? 1 : 0);
			}
		}
		if (CheatButton(ref buttonIndex, (PerformanceHUD.ShowVibrations ? "Hide" : "Show") + " Vibrations"))
		{
			PerformanceHUD.ShowVibrations = !PerformanceHUD.ShowVibrations;
		}
		if (CheatButton(ref buttonIndex, "Revert Global Pool"))
		{
			ObjectPool.instance.RevertToStartState();
		}
		if (CheatButton(ref buttonIndex, (isQuickHealEnabled ? "Disable" : "Enable") + " Quick Heal"))
		{
			isQuickHealEnabled = !isQuickHealEnabled;
		}
		if (CheatButton(ref buttonIndex, (isInstaDeathEnabled ? "Disable" : "Enable") + " Death Button"))
		{
			isInstaDeathEnabled = !isInstaDeathEnabled;
		}
		int num = 0;
		if ((bool)InputManager.ActiveDevice.DPadUp || Input.GetKeyDown(KeyCode.UpArrow))
		{
			num--;
		}
		if ((bool)InputManager.ActiveDevice.DPadDown || Input.GetKeyDown(KeyCode.DownArrow))
		{
			num++;
		}
		if (num != 0 && (num != lastSelectDelta || selectCooldown < Mathf.Epsilon))
		{
			selectedButtonIndex = Mathf.Clamp((selectedButtonIndex + num + buttonIndex) % buttonIndex, 0, buttonIndex - 1);
			selectCooldown = 0.2f;
			lastSelectDelta = num;
			safetyCounter = 0;
		}
		if (Event.current.type == EventType.Repaint && (InputManager.ActiveDevice.Action2.WasPressed || InputManager.ActiveDevice.Action3.WasPressed || InputManager.ActiveDevice.Action4.WasPressed || (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Z)))
		{
			isOpen = false;
			safetyCounter = 0;
		}
	}

	private static void OpenStagStations()
	{
		GameManager unsafeInstance = GameManager.UnsafeInstance;
		if (unsafeInstance != null)
		{
			PlayerData playerData = unsafeInstance.playerData;
			playerData.SetBool("openedTown", true);
			playerData.SetBool("openedTownBuilding", true);
			playerData.SetBool("openedCrossroads", true);
			playerData.SetBool("openedGreenpath", true);
			playerData.SetBool("openedRuins1", true);
			playerData.SetBool("openedRuins2", true);
			playerData.SetBool("openedFungalWastes", true);
			playerData.SetBool("openedRoyalGardens", true);
			playerData.SetBool("openedRestingGrounds", true);
			playerData.SetBool("openedDeepnest", true);
			playerData.SetBool("openedStagNest", true);
			playerData.SetBool("openedHiddenStation", true);
			playerData.SetBool("gladeDoorOpened", true);
			playerData.SetBool("troupeInTown", true);
		}
	}

	protected bool CheatButton(ref int buttonIndex, string content)
	{
		bool flag = selectedButtonIndex == buttonIndex;
		Rect position = new Rect(25f, 25 + 22 * buttonIndex, 200f, 20f);
		GUI.color = new Color(0f, 0f, 0f, 0.5f);
		GUI.DrawTexture(position, Texture2D.whiteTexture);
		GUI.color = (flag ? Color.white : Color.grey);
		GUI.Label(position, (flag ? "> " : "") + content);
		buttonIndex++;
		if (flag)
		{
			if (Event.current.type != EventType.Repaint || !InputManager.ActiveDevice.Action1.WasPressed)
			{
				if (Event.current.type == EventType.KeyDown)
				{
					return Event.current.keyCode == KeyCode.Z;
				}
				return false;
			}
			return true;
		}
		return false;
	}

	protected bool SafetyCheatButton(ref int buttonIndex, string content)
	{
		int num = ((selectedButtonIndex == buttonIndex) ? safetyCounter : 0);
		if (CheatButton(ref buttonIndex, content + " [" + num + "/" + 10 + "]"))
		{
			if (safetyCounter < 10)
			{
				safetyCounter++;
			}
			return safetyCounter >= 10;
		}
		return false;
	}

	private void Restore()
	{
		GameManager unsafeInstance = GameManager.UnsafeInstance;
		if (unsafeInstance != null)
		{
			HeroController hero_ctrl = unsafeInstance.hero_ctrl;
			if (hero_ctrl != null)
			{
				hero_ctrl.AddHealth(unsafeInstance.playerData.GetInt("maxHealth") - unsafeInstance.playerData.GetInt("health"));
				hero_ctrl.AddMPCharge(unsafeInstance.playerData.GetInt("maxMP") - unsafeInstance.playerData.GetInt("MPCharge"));
			}
		}
	}

	private void Kill()
	{
		GameManager unsafeInstance = GameManager.UnsafeInstance;
		if (unsafeInstance != null)
		{
			HeroController hero_ctrl = unsafeInstance.hero_ctrl;
			if (hero_ctrl != null)
			{
				PlayerData.instance.SetBool("isInvincible", false);
				hero_ctrl.TakeDamage(null, CollisionSide.other, 9999, 0);
			}
		}
	}

	private void GetGeo(int amount)
	{
		GameManager unsafeInstance = GameManager.UnsafeInstance;
		if (unsafeInstance != null)
		{
			unsafeInstance.playerData.AddGeo(amount);
		}
	}
}
