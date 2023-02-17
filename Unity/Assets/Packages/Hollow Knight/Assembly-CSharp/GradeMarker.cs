using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class GradeMarker : MonoBehaviour
{
	public bool enableGrade = true;

	private bool activating;

	private bool deactivating;

	[Header("Range")]
	public float maxIntensityRadius;

	public float cutoffRadius;

	[Header("Target Color Grade")]
	[Range(0f, 5f)]
	public float saturation;

	public AnimationCurve redChannel;

	public AnimationCurve greenChannel;

	public AnimationCurve blueChannel;

	[Header("Target Scene Lighting")]
	[Range(0f, 1f)]
	public float ambientIntensity;

	public Color ambientColor;

	[Header("Target Hero Light")]
	public Color heroLightColor;

	private GameManager gm;

	private GameCameras gc;

	private HeroController hero;

	private SceneColorManager scm;

	private int updateEvery = 2;

	private Vector2 heading;

	private float sqrNear;

	private float sqrFar;

	private float sqrEffectRange;

	private float t;

	private float u;

	private float origMaxIntensityRadius;

	private float origCutoffRadius;

	private float startMaxIntensityRadius;

	private float startCutoffRadius;

	private float finalMaxIntensityRadius;

	private float finalCutoffRadius;

	private float shrunkPercentage = 30f;

	[HideInInspector]
	public float easeDuration = 1.5f;

	private float easeTimer = 2f;

	private IEnumerator startup;

	protected void OnEnable()
	{
		gm = GameManager.instance;
		if (gm != null)
		{
			gm.UnloadingLevel += OnUnloadingLevel;
		}
	}

	protected void OnDisable()
	{
		if (gm != null)
		{
			gm.UnloadingLevel -= OnUnloadingLevel;
		}
	}

	private void Start()
	{
		if (startup != null)
		{
			StopCoroutine(startup);
		}
		startup = OnStart();
		StartCoroutine(startup);
	}

	private void OnUnloadingLevel()
	{
		Deactivate();
		base.enabled = false;
	}

	public void SetStartSizeForTrigger()
	{
		origCutoffRadius = cutoffRadius;
		origMaxIntensityRadius = maxIntensityRadius;
		cutoffRadius = origCutoffRadius * (shrunkPercentage / 100f);
		maxIntensityRadius = origMaxIntensityRadius * (shrunkPercentage / 100f);
		startCutoffRadius = cutoffRadius;
		startMaxIntensityRadius = maxIntensityRadius;
	}

	public void Activate()
	{
		heading = hero.transform.position - base.transform.position;
		sqrNear = maxIntensityRadius * maxIntensityRadius;
		sqrFar = cutoffRadius * cutoffRadius;
		sqrEffectRange = sqrFar - sqrNear;
		u = (heading.sqrMagnitude - sqrNear) / sqrEffectRange;
		t = Mathf.Clamp01(1f - u);
		scm.SaturationB = SceneManager.AdjustSaturationForPlatform(saturation);
		scm.RedB = redChannel;
		scm.GreenB = greenChannel;
		scm.BlueB = blueChannel;
		scm.AmbientColorB = ambientColor;
		scm.AmbientIntensityB = ambientIntensity;
		if (GameManager.instance.IsGameplayScene())
		{
			scm.HeroLightColorB = heroLightColor;
		}
		enableGrade = true;
		scm.MarkerActive(active: true);
	}

	public void Deactivate()
	{
		if (startup != null)
		{
			StopCoroutine(startup);
		}
		startup = null;
		if (scm == null)
		{
			enableGrade = false;
		}
		else
		{
			orig_Deactivate();
		}
	}

	public void ActivateGradual()
	{
		startCutoffRadius = cutoffRadius;
		startMaxIntensityRadius = maxIntensityRadius;
		finalCutoffRadius = origCutoffRadius;
		finalMaxIntensityRadius = origMaxIntensityRadius;
		cutoffRadius = startCutoffRadius;
		maxIntensityRadius = startMaxIntensityRadius;
		Activate();
		activating = true;
		deactivating = false;
		easeTimer = 0f;
	}

	public void DeactivateGradual()
	{
		startCutoffRadius = cutoffRadius;
		startMaxIntensityRadius = maxIntensityRadius;
		finalCutoffRadius = cutoffRadius * (shrunkPercentage / 100f);
		finalMaxIntensityRadius = maxIntensityRadius * (shrunkPercentage / 100f);
		activating = false;
		deactivating = true;
		easeTimer = 0f;
	}

	private void Update()
	{
		if (!(hero == null))
		{
			orig_Update();
		}
	}

	private void UpdateLow()
	{
		if (!(hero == null))
		{
			orig_UpdateLow();
		}
	}

	private void orig_Start()
	{
		gc = GameCameras.instance;
		scm = gc.sceneColorManager;
		hero = HeroController.instance;
		if (enableGrade)
		{
			Activate();
		}
	}

	private IEnumerator OnStart()
	{
		while (HeroController.instance == null)
		{
			yield return null;
		}
		orig_Start();
	}

	private void orig_Update()
	{
		if (Time.frameCount % updateEvery == 0)
		{
			UpdateLow();
		}
		if (!(easeTimer < easeDuration))
		{
			return;
		}
		easeTimer += Time.deltaTime;
		float num = easeTimer / easeDuration;
		maxIntensityRadius = Mathf.Lerp(startMaxIntensityRadius, finalMaxIntensityRadius, num);
		cutoffRadius = Mathf.Lerp(startCutoffRadius, finalCutoffRadius, num);
		if (activating)
		{
			if (easeTimer >= easeDuration)
			{
				activating = false;
			}
		}
		else if (deactivating && easeTimer >= easeDuration)
		{
			deactivating = false;
			enableGrade = false;
		}
		if (easeTimer > easeDuration)
		{
			easeTimer = easeDuration;
		}
	}

	private void orig_UpdateLow()
	{
		heading = hero.transform.position - base.transform.position;
		sqrNear = maxIntensityRadius * maxIntensityRadius;
		sqrFar = cutoffRadius * cutoffRadius;
		sqrEffectRange = sqrFar - sqrNear;
		u = (heading.sqrMagnitude - sqrNear) / sqrEffectRange;
		t = Mathf.Clamp01(1f - u);
		if (scm.startBufferActive)
		{
			scm.MarkerActive(active: true);
			scm.SetFactor(t);
			return;
		}
		bool markerActive = scm.markerActive;
		if (u < 0f)
		{
			scm.MarkerActive(active: false);
			scm.SetFactor(1f);
		}
		else if (u < 1.1f)
		{
			scm.MarkerActive(active: true);
			scm.SetFactor(t);
		}
		else
		{
			scm.MarkerActive(active: false);
			scm.SetFactor(0f);
		}
		if (markerActive != scm.markerActive)
		{
			scm.UpdateScript();
		}
	}

	public void orig_Deactivate()
	{
		enableGrade = false;
		scm.SetFactor(0f);
	}
}
