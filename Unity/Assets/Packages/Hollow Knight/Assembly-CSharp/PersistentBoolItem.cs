using UnityEngine;

public class PersistentBoolItem : MonoBehaviour
{
	public delegate void BoolEvent(bool value);

	public delegate void BoolRefEvent(ref bool value);

	[SerializeField]
	public bool semiPersistent;

	[SerializeField]
	public PersistentBoolData persistentBoolData;

	private GameManager gm;

	private PlayMakerFSM myFSM;

	private bool started;

	public event BoolEvent OnSetSaveState;

	public event BoolRefEvent OnGetSaveState;

	private void Awake()
	{
		persistentBoolData.semiPersistent = semiPersistent;
	}

	private void OnEnable()
	{
		gm = GameManager.instance;
		gm.SavePersistentObjects += SaveState;
		if (semiPersistent)
		{
			gm.ResetSemiPersistentObjects += ResetState;
		}
		if (this.OnGetSaveState == null)
		{
			LookForMyFSM();
		}
	}

	private void OnDisable()
	{
		if (gm != null)
		{
			gm.SavePersistentObjects -= SaveState;
			gm.ResetSemiPersistentObjects -= ResetState;
		}
	}

	private void Start()
	{
		if (started)
		{
			return;
		}
		SetMyID();
		PersistentBoolData persistentBoolData = SceneData.instance.FindMyState(this.persistentBoolData);
		if (persistentBoolData != null)
		{
			this.persistentBoolData.activated = persistentBoolData.activated;
			if (this.OnSetSaveState != null)
			{
				this.OnSetSaveState(persistentBoolData.activated);
			}
			else if (myFSM != null)
			{
				myFSM.FsmVariables.FindFsmBool("Activated").Value = persistentBoolData.activated;
			}
			else
			{
				LookForMyFSM();
			}
		}
		else if (this.OnGetSaveState != null)
		{
			this.OnGetSaveState(ref this.persistentBoolData.activated);
		}
		else
		{
			UpdateActivatedFromFSM();
		}
	}

	public void SaveState()
	{
		SetMyID();
		if (this.OnGetSaveState != null)
		{
			this.OnGetSaveState(ref persistentBoolData.activated);
		}
		else
		{
			UpdateActivatedFromFSM();
		}
		SceneData.instance.SaveMyState(persistentBoolData);
	}

	private void ResetState()
	{
		if (semiPersistent)
		{
			persistentBoolData.activated = false;
			if (myFSM != null)
			{
				myFSM.SendEvent("RESET");
			}
			else
			{
				Debug.LogWarning("Persistent Bool Item - Couldn't reset value on FSM because it's missing.");
			}
		}
	}

	private void SetMyID()
	{
		if (string.IsNullOrEmpty(persistentBoolData.id))
		{
			persistentBoolData.id = base.name;
		}
		if (string.IsNullOrEmpty(persistentBoolData.sceneName))
		{
			persistentBoolData.sceneName = GameManager.GetBaseSceneName(base.gameObject.scene.name);
		}
	}

	public void PreSetup()
	{
		Start();
	}

	private void UpdateActivatedFromFSM()
	{
		if (myFSM != null)
		{
			persistentBoolData.activated = myFSM.FsmVariables.FindFsmBool("Activated").Value;
		}
		else
		{
			LookForMyFSM();
		}
	}

	private void LookForMyFSM()
	{
		PlayMakerFSM[] components = GetComponents<PlayMakerFSM>();
		if (components == null)
		{
			Debug.LogErrorFormat("Persistent Bool Item ({0}) does not have a PlayMakerFSM attached to read state from.", base.name);
			return;
		}
		myFSM = FSMUtility.FindFSMWithPersistentBool(components);
		if (myFSM == null)
		{
			Debug.LogErrorFormat("Persistent Bool Item ({0}) does not have a PlayMakerFSM attached to read state from.", base.name);
		}
	}
}
