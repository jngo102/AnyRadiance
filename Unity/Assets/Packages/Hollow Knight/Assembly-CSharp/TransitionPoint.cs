using System.Collections.Generic;
using GlobalEnums;
using UnityEngine;
using UnityEngine.Audio;

public class TransitionPoint : MonoBehaviour
{
	public delegate void BeforeTransitionEvent();

	private GameManager gm;

	private PlayerData playerData;

	private bool activated;

	[Header("Door Type Gate Settings")]
	[Space(5f)]
	public bool isADoor;

	public bool dontWalkOutOfDoor;

	[Header("Gate Entry")]
	[Tooltip("The wait time before entering from this gate (not the target gate).")]
	public float entryDelay;

	public bool alwaysEnterRight;

	public bool alwaysEnterLeft;

	[Header("Force Hard Land (Top Gates Only)")]
	[Space(5f)]
	public bool hardLandOnExit;

	[Header("Destination Scene")]
	[Space(5f)]
	public string targetScene;

	public string entryPoint;

	public Vector2 entryOffset;

	[SerializeField]
	private bool alwaysUnloadUnusedAssets;

	public PlayMakerFSM customFadeFSM;

	[Header("Hazard Respawn")]
	[Space(5f)]
	public bool nonHazardGate;

	public HazardRespawnMarker respawnMarker;

	[Header("Set Audio Snapshots")]
	[Space(5f)]
	public AudioMixerSnapshot atmosSnapshot;

	public AudioMixerSnapshot enviroSnapshot;

	public AudioMixerSnapshot actorSnapshot;

	public AudioMixerSnapshot musicSnapshot;

	private Color myGreen = new Color(0f, 0.8f, 0f, 0.5f);

	[Header("Cosmetics")]
	public GameManager.SceneLoadVisualizations sceneLoadVisualization;

	public bool customFade;

	public bool forceWaitFetch;

	private static List<TransitionPoint> transitionPoints;

	public static string lastEntered = "";

	public static List<TransitionPoint> TransitionPoints => transitionPoints;

	public event BeforeTransitionEvent OnBeforeTransition;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void Init()
	{
		transitionPoints = new List<TransitionPoint>();
	}

	protected void Awake()
	{
		transitionPoints.Add(this);
	}

	protected void OnDestroy()
	{
		transitionPoints.Remove(this);
	}

	private void Start()
	{
		gm = GameManager.instance;
		playerData = PlayerData.instance;
		if (!nonHazardGate && respawnMarker == null)
		{
			Debug.LogError("Transition Gate " + base.name + " in " + gm.sceneName + " does not have its respawn marker set in inspector.");
		}
	}

	private void OnTriggerEnter2D(Collider2D movingObj)
	{
		if (isADoor || movingObj.gameObject.layer != 9 || gm.gameState != GameState.PLAYING)
		{
			return;
		}
		if (!string.IsNullOrEmpty(targetScene) && !string.IsNullOrEmpty(entryPoint))
		{
			if ((bool)customFadeFSM)
			{
				customFadeFSM.SendEvent("FADE");
			}
			if (atmosSnapshot != null)
			{
				atmosSnapshot.TransitionTo(1.5f);
			}
			if (enviroSnapshot != null)
			{
				enviroSnapshot.TransitionTo(1.5f);
			}
			if (actorSnapshot != null)
			{
				actorSnapshot.TransitionTo(1.5f);
			}
			if (musicSnapshot != null)
			{
				musicSnapshot.TransitionTo(1.5f);
			}
			activated = true;
			lastEntered = base.gameObject.name;
			if (this.OnBeforeTransition != null)
			{
				this.OnBeforeTransition();
			}
			gm.BeginSceneTransition(new GameManager.SceneLoadInfo
			{
				SceneName = targetScene,
				EntryGateName = entryPoint,
				HeroLeaveDirection = GetGatePosition(),
				EntryDelay = entryDelay,
				WaitForSceneTransitionCameraFade = true,
				PreventCameraFadeOut = (customFadeFSM != null),
				Visualization = sceneLoadVisualization,
				AlwaysUnloadUnusedAssets = alwaysUnloadUnusedAssets,
				forceWaitFetch = forceWaitFetch
			});
		}
		else
		{
			Debug.LogError(gm.sceneName + " " + base.name + " no target scene has been set on this gate.");
		}
	}

	private void OnTriggerStay2D(Collider2D movingObj)
	{
		if (!activated)
		{
			OnTriggerEnter2D(movingObj);
		}
	}

	private void OnDrawGizmos()
	{
		if (base.transform != null)
		{
			Vector3 position = (Vector3)(Vector2)base.transform.position + new Vector3(0f, GetComponent<BoxCollider2D>().bounds.extents.y + 1.5f, 0f);
			GizmoUtility.DrawText(GUI.skin, targetScene, position, myGreen, 10);
		}
	}

	public GatePosition GetGatePosition()
	{
		string text = base.name;
		if (text.Contains("top"))
		{
			return GatePosition.top;
		}
		if (text.Contains("right"))
		{
			return GatePosition.right;
		}
		if (text.Contains("left"))
		{
			return GatePosition.left;
		}
		if (text.Contains("bot"))
		{
			return GatePosition.bottom;
		}
		if (text.Contains("door") || isADoor)
		{
			return GatePosition.door;
		}
		Debug.LogError("Gate name " + text + "does not conform to a valid gate position type. Make sure gate name has the form 'left1'");
		return GatePosition.unknown;
	}

	public void SetTargetScene(string newScene)
	{
		targetScene = newScene;
	}
}
