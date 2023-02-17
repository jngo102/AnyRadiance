using System;
using System.Text;
using Modding;
using UnityEngine;

public abstract class Platform : MonoBehaviour
{
	protected enum SaveSlotFileNameUsage
	{
		Primary,
		Backup,
		BackupMarkedForDeletion
	}

	public interface ISharedData
	{
		bool HasKey(string key);

		void DeleteKey(string key);

		void DeleteAll();

		bool GetBool(string key, bool def);

		void SetBool(string key, bool val);

		int GetInt(string key, int def);

		void SetInt(string key, int val);

		float GetFloat(string key, float def);

		void SetFloat(string key, float val);

		string GetString(string key, string def);

		void SetString(string key, string val);

		void Save();
	}

	public delegate void AchievementsFetchedDelegate();

	public enum GraphicsTiers
	{
		VeryLow,
		Low,
		Medium,
		High
	}

	public delegate void GraphicsTierChangedDelegate(GraphicsTiers graphicsTier);

	public enum AcceptRejectInputStyles
	{
		NonJapaneseStyle,
		JapaneseStyle
	}

	public delegate void AcceptRejectInputStyleChangedDelegate();

	public enum MenuActions
	{
		None,
		Submit,
		Cancel
	}

	public enum EngagementRequirements
	{
		Invisible,
		MustDisplay
	}

	public enum EngagementStates
	{
		NotEngaged,
		EngagePending,
		Engaged
	}

	public interface IDisengageHandler
	{
		void OnDisengage(Action next);
	}

	public delegate void OnEngagedDisplayInfoChanged();

	protected static readonly byte[] HollowKnightAESKeyBytes = Encoding.UTF8.GetBytes("UKu52ePUBwetZ9wNX88o54dnfKRu0T1l");

	public const int SaveSlotCount = 5;

	protected const string FirstSaveFileName = "user.dat";

	protected const string NonFirstSaveFileNameFormat = "user{0}.dat";

	protected const string BackupSuffix = ".bak";

	protected const string BackupMarkedForDeletionSuffix = ".del";

	private GraphicsTiers graphicsTier;

	private IDisengageHandler disengageHandler;

	private static Platform current;

	public abstract string DisplayName { get; }

	public abstract bool ShowLanguageSelect { get; }

	public virtual bool IsFileSystemProtected => false;

	public virtual bool WillPreloadSaveFiles => true;

	public virtual bool IsSaveStoreMounted => true;

	public abstract ISharedData SharedData { get; }

	public abstract ISharedData EncryptedSharedData { get; }

	public virtual bool IsFiringAchievementsFromSavesAllowed => true;

	public abstract bool AreAchievementsFetched { get; }

	public virtual bool HasNativeAchievementsDialog => false;

	public virtual bool WillManageResolution => false;

	public virtual bool WillDisplayGraphicsSettings => true;

	protected virtual GraphicsTiers InitialGraphicsTier => GraphicsTiers.High;

	public GraphicsTiers GraphicsTier => graphicsTier;

	public virtual bool IsSpriteScalingApplied => false;

	public virtual float SpriteScalingFactor => 1f;

	public virtual bool WillDisplayControllerSettings => true;

	public virtual bool WillDisplayKeyboardSettings => true;

	public virtual bool WillDisplayQuitButton => true;

	public virtual bool IsControllerImplicit => false;

	public virtual bool IsMouseSupported => true;

	public virtual bool WillEverPauseOnControllerDisconnected => false;

	public virtual bool IsPausingOnControllerDisconnected => false;

	public abstract AcceptRejectInputStyles AcceptRejectInputStyle { get; }

	public virtual bool FetchScenesBeforeFade => false;

	public virtual float MaximumLoadDurationForNonCriticalGarbageCollection => 0f;

	public virtual int MaximumSceneTransitionsWithoutNonCriticalGarbageCollection => 0;

	public virtual EngagementRequirements EngagementRequirement => EngagementRequirements.Invisible;

	public virtual EngagementStates EngagementState => EngagementStates.Engaged;

	public virtual bool IsSavingAllowedByEngagement => true;

	public virtual bool CanReEngage => false;

	public virtual string EngagedDisplayName => null;

	public virtual Texture2D EngagedDisplayImage => null;

	public IDisengageHandler DisengageHandler => disengageHandler;

	public virtual bool IsPlayerPrefsLoaded => true;

	public virtual bool RequiresPreferencesSyncOnEngage => false;

	public static Platform Current => current;

	protected static bool IsPlatformSimulationEnabled => false;

	public static event Action PlatformBecameCurrent;

	public static event AchievementsFetchedDelegate AchievementsFetched;

	public static event GraphicsTierChangedDelegate GraphicsTierChanged;

	public static event AcceptRejectInputStyleChangedDelegate AcceptRejectInputStyleChanged;

	public event OnEngagedDisplayInfoChanged EngagedDisplayInfoChanged;

	public virtual SystemLanguage GetSystemLanguage()
	{
		return Application.systemLanguage;
	}

	public virtual void MountSaveStore()
	{
	}

	public static bool IsSaveSlotIndexValid(int slotIndex)
	{
		return true;
	}

	protected string GetSaveSlotFileName(int slotIndex, SaveSlotFileNameUsage usage)
	{
		string text = ((slotIndex == 0) ? "user.dat" : $"user{slotIndex}.dat");
		switch (usage)
		{
		case SaveSlotFileNameUsage.Backup:
			text += ".bak";
			break;
		case SaveSlotFileNameUsage.BackupMarkedForDeletion:
			text += ".del";
			break;
		}
		return text;
	}

	public abstract void IsSaveSlotInUse(int slotIndex, Action<bool> callback);

	public abstract void ReadSaveSlot(int slotIndex, Action<byte[]> callback);

	public abstract void EnsureSaveSlotSpace(int slotIndex, Action<bool> callback);

	public abstract void WriteSaveSlot(int slotIndex, byte[] binary, Action<bool> callback);

	public abstract void ClearSaveSlot(int slotIndex, Action<bool> callback);

	public virtual void AdjustGameSettings(GameSettings gameSettings)
	{
	}

	public abstract bool? IsAchievementUnlocked(string achievementId);

	public abstract void PushAchievementUnlock(string achievementId);

	public abstract void ResetAchievements();

	protected void OnAchievementsFetched()
	{
		if (Platform.AchievementsFetched != null)
		{
			Platform.AchievementsFetched();
		}
	}

	public virtual void ShowNativeAchievementsDialog()
	{
	}

	public virtual void SetSocialPresence(string socialStatusKey, bool isActive)
	{
	}

	public virtual void AddSocialStat(string name, int amount)
	{
	}

	public virtual void FlushSocialEvents()
	{
	}

	public virtual void AdjustGraphicsSettings(GameSettings gameSettings)
	{
	}

	protected void ChangeGraphicsTier(GraphicsTiers graphicsTier, bool isForced)
	{
		if (this.graphicsTier != graphicsTier || isForced)
		{
			this.graphicsTier = graphicsTier;
			Debug.LogFormat(this, "Graphics tier changed to {0}", graphicsTier);
			OnGraphicsTierChanged(graphicsTier);
		}
	}

	protected virtual void OnGraphicsTierChanged(GraphicsTiers graphicsTier)
	{
		Shader.globalMaximumLOD = GetMaximumShaderLOD(graphicsTier);
		if (Platform.GraphicsTierChanged != null)
		{
			Platform.GraphicsTierChanged(graphicsTier);
		}
	}

	public static int GetMaximumShaderLOD(GraphicsTiers graphicsTier)
	{
		return graphicsTier switch
		{
			GraphicsTiers.VeryLow => 700, 
			GraphicsTiers.Low => 800, 
			GraphicsTiers.Medium => 900, 
			GraphicsTiers.High => 1000, 
			_ => int.MaxValue, 
		};
	}

	public MenuActions GetMenuAction(bool menuSubmitInput, bool menuCancelInput, bool jumpInput, bool attackInput, bool castInput)
	{
		bool isControllerImplicit = IsControllerImplicit;
		if (isControllerImplicit)
		{
			if (menuSubmitInput)
			{
				return MenuActions.Submit;
			}
			if (menuCancelInput)
			{
				return MenuActions.Cancel;
			}
		}
		switch (AcceptRejectInputStyle)
		{
		case AcceptRejectInputStyles.NonJapaneseStyle:
			if (jumpInput)
			{
				return MenuActions.Submit;
			}
			if (attackInput || castInput)
			{
				return MenuActions.Cancel;
			}
			break;
		case AcceptRejectInputStyles.JapaneseStyle:
			if (castInput)
			{
				return MenuActions.Submit;
			}
			if (jumpInput || attackInput)
			{
				return MenuActions.Cancel;
			}
			break;
		}
		if (!isControllerImplicit)
		{
			if (menuSubmitInput)
			{
				return MenuActions.Submit;
			}
			if (menuCancelInput)
			{
				return MenuActions.Cancel;
			}
		}
		return MenuActions.None;
	}

	public virtual void ClearEngagement()
	{
	}

	public virtual void UpdateWaitingForEngagement()
	{
	}

	public virtual void SetDisengageHandler(IDisengageHandler disengageHandler)
	{
		this.disengageHandler = disengageHandler;
	}

	public void NotifyEngagedDisplayInfoChanged()
	{
		if (this.EngagedDisplayInfoChanged != null)
		{
			this.EngagedDisplayInfoChanged();
		}
	}

	protected virtual void Awake()
	{
		ChangeGraphicsTier(InitialGraphicsTier, isForced: true);
	}

	protected virtual void OnDestroy()
	{
	}

	protected virtual void Update()
	{
	}

	protected virtual void BecomeCurrent()
	{
		current = this;
		if (Platform.PlatformBecameCurrent != null)
		{
			Platform.PlatformBecameCurrent();
		}
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void Init()
	{
		CreatePlatform().BecomeCurrent();
	}

	private static Platform CreatePlatform()
	{
		return CreatePlatform<DesktopPlatform>();
	}

	private static Platform CreatePlatform<PlatformTy>() where PlatformTy : Platform
	{
		GameObject obj = new GameObject(typeof(PlatformTy).Name);
		PlatformTy result = obj.AddComponent<PlatformTy>();
		UnityEngine.Object.DontDestroyOnLoad(obj);
		return result;
	}

	public static bool orig_IsSaveSlotIndexValid(int slotIndex)
	{
		if (slotIndex >= 0)
		{
			return slotIndex < 5;
		}
		return false;
	}

	protected string orig_GetSaveSlotFileName(int slotIndex, SaveSlotFileNameUsage usage)
	{
		string text = ((slotIndex != 0) ? $"user{slotIndex}.dat" : "user.dat");
		switch (usage)
		{
		case SaveSlotFileNameUsage.Backup:
			text += ".bak";
			break;
		case SaveSlotFileNameUsage.BackupMarkedForDeletion:
			text += ".del";
			break;
		}
		return text;
	}
}
