using System.Collections.Generic;
using Language;
using TMPro;
using UnityEngine;

public class MapMarkerMenu : MonoBehaviour
{
	public float xPos_start = 1.9f;

	public float xPos_interval = 1.4333f;

	public float markerY = -12.82f;

	public float markerZ = -1f;

	public float uiPause = 0.2f;

	[Space]
	public FadeGroup fadeGroup;

	[Space]
	public AudioSource audioSource;

	public AudioClip placeClip;

	public AudioClip removeClip;

	public AudioClip cursorClip;

	public AudioClip failureClip;

	public VibrationData placementVibration;

	[Space]
	public GameObject cursor;

	public PlayMakerFSM cursorTweenFSM;

	public GameObject placementCursor;

	public GameObject placementBox;

	public GameObject changeButton;

	public GameObject cancelButton;

	public TextMeshPro actionText;

	[Space]
	public GameObject marker_b;

	public GameObject marker_r;

	public GameObject marker_w;

	public GameObject marker_y;

	public TextMeshPro amount_b;

	public TextMeshPro amount_r;

	public TextMeshPro amount_w;

	public TextMeshPro amount_y;

	[Space]
	public Vector3 placementCursorOrigin;

	public float panSpeed;

	public float placementCursorMinX;

	public float placementCursorMaxX;

	public float placementCursorMinY;

	public float placementCursorMaxY;

	[Space]
	public List<GameObject> collidingMarkers;

	[Space]
	public GameObject placeEffectPrefab;

	public GameObject removeEffectPrefab;

	public Sprite spriteBlue;

	public Sprite spriteRed;

	public Sprite spriteYellow;

	public Sprite spriteWhite;

	private GameManager gm;

	private PlayerData pd;

	private InputHandler inputHandler;

	private GameObject gameMapObject;

	private GameMap gameMap;

	private bool hasMarker_r;

	private bool hasMarker_b;

	private bool hasMarker_y;

	private bool hasMarker_w;

	private int spareMarkers_r;

	private int spareMarkers_b;

	private int spareMarkers_y;

	private int spareMarkers_w;

	private int state;

	private int markerSelected;

	private float timer;

	private float confirmTimer;

	private float placementTimer;

	private Color enabledColour = new Color(1f, 1f, 1f, 1f);

	private Color disabledColour = new Color(0.5f, 0.5f, 0.5f, 1f);

	private bool collidingWithMarker;

	private string placeString;

	private string removeString;

	private void Update()
	{
		if (state == 2)
		{
			PanMap();
			HeroActions inputActions = InputHandler.Instance.inputActions;
			if (Platform.Current.GetMenuAction(inputActions.menuSubmit.WasPressed, inputActions.menuCancel.WasPressed, inputActions.jump.WasPressed, inputActions.attack.WasPressed, inputActions.cast.WasPressed) == Platform.MenuActions.Submit && confirmTimer <= 0f)
			{
				if (collidingWithMarker)
				{
					RemoveMarker();
				}
				else
				{
					PlaceMarker();
				}
			}
			if (inputHandler.inputActions.dreamNail.WasPressed && confirmTimer <= 0f)
			{
				MarkerSelectRight();
			}
			if (inputHandler.inputActions.paneRight.WasPressed && confirmTimer <= 0f)
			{
				MarkerSelectRight();
			}
			if (inputHandler.inputActions.paneLeft.WasPressed && confirmTimer <= 0f)
			{
				MarkerSelectLeft();
			}
		}
		if (timer > 0f)
		{
			timer -= Time.deltaTime;
		}
		if (confirmTimer > 0f)
		{
			confirmTimer -= Time.deltaTime;
		}
		if (placementTimer > 0f)
		{
			placementTimer -= Time.deltaTime;
		}
	}

	public void Open()
	{
		if (gm == null)
		{
			gm = GameManager.instance;
		}
		if (pd == null)
		{
			pd = PlayerData.instance;
		}
		if (inputHandler == null)
		{
			inputHandler = gm.GetComponent<InputHandler>();
		}
		if (gameMapObject == null)
		{
			gameMapObject = gm.gameMap;
			gameMap = gameMapObject.GetComponent<GameMap>();
		}
		placementCursor.SetActive(value: false);
		hasMarker_r = pd.GetBool("hasMarker_r");
		hasMarker_b = pd.GetBool("hasMarker_b");
		hasMarker_y = pd.GetBool("hasMarker_y");
		hasMarker_w = pd.GetBool("hasMarker_w");
		spareMarkers_r = pd.GetInt("spareMarkers_r");
		spareMarkers_b = pd.GetInt("spareMarkers_b");
		spareMarkers_y = pd.GetInt("spareMarkers_y");
		spareMarkers_w = pd.GetInt("spareMarkers_w");
		markerSelected = 0;
		float num = xPos_start;
		if (hasMarker_b)
		{
			marker_b.SetActive(value: true);
			marker_b.transform.localPosition = new Vector3(num, markerY, markerZ);
			num += xPos_interval;
			if (pd.GetInt("spareMarkers_b") > 0)
			{
				markerSelected = 1;
			}
		}
		if (hasMarker_r)
		{
			marker_r.SetActive(value: true);
			marker_r.transform.localPosition = new Vector3(num, markerY, markerZ);
			num += xPos_interval;
			if (markerSelected == 0 && pd.GetInt("spareMarkers_r") > 0)
			{
				markerSelected = 2;
			}
		}
		if (hasMarker_y)
		{
			marker_y.SetActive(value: true);
			marker_y.transform.localPosition = new Vector3(num, markerY, markerZ);
			num += xPos_interval;
			if (markerSelected == 0 && pd.GetInt("spareMarkers_y") > 0)
			{
				markerSelected = 3;
			}
		}
		if (hasMarker_w)
		{
			marker_w.SetActive(value: true);
			marker_w.transform.localPosition = new Vector3(num, markerY, markerZ);
			num += xPos_interval;
			if (markerSelected == 0 && pd.GetInt("spareMarkers_w") > 0)
			{
				markerSelected = 4;
			}
		}
		if (markerSelected == 0)
		{
			if (hasMarker_b)
			{
				markerSelected = 1;
			}
			else if (hasMarker_r)
			{
				markerSelected = 2;
			}
			else if (hasMarker_y)
			{
				markerSelected = 3;
			}
			else if (hasMarker_w)
			{
				markerSelected = 4;
			}
		}
		UpdateAmounts();
		cursor.SetActive(value: true);
		cursor.transform.localPosition = new Vector3(xPos_start, markerY, -3f);
		fadeGroup.FadeUp();
		changeButton.SetActive(value: true);
		cancelButton.SetActive(value: true);
		collidingMarkers.Clear();
		timer = 0f;
		confirmTimer = uiPause;
		state = 2;
		StartMarkerPlacement();
		MarkerSelect(markerSelected);
		placeString = global::Language.Language.Get("CTRL_MARKER_PLACE", "UI");
		removeString = global::Language.Language.Get("CTRL_MARKER_REMOVE", "UI");
		IsNotColliding();
	}

	public void Close()
	{
		fadeGroup.FadeDown();
		changeButton.SetActive(value: false);
		cancelButton.SetActive(value: false);
		state = 0;
		placementCursor.SetActive(value: false);
	}

	private void StartMarkerPlacement()
	{
		placementCursor.SetActive(value: true);
		placementCursor.transform.localPosition = placementCursorOrigin;
		placementBox.transform.parent = placementCursor.transform;
		placementBox.transform.localPosition = new Vector3(0f, 0f, 0f);
		confirmTimer = uiPause;
		state = 2;
	}

	private void PanMap()
	{
		if (inputHandler.inputActions.rs_down.IsPressed)
		{
			placementCursor.transform.localPosition = new Vector3(placementCursor.transform.localPosition.x, placementCursor.transform.localPosition.y - panSpeed * 2f * Time.deltaTime, placementCursor.transform.localPosition.z);
		}
		else if (inputHandler.inputActions.down.IsPressed)
		{
			placementCursor.transform.localPosition = new Vector3(placementCursor.transform.localPosition.x, placementCursor.transform.localPosition.y - panSpeed * Time.deltaTime, placementCursor.transform.localPosition.z);
		}
		else if (inputHandler.inputActions.rs_up.IsPressed)
		{
			placementCursor.transform.localPosition = new Vector3(placementCursor.transform.localPosition.x, placementCursor.transform.localPosition.y + panSpeed * 2f * Time.deltaTime, placementCursor.transform.localPosition.z);
		}
		else if (inputHandler.inputActions.up.IsPressed)
		{
			placementCursor.transform.localPosition = new Vector3(placementCursor.transform.localPosition.x, placementCursor.transform.localPosition.y + panSpeed * Time.deltaTime, placementCursor.transform.localPosition.z);
		}
		if (inputHandler.inputActions.rs_left.IsPressed)
		{
			placementCursor.transform.localPosition = new Vector3(placementCursor.transform.localPosition.x - panSpeed * 2f * Time.deltaTime, placementCursor.transform.localPosition.y, placementCursor.transform.localPosition.z);
		}
		else if (inputHandler.inputActions.left.IsPressed)
		{
			placementCursor.transform.localPosition = new Vector3(placementCursor.transform.localPosition.x - panSpeed * Time.deltaTime, placementCursor.transform.localPosition.y, placementCursor.transform.localPosition.z);
		}
		else if (inputHandler.inputActions.rs_right.IsPressed)
		{
			placementCursor.transform.localPosition = new Vector3(placementCursor.transform.localPosition.x + panSpeed * 2f * Time.deltaTime, placementCursor.transform.localPosition.y, placementCursor.transform.localPosition.z);
		}
		else if (inputHandler.inputActions.right.IsPressed)
		{
			placementCursor.transform.localPosition = new Vector3(placementCursor.transform.localPosition.x + panSpeed * Time.deltaTime, placementCursor.transform.localPosition.y, placementCursor.transform.localPosition.z);
		}
		if (placementCursor.transform.localPosition.x < placementCursorMinX)
		{
			placementCursor.transform.localPosition = new Vector3(placementCursorMinX, placementCursor.transform.localPosition.y, placementCursor.transform.localPosition.z);
			if (placementTimer <= 0f)
			{
				gameMapObject.transform.position = new Vector3(gameMapObject.transform.position.x + panSpeed * 2f * Time.deltaTime, gameMapObject.transform.position.y, gameMapObject.transform.position.z);
				gameMap.KeepWithinBounds();
			}
		}
		if (placementCursor.transform.localPosition.x > placementCursorMaxX)
		{
			placementCursor.transform.localPosition = new Vector3(placementCursorMaxX, placementCursor.transform.localPosition.y, placementCursor.transform.localPosition.z);
			if (placementTimer <= 0f)
			{
				gameMapObject.transform.position = new Vector3(gameMapObject.transform.position.x - panSpeed * 2f * Time.deltaTime, gameMapObject.transform.position.y, gameMapObject.transform.position.z);
				gameMap.KeepWithinBounds();
			}
		}
		if (placementCursor.transform.localPosition.y < placementCursorMinY)
		{
			placementCursor.transform.localPosition = new Vector3(placementCursor.transform.localPosition.x, placementCursorMinY, placementCursor.transform.localPosition.z);
			if (placementTimer <= 0f)
			{
				gameMapObject.transform.position = new Vector3(gameMapObject.transform.position.x, gameMapObject.transform.position.y + panSpeed * 2f * Time.deltaTime, gameMapObject.transform.position.z);
				gameMap.KeepWithinBounds();
			}
		}
		if (placementCursor.transform.localPosition.y > placementCursorMaxY)
		{
			placementCursor.transform.localPosition = new Vector3(placementCursor.transform.localPosition.x, placementCursorMaxY, placementCursor.transform.localPosition.z);
			if (placementTimer <= 0f)
			{
				gameMapObject.transform.position = new Vector3(gameMapObject.transform.position.x, gameMapObject.transform.position.y - panSpeed * 2f * Time.deltaTime, gameMapObject.transform.position.z);
				gameMap.KeepWithinBounds();
			}
		}
	}

	private void MarkerSelect(int selection)
	{
		marker_b.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
		marker_r.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
		marker_y.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
		marker_w.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
		Vector3 value = new Vector3(0f, 0f, 0f);
		switch (selection)
		{
		case 1:
			marker_b.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
			value = new Vector3(marker_b.transform.localPosition.x, markerY, -3f);
			break;
		case 2:
			marker_r.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
			value = new Vector3(marker_r.transform.localPosition.x, markerY, -3f);
			break;
		case 3:
			marker_y.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
			value = new Vector3(marker_y.transform.localPosition.x, markerY, -3f);
			break;
		case 4:
			marker_w.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
			value = new Vector3(marker_w.transform.localPosition.x, markerY, -3f);
			break;
		}
		cursorTweenFSM.FsmVariables.GetFsmVector3("Tween Pos").Value = value;
		cursorTweenFSM.SendEvent("TWEEN");
		audioSource.PlayOneShot(cursorClip);
	}

	private void PlaceMarker()
	{
		bool flag = false;
		if (markerSelected == 1 && pd.GetInt("spareMarkers_b") > 0)
		{
			flag = true;
		}
		if (markerSelected == 2 && pd.GetInt("spareMarkers_r") > 0)
		{
			flag = true;
		}
		if (markerSelected == 3 && pd.GetInt("spareMarkers_y") > 0)
		{
			flag = true;
		}
		if (markerSelected == 4 && pd.GetInt("spareMarkers_w") > 0)
		{
			flag = true;
		}
		if (flag)
		{
			placementBox.transform.parent = gameMapObject.transform;
			Vector3 item = new Vector3(placementBox.transform.localPosition.x, placementBox.transform.localPosition.y, -0.1f);
			placementBox.transform.parent = placementCursor.transform;
			GameObject gameObject = placeEffectPrefab.Spawn(placementCursor.transform.position, Quaternion.Euler(0f, 0f, 0f));
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -30f);
			if (markerSelected == 1)
			{
				pd.GetVariable<List<Vector3>>("placedMarkers_b").Add(item);
				PlayerData playerData = pd;
				playerData.SetIntSwappedArgs(playerData.GetInt("spareMarkers_b") - 1, "spareMarkers_b");
				gameObject.GetComponent<SpriteRenderer>().sprite = spriteBlue;
			}
			else if (markerSelected == 2)
			{
				pd.GetVariable<List<Vector3>>("placedMarkers_r").Add(item);
				PlayerData playerData2 = pd;
				playerData2.SetIntSwappedArgs(playerData2.GetInt("spareMarkers_r") - 1, "spareMarkers_r");
				gameObject.GetComponent<SpriteRenderer>().sprite = spriteRed;
			}
			else if (markerSelected == 3)
			{
				pd.GetVariable<List<Vector3>>("placedMarkers_y").Add(item);
				PlayerData playerData3 = pd;
				playerData3.SetIntSwappedArgs(playerData3.GetInt("spareMarkers_y") - 1, "spareMarkers_y");
				gameObject.GetComponent<SpriteRenderer>().sprite = spriteYellow;
			}
			else if (markerSelected == 4)
			{
				pd.GetVariable<List<Vector3>>("placedMarkers_w").Add(item);
				PlayerData playerData4 = pd;
				playerData4.SetIntSwappedArgs(playerData4.GetInt("spareMarkers_w") - 1, "spareMarkers_w");
				gameObject.GetComponent<SpriteRenderer>().sprite = spriteWhite;
			}
			UpdateAmounts();
			gameMap.SetupMapMarkers();
			audioSource.PlayOneShot(placeClip);
			VibrationManager.PlayVibrationClipOneShot(placementVibration);
			placementTimer = 0.3f;
		}
		else
		{
			audioSource.PlayOneShot(failureClip);
		}
	}

	private void RemoveMarker()
	{
		GameObject gameObject = collidingMarkers[collidingMarkers.Count - 1];
		int colour = gameObject.GetComponent<InvMarker>().colour;
		int id = gameObject.GetComponent<InvMarker>().id;
		GameObject gameObject2 = removeEffectPrefab.Spawn(placementCursor.transform.position, Quaternion.Euler(0f, 0f, 0f));
		gameObject2.transform.position = new Vector3(gameObject2.transform.position.x, gameObject2.transform.position.y, -30f);
		switch (colour)
		{
		case 0:
		{
			pd.GetVariable<List<Vector3>>("placedMarkers_b").RemoveAt(id);
			PlayerData playerData4 = pd;
			playerData4.SetIntSwappedArgs(playerData4.GetInt("spareMarkers_b") + 1, "spareMarkers_b");
			gameObject2.GetComponent<SpriteRenderer>().sprite = spriteBlue;
			break;
		}
		case 1:
		{
			pd.GetVariable<List<Vector3>>("placedMarkers_r").RemoveAt(id);
			PlayerData playerData3 = pd;
			playerData3.SetIntSwappedArgs(playerData3.GetInt("spareMarkers_r") + 1, "spareMarkers_r");
			gameObject2.GetComponent<SpriteRenderer>().sprite = spriteRed;
			break;
		}
		case 2:
		{
			pd.GetVariable<List<Vector3>>("placedMarkers_y").RemoveAt(id);
			PlayerData playerData2 = pd;
			playerData2.SetIntSwappedArgs(playerData2.GetInt("spareMarkers_y") + 1, "spareMarkers_y");
			gameObject2.GetComponent<SpriteRenderer>().sprite = spriteYellow;
			break;
		}
		case 3:
		{
			pd.GetVariable<List<Vector3>>("placedMarkers_w").RemoveAt(id);
			PlayerData playerData = pd;
			playerData.SetIntSwappedArgs(playerData.GetInt("spareMarkers_w") + 1, "spareMarkers_w");
			gameObject2.GetComponent<SpriteRenderer>().sprite = spriteWhite;
			break;
		}
		}
		collidingMarkers.Remove(gameObject);
		if (collidingMarkers.Count <= 0)
		{
			IsNotColliding();
		}
		audioSource.PlayOneShot(removeClip);
		VibrationManager.PlayVibrationClipOneShot(placementVibration);
		UpdateAmounts();
		gameMap.SetupMapMarkers();
	}

	private void MarkerSelectLeft()
	{
		bool flag = false;
		if (markerSelected == 1)
		{
			if (hasMarker_w)
			{
				markerSelected = 4;
				flag = true;
			}
			else if (hasMarker_y)
			{
				markerSelected = 3;
				flag = true;
			}
			else if (hasMarker_r)
			{
				markerSelected = 2;
				flag = true;
			}
		}
		else if (markerSelected == 2)
		{
			if (hasMarker_b)
			{
				markerSelected = 1;
				flag = true;
			}
			else if (hasMarker_w)
			{
				markerSelected = 4;
				flag = true;
			}
			else if (hasMarker_y)
			{
				markerSelected = 3;
				flag = true;
			}
		}
		else if (markerSelected == 3)
		{
			if (hasMarker_r)
			{
				markerSelected = 2;
				flag = true;
			}
			else if (hasMarker_b)
			{
				markerSelected = 1;
				flag = true;
			}
			else if (hasMarker_w)
			{
				markerSelected = 4;
				flag = true;
			}
		}
		else if (markerSelected == 4)
		{
			if (hasMarker_y)
			{
				markerSelected = 3;
				flag = true;
			}
			else if (hasMarker_r)
			{
				markerSelected = 2;
				flag = true;
			}
			else if (hasMarker_b)
			{
				markerSelected = 1;
				flag = true;
			}
		}
		if (flag)
		{
			timer = uiPause;
			MarkerSelect(markerSelected);
		}
	}

	private void MarkerSelectRight()
	{
		bool flag = false;
		if (markerSelected == 1)
		{
			if (hasMarker_r)
			{
				markerSelected = 2;
				flag = true;
			}
			else if (hasMarker_y)
			{
				markerSelected = 3;
				flag = true;
			}
			else if (hasMarker_w)
			{
				markerSelected = 4;
				flag = true;
			}
		}
		else if (markerSelected == 2)
		{
			if (hasMarker_y)
			{
				markerSelected = 3;
				flag = true;
			}
			else if (hasMarker_w)
			{
				markerSelected = 4;
				flag = true;
			}
			else if (hasMarker_b)
			{
				markerSelected = 1;
				flag = true;
			}
		}
		else if (markerSelected == 3)
		{
			if (hasMarker_w)
			{
				markerSelected = 4;
				flag = true;
			}
			else if (hasMarker_b)
			{
				markerSelected = 1;
				flag = true;
			}
			else if (hasMarker_r)
			{
				markerSelected = 2;
				flag = true;
			}
		}
		else if (markerSelected == 4)
		{
			if (hasMarker_b)
			{
				markerSelected = 1;
				flag = true;
			}
			else if (hasMarker_r)
			{
				markerSelected = 2;
				flag = true;
			}
			else if (hasMarker_y)
			{
				markerSelected = 3;
				flag = true;
			}
		}
		if (flag)
		{
			timer = uiPause;
			MarkerSelect(markerSelected);
		}
	}

	private void UpdateAmounts()
	{
		amount_b.text = pd.spareMarkers_b.ToString();
		amount_r.text = pd.spareMarkers_r.ToString();
		amount_y.text = pd.spareMarkers_y.ToString();
		amount_w.text = pd.spareMarkers_w.ToString();
		if (pd.GetInt("spareMarkers_b") > 0)
		{
			marker_b.GetComponent<SpriteRenderer>().color = enabledColour;
			amount_b.GetComponent<TextMeshPro>().color = enabledColour;
		}
		else
		{
			marker_b.GetComponent<SpriteRenderer>().color = disabledColour;
			amount_b.GetComponent<TextMeshPro>().color = disabledColour;
		}
		if (pd.GetInt("spareMarkers_r") > 0)
		{
			marker_r.GetComponent<SpriteRenderer>().color = enabledColour;
			amount_r.GetComponent<TextMeshPro>().color = enabledColour;
		}
		else
		{
			marker_r.GetComponent<SpriteRenderer>().color = disabledColour;
			amount_r.GetComponent<TextMeshPro>().color = disabledColour;
		}
		if (pd.GetInt("spareMarkers_y") > 0)
		{
			marker_y.GetComponent<SpriteRenderer>().color = enabledColour;
			amount_y.GetComponent<TextMeshPro>().color = enabledColour;
		}
		else
		{
			marker_y.GetComponent<SpriteRenderer>().color = disabledColour;
			amount_y.GetComponent<TextMeshPro>().color = disabledColour;
		}
		if (pd.GetInt("spareMarkers_w") > 0)
		{
			marker_w.GetComponent<SpriteRenderer>().color = enabledColour;
			amount_w.GetComponent<TextMeshPro>().color = enabledColour;
		}
		else
		{
			marker_w.GetComponent<SpriteRenderer>().color = disabledColour;
			amount_w.GetComponent<TextMeshPro>().color = disabledColour;
		}
	}

	public void AddToCollidingList(GameObject go)
	{
		collidingMarkers.Add(go);
		IsColliding();
	}

	public void RemoveFromCollidingList(GameObject go)
	{
		collidingMarkers.Remove(go);
		if (collidingMarkers.Count <= 0)
		{
			IsNotColliding();
		}
	}

	private void IsColliding()
	{
		collidingWithMarker = true;
		actionText.text = removeString;
	}

	private void IsNotColliding()
	{
		collidingWithMarker = false;
		actionText.text = placeString;
	}
}
