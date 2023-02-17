using System;
using System.Collections.Generic;
using GlobalEnums;
using Modding;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class SceneManager : MonoBehaviour
{
	[Space(6f)]
	[Tooltip("This denotes the type of this scene, mainly if it is a gameplay scene or not.")]
	public SceneType sceneType;

	[Header("Gameplay Scene Settings")]
	[Tooltip("The area of the map this scene belongs to.")]
	[Space(6f)]
	public MapZone mapZone;

	[Tooltip("Determines if this area is currently windy.")]
	public bool isWindy;

	[Tooltip("Determines if this level experiences tremors.")]
	public bool isTremorZone;

	[Tooltip("Set environment type on scene entry. 0 = Dust, 1 = Grass, 2 = Bone, 3 = Spa, 4 = Metal, 5 = No Effect, 6 = Wet")]
	public int environmentType;

	public int darknessLevel;

	public bool noLantern;

	[Header("Camera Color Correction Curves")]
	[Range(0f, 5f)]
	public float saturation;

	public bool ignorePlatformSaturationModifiers;

	public AnimationCurve redChannel;

	public AnimationCurve greenChannel;

	public AnimationCurve blueChannel;

	[Header("Ambient Light")]
	[Tooltip("The default ambient light colour for this scene.")]
	[Space(6f)]
	public Color defaultColor;

	[Tooltip("The intensity of the ambient light in this scene.")]
	[Range(0f, 1f)]
	public float defaultIntensity;

	[Header("Hero Light")]
	[Tooltip("Color of the hero's light gradient (not point lights)")]
	[Space(6f)]
	public Color heroLightColor;

	[Header("Scene Particles")]
	public bool noParticles;

	public MapZone overrideParticlesWith;

	[Header("Audio Snapshots")]
	[Space(6f)]
	[SerializeField]
	private AtmosCue atmosCue;

	[SerializeField]
	private MusicCue musicCue;

	[SerializeField]
	private MusicCue infectedMusicCue;

	[SerializeField]
	private AudioMixerSnapshot musicSnapshot;

	[SerializeField]
	private float musicDelayTime;

	[SerializeField]
	private float musicTransitionTime;

	public AudioMixerSnapshot atmosSnapshot;

	public AudioMixerSnapshot enviroSnapshot;

	public AudioMixerSnapshot actorSnapshot;

	public AudioMixerSnapshot shadeSnapshot;

	public float transitionTime;

	[Header("Scene Border")]
	[Space(6f)]
	public GameObject borderPrefab;

	[Header("Mapping")]
	[Space(6f)]
	public bool manualMapTrigger;

	[Header("Object Spawns")]
	[Space(6f)]
	public GameObject hollowShadeObject;

	public GameObject dreamgateObject;

	private GameManager gm;

	private GameCameras gc;

	private HeroController heroCtrl;

	private PlayerData pd;

	private float enviroTimer;

	private bool enviroSent;

	private bool heroInfoSent;

	private bool setSaturation;

	private bool isGameplayScene;

	public static float AmbientIntesityMix = 0.5f;

	private const float SwitchConstant = 0.17f;

	private const float SwitchConstantGG = 0.1466f;

	private const float RegularConstant = 0.1466f;

	private bool gameplayScene;

	private void Start()
	{
		try
		{
			orig_Start();
		}
		catch (NullReferenceException) when (!ModLoader.LoadState.HasFlag(ModLoader.ModLoadState.Preloaded))
		{
		}
	}

	public static void SetLighting(Color ambientLightColor, float ambientLightIntensity)
	{
		float num = Mathf.Lerp(1f, ambientLightIntensity, AmbientIntesityMix);
		RenderSettings.ambientLight = new Color(ambientLightColor.r * num, ambientLightColor.g * num, ambientLightColor.b * num, 1f);
		RenderSettings.ambientIntensity = 1f;
	}

	private void Update()
	{
		if (gameplayScene && !heroInfoSent && heroCtrl != null && (heroCtrl.heroLight == null || heroCtrl.heroLight.material == null))
		{
			heroCtrl.SetDarkness(darknessLevel);
			heroInfoSent = true;
		}
		orig_Update();
	}

	public int GetDarknessLevel()
	{
		return darknessLevel;
	}

	public void SetWindy(bool setting)
	{
		isWindy = setting;
	}

	public float AdjustSaturation(float originalSaturation)
	{
		if (ignorePlatformSaturationModifiers)
		{
			return originalSaturation;
		}
		return AdjustSaturationForPlatform(originalSaturation, mapZone);
	}

	public static float AdjustSaturationForPlatform(float originalSaturation, MapZone? mapZone = null)
	{
		if (Application.platform == RuntimePlatform.Switch)
		{
			if (mapZone.HasValue && mapZone == MapZone.GODS_GLORY)
			{
				return originalSaturation + 0.1466f;
			}
			return originalSaturation + 0.17f;
		}
		return originalSaturation + 0.1466f;
	}

	private void PrintDebugInfo()
	{
		string text = "SM Setting Curves to ";
		text += "R: (";
		Keyframe[] keys = redChannel.keys;
		foreach (Keyframe keyframe in keys)
		{
			text = text + keyframe.value + ", ";
		}
		text += ") G: (";
		keys = greenChannel.keys;
		foreach (Keyframe keyframe2 in keys)
		{
			text = text + keyframe2.value + ", ";
		}
		text += " ) B: (";
		keys = blueChannel.keys;
		foreach (Keyframe keyframe3 in keys)
		{
			text = text + keyframe3.value + ", ";
		}
		text = text + ") S: " + saturation;
		Debug.Log(text);
	}

	private void DrawBlackBorders()
	{
		List<GameObject> list = new List<GameObject>();
		GameObject gameObject = UnityEngine.Object.Instantiate(borderPrefab);
		gameObject.transform.SetPosition2D(gm.sceneWidth + 10f, gm.sceneHeight / 2f);
		gameObject.transform.localScale = new Vector2(20f, gm.sceneHeight + 40f);
		list.Add(gameObject);
		gameObject = UnityEngine.Object.Instantiate(borderPrefab);
		gameObject.transform.SetPosition2D(-10f, gm.sceneHeight / 2f);
		gameObject.transform.localScale = new Vector2(20f, gm.sceneHeight + 40f);
		list.Add(gameObject);
		gameObject = UnityEngine.Object.Instantiate(borderPrefab);
		gameObject.transform.SetPosition2D(gm.sceneWidth / 2f, gm.sceneHeight + 10f);
		gameObject.transform.localScale = new Vector2(40f + gm.sceneWidth, 20f);
		list.Add(gameObject);
		gameObject = UnityEngine.Object.Instantiate(borderPrefab);
		gameObject.transform.SetPosition2D(gm.sceneWidth / 2f, -10f);
		gameObject.transform.localScale = new Vector2(40f + gm.sceneWidth, 20f);
		list.Add(gameObject);
		ModHooks.OnDrawBlackBorders(list);
	}

	private void AddSceneMapped()
	{
		if (!pd.GetVariable<List<string>>("scenesVisited").Contains(gm.GetSceneNameString()) && !manualMapTrigger && mapZone != MapZone.WHITE_PALACE && mapZone != MapZone.GODS_GLORY)
		{
			pd.GetVariable<List<string>>("scenesVisited").Add(gm.GetSceneNameString());
		}
	}

	public void UpdateSceneSettings(SceneManagerSettings sms)
	{
		mapZone = sms.mapZone;
		defaultColor = new Color(sms.defaultColor.r, sms.defaultColor.g, sms.defaultColor.b, sms.defaultColor.a);
		defaultIntensity = sms.defaultIntensity;
		saturation = sms.saturation;
		redChannel = new AnimationCurve(sms.redChannel.keys.Clone() as Keyframe[]);
		greenChannel = new AnimationCurve(sms.greenChannel.keys.Clone() as Keyframe[]);
		blueChannel = new AnimationCurve(sms.blueChannel.keys.Clone() as Keyframe[]);
		heroLightColor = new Color(sms.heroLightColor.r, sms.heroLightColor.g, sms.heroLightColor.b, sms.heroLightColor.a);
	}

	private void orig_Update()
	{
		if (!isGameplayScene)
		{
			return;
		}
		if (enviroTimer < 0.25f)
		{
			enviroTimer += Time.deltaTime;
		}
		else if (!enviroSent && heroCtrl != null)
		{
			heroCtrl.checkEnvironment();
			enviroSent = true;
		}
		if (!heroInfoSent && heroCtrl != null)
		{
			heroCtrl.heroLight.material.SetColor("_Color", Color.white);
			heroCtrl.SetDarkness(darknessLevel);
			heroInfoSent = true;
		}
		if (!setSaturation)
		{
			if (AdjustSaturation(saturation) != gc.colorCorrectionCurves.saturation)
			{
				gc.colorCorrectionCurves.saturation = AdjustSaturation(saturation);
			}
			setSaturation = true;
		}
	}

	private void orig_Start()
	{
		gm = GameManager.instance;
		gc = GameCameras.instance;
		pd = PlayerData.instance;
		if (gm.IsGameplayScene())
		{
			isGameplayScene = true;
			heroCtrl = HeroController.instance;
		}
		else
		{
			isGameplayScene = false;
		}
		gc.colorCorrectionCurves.saturation = AdjustSaturation(saturation);
		gc.colorCorrectionCurves.redChannel = redChannel;
		gc.colorCorrectionCurves.greenChannel = greenChannel;
		gc.colorCorrectionCurves.blueChannel = blueChannel;
		gc.colorCorrectionCurves.UpdateParameters();
		gc.sceneColorManager.SaturationA = AdjustSaturation(saturation);
		gc.sceneColorManager.RedA = redChannel;
		gc.sceneColorManager.GreenA = greenChannel;
		gc.sceneColorManager.BlueA = blueChannel;
		SetLighting(defaultColor, defaultIntensity);
		gc.sceneColorManager.AmbientColorA = defaultColor;
		gc.sceneColorManager.AmbientIntensityA = defaultIntensity;
		if (isGameplayScene)
		{
			if (heroCtrl != null)
			{
				heroCtrl.heroLight.color = heroLightColor;
			}
			gc.sceneColorManager.HeroLightColorA = heroLightColor;
		}
		pd.SetIntSwappedArgs(environmentType, "environmentType");
		pd.SetIntSwappedArgs(environmentType, "environmentTypeDefault");
		if ((bool)GameManager.instance)
		{
			GameManager.EnterSceneEvent temp = null;
			temp = delegate
			{
				AddSceneMapped();
				GameManager.instance.OnFinishedEnteringScene -= temp;
			};
			GameManager.instance.OnFinishedEnteringScene += temp;
		}
		else
		{
			AddSceneMapped();
		}
		if (atmosCue != null)
		{
			gm.AudioManager.ApplyAtmosCue(atmosCue, transitionTime);
		}
		MusicCue musicCue = this.musicCue;
		if (gm.playerData.GetBool("crossroadsInfected") && infectedMusicCue != null)
		{
			musicCue = infectedMusicCue;
		}
		if (musicCue != null)
		{
			gm.AudioManager.ApplyMusicCue(musicCue, musicDelayTime, musicTransitionTime, applySnapshot: false);
		}
		if (musicSnapshot != null)
		{
			gm.AudioManager.ApplyMusicSnapshot(musicSnapshot, musicDelayTime, musicTransitionTime);
		}
		if (enviroSnapshot != null)
		{
			enviroSnapshot.TransitionTo(transitionTime);
		}
		if (actorSnapshot != null)
		{
			actorSnapshot.TransitionTo(transitionTime);
		}
		if (shadeSnapshot != null)
		{
			shadeSnapshot.TransitionTo(transitionTime);
		}
		if (sceneType == SceneType.GAMEPLAY)
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag("Vignette");
			if ((bool)gameObject)
			{
				PlayMakerFSM playMakerFSM = FSMUtility.LocateFSM(gameObject, "Darkness Control");
				if ((bool)playMakerFSM)
				{
					FSMUtility.SetInt(playMakerFSM, "Darkness Level", darknessLevel);
				}
				if (!noLantern)
				{
					FSMUtility.LocateFSM(gameObject, "Darkness Control").SendEvent("RESET");
				}
				else
				{
					FSMUtility.LocateFSM(gameObject, "Darkness Control").SendEvent("SCENE RESET NO LANTERN");
					if (heroCtrl != null)
					{
						heroCtrl.wieldingLantern = false;
					}
				}
			}
		}
		if (isGameplayScene)
		{
			DrawBlackBorders();
		}
		if (pd.GetBool("soulLimited") && isGameplayScene && pd.GetString("shadeScene") == base.gameObject.scene.name)
		{
			GameObject obj = UnityEngine.Object.Instantiate(hollowShadeObject, new Vector3(pd.GetFloat("shadePositionX"), pd.GetFloat("shadePositionY"), 0.006f), Quaternion.identity);
			obj.transform.SetParent(base.transform, worldPositionStays: true);
			obj.transform.SetParent(null);
		}
		if (isGameplayScene && pd.GetString("dreamGateScene") == base.gameObject.scene.name)
		{
			GameObject obj2 = UnityEngine.Object.Instantiate(dreamgateObject, new Vector3(pd.GetFloat("dreamGateX"), pd.GetFloat("dreamGateY") - 1.429361f, -0.002f), Quaternion.identity);
			obj2.transform.SetParent(base.transform, worldPositionStays: true);
			obj2.transform.SetParent(null);
		}
	}
}
