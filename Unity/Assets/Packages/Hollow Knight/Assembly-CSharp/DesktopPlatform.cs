using System;
using System.Collections.Generic;
using System.IO;
using InControl;
using UnityEngine;

public class DesktopPlatform : Platform, VibrationManager.IVibrationMixerProvider
{
	private delegate DesktopOnlineSubsystem CreateOnlineSubsystemDelegate();

	private string saveDirPath;

	private PlayerPrefsSharedData sharedData;

	private PlayerPrefsSharedData encryptedSharedData;

	private DesktopOnlineSubsystem onlineSubsystem;

	private PlatformVibrationHelper vibrationHelper;

	public override string DisplayName => "Desktop";

	public override ISharedData SharedData => sharedData;

	public override ISharedData EncryptedSharedData => encryptedSharedData;

	public override bool AreAchievementsFetched => onlineSubsystem?.AreAchievementsFetched ?? true;

	public override bool HasNativeAchievementsDialog => onlineSubsystem?.HasNativeAchievementsDialog ?? base.HasNativeAchievementsDialog;

	public override AcceptRejectInputStyles AcceptRejectInputStyle => AcceptRejectInputStyles.NonJapaneseStyle;

	public override bool ShowLanguageSelect => true;

	public override bool IsControllerImplicit
	{
		get
		{
			if ((bool)InputHandler.Instance && InputHandler.Instance.lastActiveController == BindingSourceType.DeviceBindingSource)
			{
				return true;
			}
			return false;
		}
	}

	public override bool WillPreloadSaveFiles => onlineSubsystem?.WillPreloadSaveFiles ?? base.WillPreloadSaveFiles;

	public override EngagementRequirements EngagementRequirement => onlineSubsystem?.EngagementRequirement ?? base.EngagementRequirement;

	public override EngagementStates EngagementState => onlineSubsystem?.EngagementState ?? base.EngagementState;

	public override string EngagedDisplayName => onlineSubsystem?.EngagedDisplayName ?? base.EngagedDisplayName;

	public override Texture2D EngagedDisplayImage => onlineSubsystem?.EngagedDisplayImage ?? base.EngagedDisplayImage;

	protected override void Awake()
	{
		base.Awake();
		saveDirPath = Application.persistentDataPath;
		sharedData = new PlayerPrefsSharedData(null);
		encryptedSharedData = new PlayerPrefsSharedData(Platform.HollowKnightAESKeyBytes);
		CreateOnlineSubsystem();
		if (onlineSubsystem == null)
		{
			OnOnlineSubsystemAchievementsFetched();
		}
		vibrationHelper = new PlatformVibrationHelper();
	}

	private void CreateOnlineSubsystem()
	{
		List<CreateOnlineSubsystemDelegate> list = new List<CreateOnlineSubsystemDelegate>();
		if (SteamOnlineSubsystem.IsPackaged(this))
		{
			list.Add(() => new SteamOnlineSubsystem(this));
		}
		if (GOGGalaxyOnlineSubsystem.IsPackaged(this))
		{
			list.Add(() => new GOGGalaxyOnlineSubsystem(this));
		}
		if (list.Count == 0)
		{
			Debug.LogFormat(this, "No online subsystems packaged.");
			return;
		}
		if (list.Count > 1)
		{
			Debug.LogErrorFormat(this, "Multiple online subsystems packaged.");
			Application.Quit();
			return;
		}
		onlineSubsystem = list[0]();
		Debug.LogFormat(this, "Selected online subsystem " + onlineSubsystem.GetType().Name);
		if (onlineSubsystem is GOGGalaxyOnlineSubsystem gOGGalaxyOnlineSubsystem && !gOGGalaxyOnlineSubsystem.DidInitialize)
		{
			onlineSubsystem = null;
			Debug.LogError("GOG was not initialised, will not be used as online subsystem.");
		}
	}

	protected override void OnDestroy()
	{
		if (onlineSubsystem != null)
		{
			onlineSubsystem.Dispose();
			onlineSubsystem = null;
		}
		vibrationHelper.Destroy();
		base.OnDestroy();
	}

	protected override void Update()
	{
		base.Update();
		if (onlineSubsystem != null)
		{
			onlineSubsystem.Update();
		}
		vibrationHelper.UpdateVibration();
	}

	private string GetSaveSlotPath(int slotIndex, SaveSlotFileNameUsage usage)
	{
		return Path.Combine(saveDirPath, GetSaveSlotFileName(slotIndex, usage));
	}

	public override void IsSaveSlotInUse(int slotIndex, Action<bool> callback)
	{
		DesktopOnlineSubsystem desktopOnlineSubsystem = onlineSubsystem;
		if (desktopOnlineSubsystem != null && desktopOnlineSubsystem.HandlesGameSaves)
		{
			onlineSubsystem.IsSaveSlotInUse(slotIndex, callback);
		}
		else
		{
			LocalIsSaveSlotInUse(slotIndex, callback);
		}
	}

	public void LocalIsSaveSlotInUse(int slotIndex, Action<bool> callback)
	{
		string saveSlotPath = GetSaveSlotPath(slotIndex, SaveSlotFileNameUsage.Primary);
		bool inUse;
		try
		{
			inUse = File.Exists(saveSlotPath);
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
			inUse = false;
		}
		CoreLoop.InvokeNext(delegate
		{
			callback?.Invoke(inUse);
		});
	}

	public override void ReadSaveSlot(int slotIndex, Action<byte[]> callback)
	{
		DesktopOnlineSubsystem desktopOnlineSubsystem = onlineSubsystem;
		if (desktopOnlineSubsystem != null && desktopOnlineSubsystem.HandlesGameSaves)
		{
			onlineSubsystem.ReadSaveSlot(slotIndex, callback);
		}
		else
		{
			LocalReadSaveSlot(slotIndex, callback);
		}
	}

	public void LocalReadSaveSlot(int slotIndex, Action<byte[]> callback)
	{
		string saveSlotPath = GetSaveSlotPath(slotIndex, SaveSlotFileNameUsage.Primary);
		byte[] bytes;
		try
		{
			bytes = File.ReadAllBytes(saveSlotPath);
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
			bytes = null;
		}
		CoreLoop.InvokeNext(delegate
		{
			callback?.Invoke(bytes);
		});
	}

	public override void EnsureSaveSlotSpace(int slotIndex, Action<bool> callback)
	{
		CoreLoop.InvokeNext(delegate
		{
			callback?.Invoke(obj: true);
		});
	}

	public override void WriteSaveSlot(int slotIndex, byte[] bytes, Action<bool> callback)
	{
		DesktopOnlineSubsystem desktopOnlineSubsystem = onlineSubsystem;
		if (desktopOnlineSubsystem != null && desktopOnlineSubsystem.HandlesGameSaves)
		{
			onlineSubsystem.WriteSaveSlot(slotIndex, bytes, callback);
			return;
		}
		string saveSlotPath = GetSaveSlotPath(slotIndex, SaveSlotFileNameUsage.Primary);
		string saveSlotPath2 = GetSaveSlotPath(slotIndex, SaveSlotFileNameUsage.Backup);
		string text = saveSlotPath + ".new";
		if (File.Exists(text))
		{
			Debug.LogWarning($"Temp file <b>{text}</b> was found and is likely corrupted. The file has been deleted.");
		}
		try
		{
			File.WriteAllBytes(text, bytes);
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
		try
		{
			File.WriteAllBytes(saveSlotPath.Replace(".dat", "") + "_1.5.78.11833.dat", bytes);
		}
		catch (Exception exception2)
		{
			Debug.LogException(exception2);
		}
		bool successful;
		try
		{
			if (File.Exists(saveSlotPath))
			{
				File.Replace(text, saveSlotPath, saveSlotPath2 + GetBackupNumber(saveSlotPath2));
			}
			else
			{
				File.Move(text, saveSlotPath);
			}
			successful = true;
		}
		catch (Exception exception3)
		{
			Debug.LogException(exception3);
			successful = false;
		}
		CoreLoop.InvokeNext(delegate
		{
			if (callback != null)
			{
				callback(successful);
			}
		});
	}

	private int GetBackupNumber(string backupPath)
	{
		int num = 0;
		int num2 = 3;
		string[] files = Directory.GetFiles(Path.GetDirectoryName(backupPath));
		List<string> list = new List<string>();
		string[] array = files;
		foreach (string text in array)
		{
			if (text.StartsWith(backupPath))
			{
				list.Add(text);
			}
		}
		if (list.Count > 0)
		{
			int index = 0;
			int num3 = int.MaxValue;
			int num4 = 0;
			for (int num5 = list.Count - 1; num5 >= 0; num5--)
			{
				string text2 = list[num5].Replace(backupPath, "");
				if (text2 != "")
				{
					try
					{
						num = int.Parse(text2);
						if (num < num3)
						{
							num3 = num;
							index = num5;
						}
						if (num > num4)
						{
							num4 = num;
						}
					}
					catch
					{
						Debug.LogWarning($"Backup file: {list[num5]} does not have a numerical extension, and will be ignored.");
					}
				}
			}
			num = num4;
			if (list.Count >= num2)
			{
				File.Delete(list[index]);
			}
		}
		return num + 1;
	}

	public override void ClearSaveSlot(int slotIndex, Action<bool> callback)
	{
		DesktopOnlineSubsystem desktopOnlineSubsystem = onlineSubsystem;
		if (desktopOnlineSubsystem != null && desktopOnlineSubsystem.HandlesGameSaves)
		{
			onlineSubsystem.ClearSaveSlot(slotIndex, callback);
			return;
		}
		string saveSlotPath = GetSaveSlotPath(slotIndex, SaveSlotFileNameUsage.Primary);
		bool successful;
		try
		{
			File.Delete(saveSlotPath);
			successful = true;
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
			successful = false;
		}
		CoreLoop.InvokeNext(delegate
		{
			if (callback != null)
			{
				callback(successful);
			}
		});
	}

	public override bool? IsAchievementUnlocked(string achievementId)
	{
		if (onlineSubsystem != null)
		{
			return onlineSubsystem.IsAchievementUnlocked(achievementId);
		}
		return EncryptedSharedData.GetBool(achievementId, def: false);
	}

	public override void PushAchievementUnlock(string achievementId)
	{
		if (onlineSubsystem != null)
		{
			onlineSubsystem.PushAchievementUnlock(achievementId);
		}
		EncryptedSharedData.SetBool(achievementId, val: true);
	}

	public override void ResetAchievements()
	{
		if (onlineSubsystem != null)
		{
			onlineSubsystem.ResetAchievements();
		}
	}

	public bool IncludesPlugin(string pluginName)
	{
		string path = "Plugins";
		string path2 = Path.Combine(Path.Combine(Application.dataPath, path), pluginName);
		if (!File.Exists(path2))
		{
			return Directory.Exists(path2);
		}
		return true;
	}

	public void OnOnlineSubsystemAchievementsFetched()
	{
		OnAchievementsFetched();
	}

	public VibrationMixer GetVibrationMixer()
	{
		return vibrationHelper.GetMixer();
	}
}
