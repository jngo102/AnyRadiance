using UnityEngine;
using UnityEngine.Events;

public class NextSceneOnInput : MonoBehaviour
{
	private enum NextSceneType
	{
		GGReturn
	}

	[SerializeField]
	private UnityEvent onBeforeSave;

	[SerializeField]
	private bool doSaveOnStart = true;

	[Space]
	[SerializeField]
	private bool acceptingInput;

	[SerializeField]
	private float maxInputBlockDelay = 10f;

	private float inputAcceptTime;

	private bool hasSkipped;

	[Space]
	[SerializeField]
	private NextSceneType nextSceneType;

	public bool AcceptingInput
	{
		get
		{
			if (!acceptingInput)
			{
				return Time.time >= inputAcceptTime;
			}
			return true;
		}
	}

	private bool ButtonPressed
	{
		get
		{
			if (!InputHandler.Instance || !InputHandler.Instance.inputActions.ActiveDevice.AnyButtonIsPressed)
			{
				return Input.anyKeyDown;
			}
			return true;
		}
	}

	private void Start()
	{
		inputAcceptTime = Time.time + maxInputBlockDelay;
		onBeforeSave.Invoke();
		if (doSaveOnStart)
		{
			GameManager.instance.SaveGame();
		}
	}

	public void UnlockSkip()
	{
		acceptingInput = true;
	}

	private void Update()
	{
		if (!hasSkipped && AcceptingInput && ButtonPressed)
		{
			Skip();
			hasSkipped = true;
		}
	}

	private void Skip()
	{
		if (nextSceneType == NextSceneType.GGReturn)
		{
			GameManager.instance.BeginSceneTransition(new GameManager.SceneLoadInfo
			{
				SceneName = "GG_Atrium",
				EntryGateName = GameManager.instance.playerData.GetString("bossReturnEntryGate"),
				EntryDelay = 0f,
				Visualization = GameManager.SceneLoadVisualizations.GodsAndGlory,
				PreventCameraFadeOut = false,
				WaitForSceneTransitionCameraFade = true
			});
		}
		else
		{
			Debug.LogError("Next Scene Type \"{0}\" not implemented!");
		}
	}
}
