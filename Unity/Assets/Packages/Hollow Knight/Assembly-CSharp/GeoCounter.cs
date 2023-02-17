using UnityEngine;

public class GeoCounter : MonoBehaviour
{
	private const float ROLLER_START_PAUSE = 1.5f;

	private const float DIGIT_CHANGE_TIME = 0.025f;

	[HideInInspector]
	public PlayerData playerData;

	public GameObject geoSprite;

	public TextMesh geoTextMesh;

	public TextMesh subTextMesh;

	public TextMesh addTextMesh;

	private PlayMakerFSM geoSpriteFsm;

	private PlayMakerFSM subTextFsm;

	private PlayMakerFSM addTextFsm;

	private int counterCurrent;

	private int geoChange;

	private int addCounter;

	private int takeCounter;

	private int addRollerState;

	private int takeRollerState;

	private int changePerTick;

	private float addRollerStartTimer;

	private float takeRollerStartTimer;

	private float digitChangeTimer;

	private bool toZero;

	private void Awake()
	{
		geoSpriteFsm = FSMUtility.GetFSM(geoSprite);
		subTextFsm = FSMUtility.GetFSM(subTextMesh.gameObject);
		addTextFsm = FSMUtility.GetFSM(addTextMesh.gameObject);
	}

	private void Start()
	{
		playerData = PlayerData.instance;
		counterCurrent = playerData.GetInt("geo");
		geoTextMesh.text = counterCurrent.ToString();
	}

	public void UpdateGeo()
	{
	}

	public void NewSceneRefresh()
	{
		counterCurrent = playerData.GetInt("geo");
		geoTextMesh.text = counterCurrent.ToString();
		toZero = false;
		takeRollerState = 0;
		addRollerState = 0;
	}

	public void AddGeo(int geo)
	{
		geoSpriteFsm.SendEvent("CHECK FIRST");
		if (takeRollerState > 0)
		{
			geoChange = geo;
			addCounter = geoChange;
			takeRollerState = 0;
			geoSpriteFsm.SendEvent("IDLE");
			subTextFsm.SendEvent("DOWN");
			counterCurrent = playerData.GetInt("geo") + -addCounter;
			geoTextMesh.text = counterCurrent.ToString();
		}
		if (addRollerState == 0)
		{
			geoChange = geo;
			addCounter = geoChange;
			addTextFsm.SendEvent("UP");
			addTextMesh.text = "+ " + addCounter;
			addRollerStartTimer = 1.5f;
			addRollerState = 1;
		}
		else if (addRollerState == 1)
		{
			geoChange = geo;
			addCounter += geoChange;
			addTextMesh.text = "+ " + addCounter;
			addRollerStartTimer = 1.5f;
		}
		else if (addRollerState == 2)
		{
			geoChange = geo;
			addCounter = geoChange;
			geoSpriteFsm.SendEvent("IDLE");
			counterCurrent = playerData.GetInt("geo");
			geoTextMesh.text = counterCurrent.ToString();
			addTextMesh.text = "+ " + addCounter;
			addRollerState = 1;
			addRollerStartTimer = 1.5f;
		}
		changePerTick = (int)((double)((float)addCounter * 0.025f) * 1.75);
		if (changePerTick < 1)
		{
			changePerTick = 1;
		}
	}

	public void TakeGeo(int geo)
	{
		if (addRollerState > 0)
		{
			geoChange = -geo;
			takeCounter = geoChange;
			addRollerState = 0;
			geoSpriteFsm.SendEvent("IDLE");
			addTextFsm.SendEvent("DOWN");
			counterCurrent = playerData.GetInt("geo") + -takeCounter;
			geoTextMesh.text = counterCurrent.ToString();
		}
		if (takeRollerState == 0)
		{
			geoChange = -geo;
			takeCounter = geoChange;
			subTextFsm.SendEvent("UP");
			subTextMesh.text = "- " + -takeCounter;
			takeRollerStartTimer = 1.5f;
			takeRollerState = 1;
		}
		else if (takeRollerState == 1)
		{
			geoChange = -geo;
			takeCounter += geoChange;
			subTextMesh.text = "- " + -takeCounter;
			takeRollerStartTimer = 1.5f;
		}
		else if (takeRollerState == 2)
		{
			geoChange = -geo;
			takeCounter = geoChange;
			geoSpriteFsm.SendEvent("IDLE");
			counterCurrent = playerData.GetInt("geo");
			geoTextMesh.text = counterCurrent.ToString();
			subTextMesh.text = "- " + -takeCounter;
			takeRollerState = 1;
			takeRollerStartTimer = 1.5f;
		}
		changePerTick = (int)((double)((float)takeCounter * 0.025f) * 1.75);
		if (changePerTick > -1)
		{
			changePerTick = -1;
		}
	}

	public void ToZero()
	{
		if (counterCurrent == 0)
		{
			geoSpriteFsm.SendEvent("SHATTER");
			return;
		}
		changePerTick = -(int)((float)counterCurrent * 0.025f * 1.75f);
		if (changePerTick > -1)
		{
			changePerTick = -1;
		}
		geoSpriteFsm.SendEvent("TO ZERO");
		toZero = true;
	}

	private void Update()
	{
		if (toZero)
		{
			if (digitChangeTimer < 0f)
			{
				if (counterCurrent > 0)
				{
					counterCurrent += changePerTick;
					if (counterCurrent <= 0)
					{
						counterCurrent = 0;
						geoSpriteFsm.SendEvent("SHATTER");
						toZero = false;
					}
					geoTextMesh.text = counterCurrent.ToString();
					digitChangeTimer += 0.025f;
				}
			}
			else
			{
				digitChangeTimer -= Time.deltaTime;
			}
			return;
		}
		if (addRollerState == 1)
		{
			if (addRollerStartTimer > 0f)
			{
				addRollerStartTimer -= Time.deltaTime;
			}
			else
			{
				addRollerState = 2;
			}
		}
		if (addRollerState == 2 && addCounter > 0)
		{
			geoSpriteFsm.SendEvent("GET");
			if (digitChangeTimer < 0f)
			{
				addCounter -= changePerTick;
				counterCurrent += changePerTick;
				geoTextMesh.text = counterCurrent.ToString();
				if (addTextMesh != null)
				{
					addTextMesh.text = "+ " + addCounter;
				}
				if (addCounter <= 0)
				{
					geoSpriteFsm.SendEvent("IDLE");
					addCounter = 0;
					addTextMesh.text = "+ 0";
					addRollerState = 0;
					counterCurrent = playerData.GetInt("geo");
					geoTextMesh.text = counterCurrent.ToString();
					addTextFsm.SendEvent("DOWN");
				}
				digitChangeTimer += 0.025f;
			}
			else
			{
				digitChangeTimer -= Time.deltaTime;
			}
		}
		if (takeRollerState == 1)
		{
			if (takeRollerStartTimer > 0f)
			{
				takeRollerStartTimer -= Time.deltaTime;
			}
			else
			{
				takeRollerState = 2;
			}
		}
		if (takeRollerState != 2 || takeCounter >= 0)
		{
			return;
		}
		geoSpriteFsm.SendEvent("TAKE");
		if (digitChangeTimer < 0f)
		{
			takeCounter -= changePerTick;
			counterCurrent += changePerTick;
			geoTextMesh.text = counterCurrent.ToString();
			if (subTextMesh != null)
			{
				subTextMesh.text = "- " + -takeCounter;
			}
			if (takeCounter >= 0)
			{
				geoSpriteFsm.SendEvent("IDLE");
				takeCounter = 0;
				subTextMesh.text = "- 0";
				takeRollerState = 0;
				counterCurrent = playerData.GetInt("geo");
				geoTextMesh.text = counterCurrent.ToString();
				subTextFsm.SendEvent("DOWN");
			}
			digitChangeTimer += 0.025f;
		}
		else
		{
			digitChangeTimer -= Time.deltaTime;
		}
	}
}
