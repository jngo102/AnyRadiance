using System;
using System.IO;
using System.Text;
using Steamworks;
using UnityEngine;

public class SteamOnlineSubsystem : DesktopOnlineSubsystem
{
	private DesktopPlatform platform;

	private bool didInitialize;

	private bool statsReceived;

	private SteamAPIWarningMessageHook_t warningCallback;

	private Callback<GameOverlayActivated_t> gameOverlayCallback;

	private Callback<UserStatsReceived_t> statsReceivedCallback;

	private Callback<SteamShutdown_t> steamShutdownCallback;

	private Callback<UserAchievementStored_t> achievementStoredCallback;

	public override bool AreAchievementsFetched => statsReceived;

	public static bool IsPackaged(DesktopPlatform desktopPlatform)
	{
		return desktopPlatform.IncludesPlugin(Path.Combine("x86_64", "steam_api64.dll"));
	}

	public SteamOnlineSubsystem(DesktopPlatform platform)
	{
		this.platform = platform;
		if (!Packsize.Test())
		{
			Debug.LogErrorFormat("Steamworks packsize incorrect.");
		}
		if (!DllCheck.Test())
		{
			Debug.LogErrorFormat("Steamworks binaries out of date or missing.");
		}
		if (SteamAPI.RestartAppIfNecessary(new AppId_t(367520u)))
		{
			Debug.LogError("Application was not launched through Steam! Shutting down...");
			Application.Quit();
		}
		Debug.LogFormat("Steam initializing");
		if (didInitialize = SteamAPI.Init())
		{
			warningCallback = OnSteamLogMessage;
			SteamClient.SetWarningMessageHook(warningCallback);
			gameOverlayCallback = Callback<GameOverlayActivated_t>.Create(OnGameOverlayActivated);
			statsReceivedCallback = Callback<UserStatsReceived_t>.Create(OnStatsReceived);
			steamShutdownCallback = Callback<SteamShutdown_t>.Create(OnSteamShutdown);
			achievementStoredCallback = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
			string personaName = SteamFriends.GetPersonaName();
			Debug.LogFormat("Steam logged in as {0}", personaName);
			if (!SteamUserStats.RequestCurrentStats())
			{
				Debug.LogErrorFormat("Steam unable to request current stats.");
			}
		}
		else
		{
			Debug.LogErrorFormat("Steam failed to initialize");
		}
	}

	public override void Dispose()
	{
		if (didInitialize)
		{
			Debug.LogFormat("Shutting down Steam API.");
			SteamAPI.Shutdown();
		}
		base.Dispose();
	}

	public override void Update()
	{
		base.Update();
		SteamAPI.RunCallbacks();
	}

	private void OnSteamLogMessage(int severity, StringBuilder content)
	{
		string format = "Steam: " + content.ToString();
		if (severity == 1)
		{
			Debug.LogWarningFormat(format);
		}
		else
		{
			Debug.LogFormat(format);
		}
	}

	private void OnGameOverlayActivated(GameOverlayActivated_t ev)
	{
		Debug.LogFormat("Steam overlay became {0}.", (ev.m_bActive == 0) ? "closed" : "opened");
	}

	private void OnStatsReceived(UserStatsReceived_t ev)
	{
		if (ev.m_eResult == EResult.k_EResultOK)
		{
			statsReceived = true;
			Debug.LogFormat("Steam stats received.");
			platform.OnOnlineSubsystemAchievementsFetched();
		}
		else
		{
			Debug.LogErrorFormat("Steam failed to receive stats: {0}", ev.m_eResult);
		}
	}

	private void OnSteamShutdown(SteamShutdown_t ev)
	{
		Debug.LogFormat("Steam shut down.");
		didInitialize = false;
	}

	private void OnAchievementStored(UserAchievementStored_t ev)
	{
		Debug.LogFormat("Steam achievement {0} ({1}/{2}) upload complete", ev.m_rgchAchievementName, ev.m_nCurProgress, ev.m_nMaxProgress);
	}

	public override void PushAchievementUnlock(string achievementId)
	{
		if (didInitialize)
		{
			try
			{
				SteamUserStats.SetAchievement(achievementId);
				SteamUserStats.StoreStats();
				Debug.LogFormat("Pushing achievement {0}", achievementId);
				return;
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				return;
			}
		}
		Debug.LogErrorFormat("Unable to unlock achievement {0}, because Steam is not initialized", achievementId);
	}

	public override bool? IsAchievementUnlocked(string achievementId)
	{
		if (didInitialize)
		{
			try
			{
				if (!SteamUserStats.GetAchievement(achievementId, out var pbAchieved))
				{
					Debug.LogErrorFormat("Failed to retrieve achievement state for {0}", achievementId);
					return null;
				}
				return pbAchieved;
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				return null;
			}
		}
		Debug.LogErrorFormat("Unable to retrieve achievement state for {0}, because Steam is not initialized", achievementId);
		return null;
	}

	public override void ResetAchievements()
	{
		if (didInitialize)
		{
			try
			{
				SteamUserStats.ResetAllStats(bAchievementsToo: true);
				Debug.LogFormat("Reset all stats");
				return;
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				return;
			}
		}
		Debug.LogErrorFormat("Unable to reset all stats, because Steam is not initialized");
	}
}
