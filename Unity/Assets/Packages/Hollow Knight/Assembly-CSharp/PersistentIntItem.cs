using UnityEngine;

public class PersistentIntItem : MonoBehaviour
{
	[SerializeField]
	[Tooltip("If checked, this object will reset its state under certain circumstances such as hero death.")]
	public bool semiPersistent;

	[SerializeField]
	public PersistentIntData persistentIntData;

	private GameManager gm;

	private PlayMakerFSM myFSM;

	private void Awake()
	{
		persistentIntData.semiPersistent = semiPersistent;
	}

	private void OnEnable()
	{
		gm = GameManager.instance;
		gm.SavePersistentObjects += SaveState;
		if (semiPersistent)
		{
			gm.ResetSemiPersistentObjects += ResetState;
		}
		LookForMyFSM();
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
		SetMyID();
		PersistentIntData persistentIntData = SceneData.instance.FindMyState(this.persistentIntData);
		if (persistentIntData != null)
		{
			this.persistentIntData.value = persistentIntData.value;
			if (myFSM != null)
			{
				myFSM.FsmVariables.GetFsmInt("Value").Value = persistentIntData.value;
			}
			else
			{
				LookForMyFSM();
			}
		}
		else
		{
			UpdateValueFromFSM();
		}
	}

	private void SaveState()
	{
		SetMyID();
		UpdateValueFromFSM();
		SceneData.instance.SaveMyState(persistentIntData);
	}

	private void ResetState()
	{
		if (semiPersistent)
		{
			persistentIntData.value = -1;
			if (myFSM != null)
			{
				myFSM.SendEvent("RESET");
			}
			else
			{
				Debug.LogError("Persistent Bool Item - Couldn't reset value on FSM because it's missing.");
			}
		}
	}

	private void SetMyID()
	{
		if (string.IsNullOrEmpty(persistentIntData.id))
		{
			persistentIntData.id = base.name;
		}
		if (string.IsNullOrEmpty(persistentIntData.sceneName))
		{
			persistentIntData.sceneName = GameManager.GetBaseSceneName(base.gameObject.scene.name);
		}
	}

	private void UpdateValueFromFSM()
	{
		if (myFSM != null)
		{
			persistentIntData.value = myFSM.FsmVariables.GetFsmInt("Value").Value;
		}
		else
		{
			LookForMyFSM();
		}
	}

	private void SetValueOnFSM(int newValue)
	{
		if (myFSM != null)
		{
			myFSM.FsmVariables.GetFsmInt("Value").Value = newValue;
		}
	}

	private void LookForMyFSM()
	{
		PlayMakerFSM[] components = GetComponents<PlayMakerFSM>();
		if (components == null)
		{
			Debug.LogErrorFormat("Persistent Int Item ({0}) does not have a PlayMakerFSM attached to read state from.", base.name);
			return;
		}
		myFSM = FSMUtility.FindFSMWithPersistentInt(components);
		if (myFSM == null)
		{
			Debug.LogErrorFormat("Persistent Int Item ({0}) does not have a PlayMakerFSM attached to read state from.", base.name);
		}
	}
}
