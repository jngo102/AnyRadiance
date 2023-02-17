using System.Collections;
using GlobalEnums;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraLockArea : MonoBehaviour
{
	private bool verboseMode;

	public float cameraXMin;

	public float cameraYMin;

	public float cameraXMax;

	public float cameraYMax;

	private float leftSideX;

	private float rightSideX;

	private float topSideY;

	private float botSideY;

	private Vector3 heroPos;

	private bool enteredLeft;

	private bool enteredRight;

	private bool enteredTop;

	private bool enteredBot;

	private bool exitedLeft;

	private bool exitedRight;

	private bool exitedTop;

	private bool exitedBot;

	public bool preventLookUp;

	public bool preventLookDown;

	public bool maxPriority;

	private GameCameras gcams;

	private CameraController cameraCtrl;

	private CameraTarget camTarget;

	private Collider2D box2d;

	private void Awake()
	{
		box2d = GetComponent<Collider2D>();
	}

	private IEnumerator Start()
	{
		gcams = GameCameras.instance;
		if (gcams == null)
		{
			yield break;
		}
		cameraCtrl = gcams.cameraController;
		camTarget = gcams.cameraTarget;
		Scene scene = gameObject.scene;
		if (!(cameraCtrl == null))
		{
			while (cameraCtrl.tilemap == null || cameraCtrl.tilemap.gameObject.scene != scene)
			{
				yield return null;
			}
			if (!ValidateBounds())
			{
				Debug.LogError("Camera bounds are unspecified for " + name + ", please specify lock area bounds for this Camera Lock Area.");
			}
			if (box2d != null)
			{
				leftSideX = box2d.bounds.min.x;
				rightSideX = box2d.bounds.max.x;
				botSideY = box2d.bounds.min.y;
				topSideY = box2d.bounds.max.y;
			}
		}
	}

	private bool IsInApplicableGameState()
	{
		GameManager unsafeInstance = GameManager.UnsafeInstance;
		if (unsafeInstance == null)
		{
			return false;
		}
		if (unsafeInstance.gameState != GameState.PLAYING)
		{
			return unsafeInstance.gameState == GameState.ENTERING_LEVEL;
		}
		return true;
	}

	public void OnTriggerEnter2D(Collider2D otherCollider)
	{
		if (!IsInApplicableGameState() || !otherCollider.CompareTag("Player"))
		{
			return;
		}
		heroPos = otherCollider.gameObject.transform.position;
		if (box2d != null)
		{
			if (heroPos.x > leftSideX - 1f && heroPos.x < leftSideX + 1f)
			{
				camTarget.enteredLeft = true;
			}
			else
			{
				camTarget.enteredLeft = false;
			}
			if (heroPos.x > rightSideX - 1f && heroPos.x < rightSideX + 1f)
			{
				camTarget.enteredRight = true;
			}
			else
			{
				camTarget.enteredRight = false;
			}
			if (heroPos.y > topSideY - 2f && heroPos.y < topSideY + 2f)
			{
				camTarget.enteredTop = true;
			}
			else
			{
				camTarget.enteredTop = false;
			}
			if (heroPos.y > botSideY - 1f && heroPos.y < botSideY + 1f)
			{
				camTarget.enteredBot = true;
			}
			else
			{
				camTarget.enteredBot = false;
			}
		}
		cameraCtrl.LockToArea(this);
		if (verboseMode)
		{
			Debug.Log("Lockzone Enter Lock " + base.name);
		}
	}

	public void OnTriggerStay2D(Collider2D otherCollider)
	{
		if (!base.isActiveAndEnabled || !box2d.isActiveAndEnabled)
		{
			Debug.LogWarning("Fix for Unity trigger event queue!");
		}
		else if (IsInApplicableGameState() && otherCollider.CompareTag("Player"))
		{
			if (verboseMode)
			{
				Debug.Log("Lockzone Stay Lock " + base.name);
			}
			cameraCtrl.LockToArea(this);
		}
	}

	public void OnTriggerExit2D(Collider2D otherCollider)
	{
		if (!otherCollider.CompareTag("Player"))
		{
			return;
		}
		heroPos = otherCollider.gameObject.transform.position;
		if (box2d != null)
		{
			if (heroPos.x > leftSideX - 1f && heroPos.x < leftSideX + 1f)
			{
				camTarget.exitedLeft = true;
			}
			else
			{
				camTarget.exitedLeft = false;
			}
			if (heroPos.x > rightSideX - 1f && heroPos.x < rightSideX + 1f)
			{
				camTarget.exitedRight = true;
			}
			else
			{
				camTarget.exitedRight = false;
			}
			if (heroPos.y > topSideY - 2f && heroPos.y < topSideY + 2f)
			{
				camTarget.exitedTop = true;
			}
			else
			{
				camTarget.exitedTop = false;
			}
			if (heroPos.y > botSideY - 1f && heroPos.y < botSideY + 1f)
			{
				camTarget.exitedBot = true;
			}
			else
			{
				camTarget.exitedBot = false;
			}
		}
		cameraCtrl.ReleaseLock(this);
		if (verboseMode)
		{
			Debug.Log("Lockzone Exit Lock " + base.name);
		}
	}

	public void OnDisable()
	{
		if (cameraCtrl != null)
		{
			cameraCtrl.ReleaseLock(this);
		}
	}

	private bool ValidateBounds()
	{
		if (cameraXMin == -1f)
		{
			cameraXMin = 14.6f;
		}
		if (cameraXMax == -1f)
		{
			cameraXMax = cameraCtrl.xLimit;
		}
		if (cameraYMin == -1f)
		{
			cameraYMin = 8.3f;
		}
		if (cameraYMax == -1f)
		{
			cameraYMax = cameraCtrl.yLimit;
		}
		if (cameraXMin == 0f && cameraXMax == 0f && cameraYMin == 0f && cameraYMax == 0f)
		{
			return false;
		}
		return true;
	}

	public void SetXMin(float xmin)
	{
		cameraXMin = xmin;
	}

	public void SetXMax(float xmax)
	{
		cameraXMax = xmax;
	}

	private IEnumerator orig_Start()
	{
		gcams = GameCameras.instance;
		cameraCtrl = gcams.cameraController;
		camTarget = gcams.cameraTarget;
		Scene scene = gameObject.scene;
		while (cameraCtrl.tilemap == null || cameraCtrl.tilemap.gameObject.scene != scene)
		{
			yield return null;
		}
		if (!ValidateBounds())
		{
			Debug.LogError("Camera bounds are unspecified for " + name + ", please specify lock area bounds for this Camera Lock Area.");
		}
		if (box2d != null)
		{
			leftSideX = box2d.bounds.min.x;
			rightSideX = box2d.bounds.max.x;
			botSideY = box2d.bounds.min.y;
			topSideY = box2d.bounds.max.y;
		}
	}
}
