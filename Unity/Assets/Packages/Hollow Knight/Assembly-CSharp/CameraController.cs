using System.Collections;
using System.Collections.Generic;
using GlobalEnums;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

public class CameraController : MonoBehaviour
{
	public enum CameraMode
	{
		FROZEN,
		FOLLOWING,
		LOCKED,
		PANNING,
		FADEOUT,
		FADEIN,
		PREVIOUS
	}

	private bool verboseMode;

	public CameraMode mode;

	private CameraMode prevMode;

	public bool atSceneBounds;

	public bool atHorizontalSceneBounds;

	private bool isGameplayScene;

	private bool teleporting;

	public Vector3 lastFramePosition;

	public Vector2 lastLockPosition;

	private Coroutine fadeInFailSafeCo;

	[Header("Inspector Variables")]
	public float dampTime;

	public float dampTimeX;

	public float dampTimeY;

	public float dampTimeFalling;

	public float heroBotYLimit;

	private float panTime;

	private float currentPanTime;

	private Vector3 velocity;

	private Vector3 velocityX;

	private Vector3 velocityY;

	public float fallOffset;

	public float fallOffset_multiplier;

	public Vector3 destination;

	public float maxVelocity;

	public float maxVelocityFalling;

	private float maxVelocityCurrent;

	private float horizontalOffset;

	public float lookOffset;

	private float startLockedTimer;

	private float targetDeltaX;

	private float targetDeltaY;

	[HideInInspector]
	public Vector2 panToTarget;

	public float sceneWidth;

	public float sceneHeight;

	public float xLimit;

	public float yLimit;

	private CameraLockArea currentLockArea;

	private Vector3 panStartPos;

	private Vector3 panEndPos;

	public Camera cam;

	private HeroController hero_ctrl;

	private GameManager gm;

	public tk2dTileMap tilemap;

	public CameraTarget camTarget;

	private PlayMakerFSM fadeFSM;

	private Transform cameraParent;

	public List<CameraLockArea> lockZoneList;

	public float xLockMin;

	public float xLockMax;

	public float yLockMin;

	public float yLockMax;

	public void GameInit()
	{
		gm = GameManager.instance;
		cam = GetComponent<Camera>();
		cameraParent = base.transform.parent.transform;
		fadeFSM = FSMUtility.LocateFSM(base.gameObject, "CameraFade");
		ApplyEffectConfiguration(isGameplayLevel: false, isBloomForced: false);
		gm.UnloadingLevel += OnLevelUnload;
	}

	public void SceneInit()
	{
		startLockedTimer = 0.5f;
		velocity = Vector3.zero;
		bool isBloomForced = false;
		if (gm.IsGameplayScene())
		{
			isGameplayScene = true;
			if (hero_ctrl == null)
			{
				hero_ctrl = HeroController.instance;
				hero_ctrl.heroInPosition += PositionToHero;
			}
			lockZoneList = new List<CameraLockArea>();
			GetTilemapInfo();
			xLockMin = 0f;
			xLockMax = xLimit;
			yLockMin = 0f;
			yLockMax = yLimit;
			dampTimeX = dampTime;
			dampTimeY = dampTime;
			maxVelocityCurrent = maxVelocity;
			string currentMapZone = gm.GetCurrentMapZone();
			if (currentMapZone == MapZone.WHITE_PALACE.ToString() || currentMapZone == MapZone.GODS_GLORY.ToString())
			{
				isBloomForced = true;
			}
			string text = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
			if (text != null && text.StartsWith("Dream_Guardian_"))
			{
				isBloomForced = true;
			}
		}
		else
		{
			isGameplayScene = false;
			if (gm.IsMenuScene())
			{
				isBloomForced = true;
			}
		}
		ApplyEffectConfiguration(isGameplayScene, isBloomForced);
	}

	public void ApplyEffectConfiguration(bool isGameplayLevel, bool isBloomForced)
	{
		bool flag = Platform.Current.GraphicsTier > Platform.GraphicsTiers.Low;
		GetComponent<FastNoise>().enabled = isGameplayLevel && flag;
		GetComponent<BloomOptimized>().enabled = flag || isBloomForced;
		GetComponent<BrightnessEffect>().enabled = flag;
		GetComponent<ColorCorrectionCurves>().enabled = true;
	}

	private void LateUpdate()
	{
		float x = base.transform.position.x;
		float y = base.transform.position.y;
		float z = base.transform.position.z;
		float x2 = cameraParent.position.x;
		float y2 = cameraParent.position.y;
		if (isGameplayScene && mode != 0)
		{
			if (hero_ctrl.cState.lookingUp)
			{
				lookOffset = hero_ctrl.transform.position.y - camTarget.transform.position.y + 6f;
			}
			else if (hero_ctrl.cState.lookingDown)
			{
				lookOffset = hero_ctrl.transform.position.y - camTarget.transform.position.y - 6f;
			}
			else
			{
				lookOffset = 0f;
			}
			UpdateTargetDestinationDelta();
			Vector3 vector = cam.WorldToViewportPoint(camTarget.transform.position);
			Vector3 vector2 = new Vector3(targetDeltaX, targetDeltaY, 0f) - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, vector.z));
			destination = new Vector3(x + vector2.x, y + vector2.y, z);
			if (mode == CameraMode.LOCKED && currentLockArea != null)
			{
				if (lookOffset > 0f && currentLockArea.preventLookUp && destination.y > currentLockArea.cameraYMax)
				{
					if (base.transform.position.y > currentLockArea.cameraYMax)
					{
						destination = new Vector3(destination.x, destination.y - lookOffset, destination.z);
					}
					else
					{
						destination = new Vector3(destination.x, currentLockArea.cameraYMax, destination.z);
					}
				}
				if (lookOffset < 0f && currentLockArea.preventLookDown && destination.y < currentLockArea.cameraYMin)
				{
					if (base.transform.position.y < currentLockArea.cameraYMin)
					{
						destination = new Vector3(destination.x, destination.y - lookOffset, destination.z);
					}
					else
					{
						destination = new Vector3(destination.x, currentLockArea.cameraYMin, destination.z);
					}
				}
			}
			if (mode == CameraMode.FOLLOWING || mode == CameraMode.LOCKED)
			{
				destination = KeepWithinSceneBounds(destination);
			}
			Vector3 vector3 = Vector3.SmoothDamp(base.transform.position, new Vector3(destination.x, y, z), ref velocityX, dampTimeX);
			Extensions.SetPosition2D(y: Vector3.SmoothDamp(base.transform.position, new Vector3(x, destination.y, z), ref velocityY, dampTimeY).y, t: base.transform, x: vector3.x);
			x = base.transform.position.x;
			y = base.transform.position.y;
			if (velocity.magnitude > maxVelocityCurrent)
			{
				velocity = velocity.normalized * maxVelocityCurrent;
			}
		}
		if (isGameplayScene)
		{
			if (x + x2 < 14.6f)
			{
				base.transform.SetPositionX(14.6f);
			}
			if (base.transform.position.x + x2 > xLimit)
			{
				base.transform.SetPositionX(xLimit);
			}
			if (base.transform.position.y + y2 < 8.3f)
			{
				base.transform.SetPositionY(8.3f);
			}
			if (base.transform.position.y + y2 > yLimit)
			{
				base.transform.SetPositionY(yLimit);
			}
			if (startLockedTimer > 0f)
			{
				startLockedTimer -= Time.deltaTime;
			}
		}
	}

	private void OnDisable()
	{
		if (hero_ctrl != null)
		{
			hero_ctrl.heroInPosition -= PositionToHero;
		}
	}

	public void FreezeInPlace(bool freezeTargetAlso = false)
	{
		SetMode(CameraMode.FROZEN);
		if (freezeTargetAlso)
		{
			camTarget.FreezeInPlace();
		}
	}

	public void FadeOut(CameraFadeType type)
	{
		SetMode(CameraMode.FROZEN);
		switch (type)
		{
		case CameraFadeType.LEVEL_TRANSITION:
			fadeFSM.Fsm.Event("FADE OUT");
			break;
		case CameraFadeType.HERO_DEATH:
			fadeFSM.Fsm.Event("RESPAWN FADE");
			break;
		case CameraFadeType.HERO_HAZARD_DEATH:
			fadeFSM.Fsm.Event("HAZARD FADE");
			break;
		case CameraFadeType.JUST_FADE:
			fadeFSM.Fsm.Event("JUST FADE");
			break;
		case CameraFadeType.START_FADE:
			fadeFSM.Fsm.Event("START FADE");
			break;
		}
	}

	public void FadeSceneIn()
	{
		GameCameras.instance.cameraFadeFSM.Fsm.Event("FADE SCENE IN");
	}

	public void LockToArea(CameraLockArea lockArea)
	{
		if (lockZoneList.Contains(lockArea))
		{
			return;
		}
		if (verboseMode)
		{
			Debug.LogFormat("LockZone Activated: {0} at startLockedTimer {1} ({2}s)", lockArea.name, startLockedTimer, Time.timeSinceLevelLoad);
		}
		lockZoneList.Add(lockArea);
		if (!(currentLockArea != null) || !currentLockArea.maxPriority || lockArea.maxPriority)
		{
			currentLockArea = lockArea;
			SetMode(CameraMode.LOCKED);
			if (lockArea.cameraXMin < 0f)
			{
				xLockMin = 14.6f;
			}
			else
			{
				xLockMin = lockArea.cameraXMin;
			}
			if (lockArea.cameraXMax < 0f)
			{
				xLockMax = xLimit;
			}
			else
			{
				xLockMax = lockArea.cameraXMax;
			}
			if (lockArea.cameraYMin < 0f)
			{
				yLockMin = 8.3f;
			}
			else
			{
				yLockMin = lockArea.cameraYMin;
			}
			if (lockArea.cameraYMax < 0f)
			{
				yLockMax = yLimit;
			}
			else
			{
				yLockMax = lockArea.cameraYMax;
			}
			if (startLockedTimer > 0f)
			{
				camTarget.transform.SetPosition2D(KeepWithinLockBounds(hero_ctrl.transform.position));
				camTarget.destination = camTarget.transform.position;
				camTarget.EnterLockZoneInstant(xLockMin, xLockMax, yLockMin, yLockMax);
				base.transform.SetPosition2D(KeepWithinLockBounds(hero_ctrl.transform.position));
				destination = base.transform.position;
			}
			else
			{
				camTarget.EnterLockZone(xLockMin, xLockMax, yLockMin, yLockMax);
			}
		}
	}

	public void ReleaseLock(CameraLockArea lockarea)
	{
		lockZoneList.Remove(lockarea);
		if (verboseMode)
		{
			Debug.Log("LockZone Released " + lockarea.name);
		}
		if (lockarea == currentLockArea)
		{
			if (lockZoneList.Count > 0)
			{
				currentLockArea = lockZoneList[lockZoneList.Count - 1];
				xLockMin = currentLockArea.cameraXMin;
				xLockMax = currentLockArea.cameraXMax;
				yLockMin = currentLockArea.cameraYMin;
				yLockMax = currentLockArea.cameraYMax;
				camTarget.enteredFromLockZone = true;
				camTarget.EnterLockZone(xLockMin, xLockMax, yLockMin, yLockMax);
				return;
			}
			lastLockPosition = base.transform.position;
			if (camTarget != null)
			{
				camTarget.enteredFromLockZone = false;
				camTarget.ExitLockZone();
			}
			currentLockArea = null;
			if (!hero_ctrl.cState.hazardDeath && !hero_ctrl.cState.dead && gm.gameState != GameState.EXITING_LEVEL)
			{
				SetMode(CameraMode.FOLLOWING);
			}
		}
		else if (verboseMode)
		{
			Debug.Log("LockZone was not the current lock when removed.");
		}
	}

	public void ResetStartTimer()
	{
		startLockedTimer = 0.5f;
	}

	public void SnapTo(float x, float y)
	{
		camTarget.transform.position = new Vector3(x, y, camTarget.transform.position.z);
		base.transform.position = new Vector3(x, y, base.transform.position.z);
	}

	private void UpdateTargetDestinationDelta()
	{
		targetDeltaX = camTarget.transform.position.x + camTarget.xOffset + camTarget.dashOffset;
		targetDeltaY = camTarget.transform.position.y + camTarget.fallOffset + lookOffset;
	}

	public void SetMode(CameraMode newMode)
	{
		if (newMode != mode)
		{
			if (newMode == CameraMode.PREVIOUS)
			{
				mode = prevMode;
				return;
			}
			prevMode = mode;
			mode = newMode;
		}
	}

	public Vector3 KeepWithinSceneBounds(Vector3 targetDest)
	{
		Vector3 result = targetDest;
		bool flag = false;
		bool flag2 = false;
		if (result.x < 14.6f)
		{
			result = new Vector3(14.6f, result.y, result.z);
			flag = true;
			flag2 = true;
		}
		if (result.x > xLimit)
		{
			result = new Vector3(xLimit, result.y, result.z);
			flag = true;
			flag2 = true;
		}
		if (result.y < 8.3f)
		{
			result = new Vector3(result.x, 8.3f, result.z);
			flag = true;
		}
		if (result.y > yLimit)
		{
			result = new Vector3(result.x, yLimit, result.z);
			flag = true;
		}
		atSceneBounds = flag;
		atHorizontalSceneBounds = flag2;
		return result;
	}

	private Vector2 KeepWithinSceneBounds(Vector2 targetDest)
	{
		bool flag = false;
		if (targetDest.x < 14.6f)
		{
			targetDest = new Vector2(14.6f, targetDest.y);
			flag = true;
		}
		if (targetDest.x > xLimit)
		{
			targetDest = new Vector2(xLimit, targetDest.y);
			flag = true;
		}
		if (targetDest.y < 8.3f)
		{
			targetDest = new Vector2(targetDest.x, 8.3f);
			flag = true;
		}
		if (targetDest.y > yLimit)
		{
			targetDest = new Vector2(targetDest.x, yLimit);
			flag = true;
		}
		atSceneBounds = flag;
		return targetDest;
	}

	private bool IsAtSceneBounds(Vector2 targetDest)
	{
		bool result = false;
		if (targetDest.x <= 14.6f)
		{
			result = true;
		}
		if (targetDest.x >= xLimit)
		{
			result = true;
		}
		if (targetDest.y <= 8.3f)
		{
			result = true;
		}
		if (targetDest.y >= yLimit)
		{
			result = true;
		}
		return result;
	}

	private bool IsAtHorizontalSceneBounds(Vector2 targetDest, out bool leftSide)
	{
		bool result = false;
		leftSide = false;
		if (targetDest.x <= 14.6f)
		{
			result = true;
			leftSide = true;
		}
		if (targetDest.x >= xLimit)
		{
			result = true;
			leftSide = false;
		}
		return result;
	}

	private bool IsTouchingSides(float x)
	{
		bool result = false;
		if (x <= 14.6f)
		{
			result = true;
		}
		if (x >= xLimit)
		{
			result = true;
		}
		return result;
	}

	public Vector2 KeepWithinLockBounds(Vector2 targetDest)
	{
		float x = targetDest.x;
		float y = targetDest.y;
		if (x < xLockMin)
		{
			x = xLockMin;
		}
		if (x > xLockMax)
		{
			x = xLockMax;
		}
		if (y < yLockMin)
		{
			y = yLockMin;
		}
		if (y > yLockMax)
		{
			y = yLockMax;
		}
		return new Vector2(x, y);
	}

	private void GetTilemapInfo()
	{
		tilemap = gm.tilemap;
		sceneWidth = tilemap.width;
		sceneHeight = tilemap.height;
		xLimit = sceneWidth - 14.6f;
		yLimit = sceneHeight - 8.3f;
	}

	public void PositionToHero(bool forceDirect)
	{
		StartCoroutine(DoPositionToHero(forceDirect));
	}

	private IEnumerator DoPositionToHero(bool forceDirect)
	{
		yield return new WaitForFixedUpdate();
		GetTilemapInfo();
		camTarget.PositionToStart();
		UpdateTargetDestinationDelta();
		CameraMode previousMode = mode;
		SetMode(CameraMode.FROZEN);
		teleporting = true;
		Vector3 newPosition = KeepWithinSceneBounds(camTarget.transform.position);
		if (verboseMode)
		{
			Debug.LogFormat("CC - STR: NewPosition: {0} TargetDelta: ({1}, {2}) CT-XOffset: {3} HeroPos: {4} CT-Pos: {5}", newPosition, targetDeltaX, targetDeltaY, camTarget.xOffset, hero_ctrl.transform.position, camTarget.transform.position);
		}
		if (forceDirect)
		{
			if (verboseMode)
			{
				Debug.Log("====> TEST 1a - ForceDirect Positioning Mode");
			}
			transform.SetPosition2D(newPosition);
		}
		else
		{
			if (verboseMode)
			{
				Debug.Log("====> TEST 1b - Normal Positioning Mode");
			}
			bool leftSide;
			bool num = IsAtHorizontalSceneBounds(newPosition, out leftSide);
			bool flag = false;
			if (currentLockArea != null)
			{
				flag = true;
			}
			if (flag)
			{
				if (verboseMode)
				{
					Debug.Log("====> TEST 3 - Lock Zone Active");
				}
				PositionToHeroFacing(newPosition, useXOffset: true);
				transform.SetPosition2D(KeepWithinLockBounds(transform.position));
			}
			else
			{
				if (verboseMode)
				{
					Debug.Log("====> TEST 4 - No Lock Zone");
				}
				PositionToHeroFacing(newPosition, useXOffset: false);
			}
			if (num)
			{
				if (verboseMode)
				{
					Debug.Log("====> TEST 2 - At Horizontal Scene Bounds");
				}
				if ((leftSide && !hero_ctrl.cState.facingRight) || (!leftSide && hero_ctrl.cState.facingRight))
				{
					if (verboseMode)
					{
						Debug.Log("====> TEST 2a - Hero Facing Bounds");
					}
					transform.SetPosition2D(newPosition);
				}
				else
				{
					if (verboseMode)
					{
						Debug.Log("====> TEST 2b - Hero Facing Inwards");
					}
					if (IsTouchingSides(targetDeltaX))
					{
						if (verboseMode)
						{
							Debug.Log("Xoffset still touching sides");
						}
						transform.SetPosition2D(newPosition);
					}
					else
					{
						if (verboseMode)
						{
							Debug.LogFormat("Not Touching Sides with Xoffset CT: {0} Hero: {1}", camTarget.transform.position, hero_ctrl.transform.position);
						}
						if (hero_ctrl.cState.facingRight)
						{
							transform.SetPosition2D(hero_ctrl.transform.position.x + 1f, newPosition.y);
						}
						else
						{
							transform.SetPosition2D(hero_ctrl.transform.position.x - 1f, newPosition.y);
						}
					}
				}
			}
		}
		destination = transform.position;
		velocity = Vector3.zero;
		velocityX = Vector3.zero;
		velocityY = Vector3.zero;
		yield return new WaitForSeconds(0.1f);
		GameCameras.instance.cameraFadeFSM.Fsm.Event("LEVEL LOADED");
		teleporting = false;
		switch (previousMode)
		{
		case CameraMode.FROZEN:
			SetMode(CameraMode.FOLLOWING);
			break;
		case CameraMode.LOCKED:
			if (currentLockArea != null)
			{
				SetMode(previousMode);
			}
			else
			{
				SetMode(CameraMode.FOLLOWING);
			}
			break;
		default:
			SetMode(previousMode);
			break;
		}
		if (verboseMode)
		{
			Debug.LogFormat("CC - PositionToHero FIN: - TargetDelta: ({0}, {1}) Destination: {2} CT-XOffset: {3} NewPosition: {4} CamTargetPos: {5} HeroPos: {6}", targetDeltaX, targetDeltaY, destination, camTarget.xOffset, newPosition, camTarget.transform.position, hero_ctrl.transform.position);
		}
	}

	private void PositionToHeroFacing()
	{
		if (hero_ctrl.cState.facingRight)
		{
			base.transform.SetPosition2D(camTarget.transform.position.x + 1f, camTarget.transform.position.y);
		}
		else
		{
			base.transform.SetPosition2D(camTarget.transform.position.x - 1f, camTarget.transform.position.y);
		}
	}

	private void PositionToHeroFacing(Vector2 newPosition, bool useXOffset)
	{
		if (useXOffset)
		{
			base.transform.SetPosition2D(newPosition.x + camTarget.xOffset, newPosition.y);
		}
		else if (hero_ctrl.cState.facingRight)
		{
			base.transform.SetPosition2D(newPosition.x + 1f, newPosition.y);
		}
		else
		{
			base.transform.SetPosition2D(newPosition.x - 1f, newPosition.y);
		}
	}

	private Vector2 GetPositionToHeroFacing(Vector2 newPosition, bool useXOffset)
	{
		if (useXOffset)
		{
			return new Vector2(newPosition.x + camTarget.xOffset, newPosition.y);
		}
		if (hero_ctrl.cState.facingRight)
		{
			return new Vector2(newPosition.x + 1f, newPosition.y);
		}
		return new Vector2(newPosition.x - 1f, newPosition.y);
	}

	private IEnumerator FadeInFailSafe()
	{
		yield return new WaitForSeconds(5f);
		if (fadeFSM.ActiveStateName != "Normal" && fadeFSM.ActiveStateName != "FadingOut")
		{
			Debug.LogFormat("Failsafe fade in activated. State: {0} Scene: {1}", fadeFSM.ActiveStateName, gm.sceneName);
			fadeFSM.Fsm.Event("FADE SCENE IN");
		}
	}

	private void StopFailSafe()
	{
		if (fadeInFailSafeCo != null)
		{
			StopCoroutine(fadeInFailSafeCo);
		}
	}

	private void OnLevelUnload()
	{
		if (verboseMode)
		{
			Debug.Log("Removing cam locks. (" + lockZoneList.Count + " total)");
		}
		while (lockZoneList.Count > 0)
		{
			ReleaseLock(lockZoneList[0]);
		}
	}

	private void OnDestroy()
	{
		if (gm != null)
		{
			gm.UnloadingLevel -= OnLevelUnload;
		}
	}
}
