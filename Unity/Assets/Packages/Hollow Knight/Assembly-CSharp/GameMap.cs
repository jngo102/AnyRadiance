using System.Collections.Generic;
using UnityEngine;

public class GameMap : MonoBehaviour
{
	private GameManager gm;

	private PlayerData pd;

	private GameObject hero;

	private InputHandler inputHandler;

	public GameObject compassIcon;

	private float originOffsetX;

	private float originOffsetY;

	private float sceneWidth;

	private float sceneHeight;

	public float doorX;

	public float doorY;

	public string doorScene;

	public string doorMapZone;

	public float doorOriginOffsetX;

	public float doorOriginOffsetY;

	public float doorSceneWidth;

	public float doorSceneHeight;

	public bool inRoom;

	public GameObject areaAncientBasin;

	public GameObject areaCity;

	public GameObject areaCliffs;

	public GameObject areaCrossroads;

	public GameObject areaCrystalPeak;

	public GameObject areaDeepnest;

	public GameObject areaFogCanyon;

	public GameObject areaFungalWastes;

	public GameObject areaGreenpath;

	public GameObject areaKingdomsEdge;

	public GameObject areaQueensGardens;

	public GameObject areaRestingGrounds;

	public GameObject areaDirtmouth;

	public GameObject areaWaterways;

	public GameObject flamePins;

	public GameObject dreamerPins;

	public GameObject shadeMarker;

	public GameObject dreamGateMarker;

	private bool posGate;

	public bool displayNextArea;

	private bool displayingCompass;

	public Vector3 currentScenePos;

	public GameObject currentScene;

	public bool canPan;

	public float panSpeed;

	public float panMinX;

	public float panMaxX;

	public float panMinY;

	public float panMaxY;

	public GameObject panArrowU;

	public GameObject panArrowD;

	public GameObject panArrowL;

	public GameObject panArrowR;

	public GameObject[] mapMarkersBlue;

	public GameObject[] mapMarkersRed;

	public GameObject[] mapMarkersYellow;

	public GameObject[] mapMarkersWhite;

	private void OnEnable()
	{
		gm = GameManager.instance;
	}

	private void OnDisable()
	{
	}

	private void Start()
	{
		gm = GameManager.instance;
		pd = PlayerData.instance;
		hero = HeroController.instance.gameObject;
		inputHandler = gm.GetComponent<InputHandler>();
		if (gm.IsGameplayScene())
		{
			GetTilemapDimensions();
		}
	}

	public void LevelReady()
	{
		inRoom = false;
		if (gm.IsGameplayScene())
		{
			GetTilemapDimensions();
		}
	}

	private void OnLevelWasLoaded()
	{
		inRoom = false;
		if (gm.IsGameplayScene())
		{
			GetTilemapDimensions();
		}
	}

	public void SetCompassPoint()
	{
		pd.SetFloatSwappedArgs(doorX, "gMap_doorX");
		pd.SetFloatSwappedArgs(doorY, "gMap_doorY");
		pd.SetStringSwappedArgs(doorScene, "gMap_doorScene");
		pd.SetStringSwappedArgs(doorMapZone, "gMap_doorMapZone");
		pd.SetFloatSwappedArgs(doorOriginOffsetX, "gMap_doorOriginOffsetX");
		pd.SetFloatSwappedArgs(doorOriginOffsetY, "gMap_doorOriginOffsetY");
		pd.SetFloatSwappedArgs(doorSceneWidth, "gMap_doorSceneWidth");
		pd.SetFloatSwappedArgs(doorSceneHeight, "gMap_doorSceneHeight");
	}

	public void GetDoorValues()
	{
		doorX = pd.GetFloat("gMap_doorX");
		doorY = pd.GetFloat("gMap_doorY");
		doorScene = pd.GetString("gMap_doorScene");
		doorMapZone = pd.GetString("gMap_doorMapZone");
		doorOriginOffsetX = pd.GetFloat("gMap_doorOriginOffsetX");
		doorOriginOffsetY = pd.GetFloat("gMap_doorOriginOffsetY");
		doorSceneWidth = pd.GetFloat("gMap_doorSceneWidth");
		doorSceneHeight = pd.GetFloat("gMap_doorSceneHeight");
	}

	public void SetupMap(bool pinsOnly = false)
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			GameObject gameObject = base.transform.GetChild(i).gameObject;
			for (int j = 0; j < gameObject.transform.childCount; j++)
			{
				GameObject gameObject2 = gameObject.transform.GetChild(j).gameObject;
				if (!pd.GetVariable<List<string>>("scenesMapped").Contains(gameObject2.transform.name) && !pd.GetBool("mapAllRooms"))
				{
					continue;
				}
				if (pd.GetBool("hasQuill") && !pinsOnly)
				{
					gameObject2.SetActive(value: true);
				}
				for (int k = 0; k < gameObject2.transform.childCount; k++)
				{
					GameObject gameObject3 = gameObject2.transform.GetChild(k).gameObject;
					if (gameObject3.name == "pin_blue_health" && !gameObject3.activeSelf && pd.GetVariable<List<string>>("scenesEncounteredCocoon").Contains(gameObject2.transform.name) && pd.GetBool("hasPinCocoon"))
					{
						gameObject3.SetActive(value: true);
					}
					if (gameObject3.name == "pin_dream_tree" && !gameObject3.activeSelf && pd.GetVariable<List<string>>("scenesEncounteredDreamPlant").Contains(gameObject2.transform.name) && pd.GetBool("hasPinDreamPlant"))
					{
						gameObject3.SetActive(value: true);
					}
					if (gameObject3.name == "pin_dream_tree" && gameObject3.activeSelf && pd.GetVariable<List<string>>("scenesEncounteredDreamPlantC").Contains(gameObject2.transform.name))
					{
						gameObject3.SetActive(value: false);
					}
				}
			}
		}
	}

	private void GetTilemapDimensions()
	{
		originOffsetX = 0f;
		originOffsetY = 0f;
		tk2dTileMap tilemap = gm.tilemap;
		sceneWidth = tilemap.width;
		sceneHeight = tilemap.height;
	}

	public void WorldMap()
	{
		string currentMapZone = gm.GetCurrentMapZone();
		displayNextArea = false;
		shadeMarker.SetActive(value: true);
		dreamGateMarker.SetActive(value: true);
		dreamerPins.SetActive(value: true);
		SetupMapMarkers();
		panMinX = -1.44f;
		panMaxX = 4.55f;
		panMinY = -8.642f;
		panMaxY = -5.58f;
		if (pd.GetBool("mapAbyss") || (currentMapZone == "ABYSS" && pd.GetBool("equippedCharm_2")))
		{
			if (pd.GetBool("mapAbyss"))
			{
				areaAncientBasin.SetActive(value: true);
			}
			if (panMinX > -13.44f)
			{
				panMinX = -13.44f;
			}
			if (panMaxY < 15.6913f)
			{
				panMaxY = 15.6913f;
			}
		}
		if (!pd.GetBool("mapCity"))
		{
			switch (currentMapZone)
			{
			case "CITY":
			case "KINGS_STATION":
			case "SOUL_SOCIETY":
			case "LURIENS_TOWER":
				break;
			default:
				goto IL_018e;
			}
			if (!pd.GetBool("equippedCharm_2"))
			{
				goto IL_018e;
			}
		}
		if (pd.GetBool("mapCity"))
		{
			areaCity.SetActive(value: true);
		}
		if (panMinX > -14.26f)
		{
			panMinX = -14.26f;
		}
		if (panMaxY < 2.27f)
		{
			panMaxY = 2.27f;
		}
		goto IL_018e;
		IL_018e:
		if (pd.GetBool("mapCliffs") || (currentMapZone == "CLIFFS" && pd.GetBool("equippedCharm_2")))
		{
			if (pd.GetBool("mapCliffs"))
			{
				areaCliffs.SetActive(value: true);
			}
			if (panMaxX < 9.07f)
			{
				panMaxX = 9.07f;
			}
			if (panMinY > -10.6653f)
			{
				panMinY = -10.6653f;
			}
		}
		if (pd.GetBool("mapCrossroads"))
		{
			areaCrossroads.SetActive(value: true);
		}
		if (pd.GetBool("mapMines") || (currentMapZone == "MINES" && pd.GetBool("equippedCharm_2")))
		{
			if (pd.GetBool("mapMines"))
			{
				areaCrystalPeak.SetActive(value: true);
			}
			if (panMinX > -13.47f)
			{
				panMinX = -13.47f;
			}
			if (panMinY > -12.58548f)
			{
				panMinY = -12.58548f;
			}
		}
		if (pd.GetBool("mapDeepnest") || ((currentMapZone == "DEEPNEST" || currentMapZone == "BEASTS_DEN") && pd.GetBool("equippedCharm_2")))
		{
			if (pd.GetBool("mapDeepnest"))
			{
				areaDeepnest.SetActive(value: true);
			}
			if (panMaxX < 17.3f)
			{
				panMaxX = 17.3f;
			}
			if (panMaxY < 8.29f)
			{
				panMaxY = 8.29f;
			}
		}
		if (pd.GetBool("mapFogCanyon") || ((currentMapZone == "FOG_CANYON" || currentMapZone == "MONOMON_ARCHIVE") && pd.GetBool("equippedCharm_2")))
		{
			if (pd.GetBool("mapFogCanyon"))
			{
				areaFogCanyon.SetActive(value: true);
			}
			if (panMaxX < 7.13f)
			{
				panMaxX = 7.13f;
			}
			if (panMaxY < -2.49f)
			{
				panMaxY = -2.49f;
			}
		}
		if (pd.GetBool("mapFungalWastes") || ((currentMapZone == "WASTES" || currentMapZone == "QUEENS_STATION") && pd.GetBool("equippedCharm_2")))
		{
			if (pd.GetBool("mapFungalWastes"))
			{
				areaFungalWastes.SetActive(value: true);
			}
			if (panMaxY < 4.14f)
			{
				panMaxY = 4.14f;
			}
		}
		if (pd.GetBool("mapGreenpath") || (currentMapZone == "GREEN_PATH" && pd.GetBool("equippedCharm_2")))
		{
			if (pd.GetBool("mapGreenpath"))
			{
				areaGreenpath.SetActive(value: true);
			}
			if (panMaxX < 17.26f)
			{
				panMaxX = 17.26f;
			}
			if (panMaxY < -6.22f)
			{
				panMaxY = -6.22f;
			}
		}
		if (!pd.GetBool("mapOutskirts"))
		{
			switch (currentMapZone)
			{
			case "OUTSKIRTS":
			case "HIVE":
			case "COLOSSEUM":
				break;
			default:
				goto IL_054e;
			}
			if (!pd.GetBool("equippedCharm_2"))
			{
				goto IL_054e;
			}
		}
		if (pd.GetBool("mapOutskirts"))
		{
			areaKingdomsEdge.SetActive(value: true);
		}
		if (panMinX > -24.16f)
		{
			panMinX = -24.16f;
		}
		if (panMaxY < 8.16f)
		{
			panMaxY = 8.16f;
		}
		goto IL_054e;
		IL_054e:
		if (pd.GetBool("mapRoyalGardens") || (currentMapZone == "ROYAL_GARDENS" && pd.GetBool("equippedCharm_2")))
		{
			if (pd.GetBool("mapRoyalGardens"))
			{
				areaQueensGardens.SetActive(value: true);
			}
			if (panMaxX < 17.3f)
			{
				panMaxX = 17.3f;
			}
			if (panMaxY < 1.53f)
			{
				panMaxY = 1.53f;
			}
		}
		if (pd.GetBool("mapRestingGrounds") || (currentMapZone == "RESTING_GROUNDS" && pd.GetBool("equippedCharm_2")))
		{
			if (pd.GetBool("mapRestingGrounds"))
			{
				areaRestingGrounds.SetActive(value: true);
			}
			if (panMinX > -14.59f)
			{
				panMinX = -14.59f;
			}
			if (panMaxY < -5.77f)
			{
				panMaxY = -5.77f;
			}
		}
		areaDirtmouth.SetActive(value: true);
		if (pd.GetBool("mapWaterways") || ((currentMapZone == "WATERWAYS" || currentMapZone == "GODSEEKER_WASTE") && pd.GetBool("equippedCharm_2")))
		{
			if (pd.GetBool("mapWaterways"))
			{
				areaWaterways.SetActive(value: true);
			}
			if (panMinX > -11.39f)
			{
				panMinX = -11.39f;
			}
			if (panMaxY < 7.06f)
			{
				panMaxY = 7.06f;
			}
		}
		flamePins.SetActive(value: true);
		PositionCompass(posShade: false);
	}

	public void QuickMapAncientBasin()
	{
		shadeMarker.SetActive(value: true);
		flamePins.SetActive(value: true);
		dreamGateMarker.SetActive(value: true);
		dreamerPins.SetActive(value: true);
		displayNextArea = true;
		SetupMapMarkers();
		areaAncientBasin.SetActive(value: true);
		areaCity.SetActive(value: false);
		areaCliffs.SetActive(value: false);
		areaCrossroads.SetActive(value: false);
		areaCrystalPeak.SetActive(value: false);
		areaDeepnest.SetActive(value: false);
		areaFogCanyon.SetActive(value: false);
		areaFungalWastes.SetActive(value: false);
		areaGreenpath.SetActive(value: false);
		areaKingdomsEdge.SetActive(value: false);
		areaQueensGardens.SetActive(value: false);
		areaRestingGrounds.SetActive(value: false);
		areaDirtmouth.SetActive(value: false);
		areaWaterways.SetActive(value: false);
		PositionCompass(posShade: false);
	}

	public void QuickMapCity()
	{
		shadeMarker.SetActive(value: true);
		flamePins.SetActive(value: true);
		dreamGateMarker.SetActive(value: true);
		dreamerPins.SetActive(value: true);
		displayNextArea = true;
		SetupMapMarkers();
		areaAncientBasin.SetActive(value: false);
		areaCity.SetActive(value: true);
		areaCliffs.SetActive(value: false);
		areaCrossroads.SetActive(value: false);
		areaCrystalPeak.SetActive(value: false);
		areaDeepnest.SetActive(value: false);
		areaFogCanyon.SetActive(value: false);
		areaFungalWastes.SetActive(value: false);
		areaGreenpath.SetActive(value: false);
		areaKingdomsEdge.SetActive(value: false);
		areaQueensGardens.SetActive(value: false);
		areaRestingGrounds.SetActive(value: false);
		areaDirtmouth.SetActive(value: false);
		areaWaterways.SetActive(value: false);
		PositionCompass(posShade: false);
	}

	public void QuickMapCliffs()
	{
		shadeMarker.SetActive(value: true);
		flamePins.SetActive(value: true);
		dreamGateMarker.SetActive(value: true);
		dreamerPins.SetActive(value: true);
		displayNextArea = true;
		SetupMapMarkers();
		areaAncientBasin.SetActive(value: false);
		areaCity.SetActive(value: false);
		areaCliffs.SetActive(value: true);
		areaCrossroads.SetActive(value: false);
		areaCrystalPeak.SetActive(value: false);
		areaDeepnest.SetActive(value: false);
		areaFogCanyon.SetActive(value: false);
		areaFungalWastes.SetActive(value: false);
		areaGreenpath.SetActive(value: false);
		areaKingdomsEdge.SetActive(value: false);
		areaQueensGardens.SetActive(value: false);
		areaRestingGrounds.SetActive(value: false);
		areaDirtmouth.SetActive(value: false);
		areaWaterways.SetActive(value: false);
		PositionCompass(posShade: false);
	}

	public void QuickMapCrossroads()
	{
		shadeMarker.SetActive(value: true);
		flamePins.SetActive(value: true);
		dreamGateMarker.SetActive(value: true);
		dreamerPins.SetActive(value: true);
		displayNextArea = true;
		SetupMapMarkers();
		areaAncientBasin.SetActive(value: false);
		areaCity.SetActive(value: false);
		areaCliffs.SetActive(value: false);
		areaCrossroads.SetActive(value: true);
		areaCrystalPeak.SetActive(value: false);
		areaDeepnest.SetActive(value: false);
		areaFogCanyon.SetActive(value: false);
		areaFungalWastes.SetActive(value: false);
		areaGreenpath.SetActive(value: false);
		areaKingdomsEdge.SetActive(value: false);
		areaQueensGardens.SetActive(value: false);
		areaRestingGrounds.SetActive(value: false);
		areaDirtmouth.SetActive(value: false);
		areaWaterways.SetActive(value: false);
		PositionCompass(posShade: false);
	}

	public void QuickMapCrystalPeak()
	{
		shadeMarker.SetActive(value: true);
		flamePins.SetActive(value: true);
		dreamGateMarker.SetActive(value: true);
		dreamerPins.SetActive(value: true);
		displayNextArea = true;
		SetupMapMarkers();
		areaAncientBasin.SetActive(value: false);
		areaCity.SetActive(value: false);
		areaCliffs.SetActive(value: false);
		areaCrossroads.SetActive(value: false);
		areaCrystalPeak.SetActive(value: true);
		areaDeepnest.SetActive(value: false);
		areaFogCanyon.SetActive(value: false);
		areaFungalWastes.SetActive(value: false);
		areaGreenpath.SetActive(value: false);
		areaKingdomsEdge.SetActive(value: false);
		areaQueensGardens.SetActive(value: false);
		areaRestingGrounds.SetActive(value: false);
		areaDirtmouth.SetActive(value: false);
		areaWaterways.SetActive(value: false);
		PositionCompass(posShade: false);
	}

	public void QuickMapDeepnest()
	{
		shadeMarker.SetActive(value: true);
		flamePins.SetActive(value: true);
		dreamGateMarker.SetActive(value: true);
		dreamerPins.SetActive(value: true);
		displayNextArea = true;
		SetupMapMarkers();
		areaAncientBasin.SetActive(value: false);
		areaCity.SetActive(value: false);
		areaCliffs.SetActive(value: false);
		areaCrossroads.SetActive(value: false);
		areaCrystalPeak.SetActive(value: false);
		areaDeepnest.SetActive(value: true);
		areaFogCanyon.SetActive(value: false);
		areaFungalWastes.SetActive(value: false);
		areaGreenpath.SetActive(value: false);
		areaKingdomsEdge.SetActive(value: false);
		areaQueensGardens.SetActive(value: false);
		areaRestingGrounds.SetActive(value: false);
		areaDirtmouth.SetActive(value: false);
		areaWaterways.SetActive(value: false);
		PositionCompass(posShade: false);
	}

	public void QuickMapFogCanyon()
	{
		shadeMarker.SetActive(value: true);
		flamePins.SetActive(value: true);
		dreamGateMarker.SetActive(value: true);
		dreamerPins.SetActive(value: true);
		displayNextArea = true;
		SetupMapMarkers();
		areaAncientBasin.SetActive(value: false);
		areaCity.SetActive(value: false);
		areaCliffs.SetActive(value: false);
		areaCrossroads.SetActive(value: false);
		areaCrystalPeak.SetActive(value: false);
		areaDeepnest.SetActive(value: false);
		areaFogCanyon.SetActive(value: true);
		areaFungalWastes.SetActive(value: false);
		areaGreenpath.SetActive(value: false);
		areaKingdomsEdge.SetActive(value: false);
		areaQueensGardens.SetActive(value: false);
		areaRestingGrounds.SetActive(value: false);
		areaDirtmouth.SetActive(value: false);
		areaWaterways.SetActive(value: false);
		PositionCompass(posShade: false);
	}

	public void QuickMapFungalWastes()
	{
		shadeMarker.SetActive(value: true);
		flamePins.SetActive(value: true);
		dreamGateMarker.SetActive(value: true);
		dreamerPins.SetActive(value: true);
		displayNextArea = true;
		SetupMapMarkers();
		areaAncientBasin.SetActive(value: false);
		areaCity.SetActive(value: false);
		areaCliffs.SetActive(value: false);
		areaCrossroads.SetActive(value: false);
		areaCrystalPeak.SetActive(value: false);
		areaDeepnest.SetActive(value: false);
		areaFogCanyon.SetActive(value: false);
		areaFungalWastes.SetActive(value: true);
		areaGreenpath.SetActive(value: false);
		areaKingdomsEdge.SetActive(value: false);
		areaQueensGardens.SetActive(value: false);
		areaRestingGrounds.SetActive(value: false);
		areaDirtmouth.SetActive(value: false);
		areaWaterways.SetActive(value: false);
		PositionCompass(posShade: false);
	}

	public void QuickMapGreenpath()
	{
		shadeMarker.SetActive(value: true);
		flamePins.SetActive(value: true);
		dreamGateMarker.SetActive(value: true);
		dreamerPins.SetActive(value: true);
		displayNextArea = true;
		SetupMapMarkers();
		areaAncientBasin.SetActive(value: false);
		areaCity.SetActive(value: false);
		areaCliffs.SetActive(value: false);
		areaCrossroads.SetActive(value: false);
		areaCrystalPeak.SetActive(value: false);
		areaDeepnest.SetActive(value: false);
		areaFogCanyon.SetActive(value: false);
		areaFungalWastes.SetActive(value: false);
		areaGreenpath.SetActive(value: true);
		areaKingdomsEdge.SetActive(value: false);
		areaQueensGardens.SetActive(value: false);
		areaRestingGrounds.SetActive(value: false);
		areaDirtmouth.SetActive(value: false);
		areaWaterways.SetActive(value: false);
		PositionCompass(posShade: false);
	}

	public void QuickMapKingdomsEdge()
	{
		shadeMarker.SetActive(value: true);
		flamePins.SetActive(value: true);
		dreamGateMarker.SetActive(value: true);
		dreamerPins.SetActive(value: true);
		displayNextArea = true;
		SetupMapMarkers();
		areaAncientBasin.SetActive(value: false);
		areaCity.SetActive(value: false);
		areaCliffs.SetActive(value: false);
		areaCrossroads.SetActive(value: false);
		areaCrystalPeak.SetActive(value: false);
		areaDeepnest.SetActive(value: false);
		areaFogCanyon.SetActive(value: false);
		areaFungalWastes.SetActive(value: false);
		areaGreenpath.SetActive(value: false);
		areaKingdomsEdge.SetActive(value: true);
		areaQueensGardens.SetActive(value: false);
		areaRestingGrounds.SetActive(value: false);
		areaDirtmouth.SetActive(value: false);
		areaWaterways.SetActive(value: false);
		PositionCompass(posShade: false);
	}

	public void QuickMapQueensGardens()
	{
		shadeMarker.SetActive(value: true);
		flamePins.SetActive(value: true);
		dreamGateMarker.SetActive(value: true);
		dreamerPins.SetActive(value: true);
		displayNextArea = true;
		SetupMapMarkers();
		areaAncientBasin.SetActive(value: false);
		areaCity.SetActive(value: false);
		areaCliffs.SetActive(value: false);
		areaCrossroads.SetActive(value: false);
		areaCrystalPeak.SetActive(value: false);
		areaDeepnest.SetActive(value: false);
		areaFogCanyon.SetActive(value: false);
		areaFungalWastes.SetActive(value: false);
		areaGreenpath.SetActive(value: false);
		areaKingdomsEdge.SetActive(value: false);
		areaQueensGardens.SetActive(value: true);
		areaRestingGrounds.SetActive(value: false);
		areaDirtmouth.SetActive(value: false);
		areaWaterways.SetActive(value: false);
		PositionCompass(posShade: false);
	}

	public void QuickMapRestingGrounds()
	{
		shadeMarker.SetActive(value: true);
		flamePins.SetActive(value: true);
		dreamGateMarker.SetActive(value: true);
		dreamerPins.SetActive(value: true);
		displayNextArea = true;
		SetupMapMarkers();
		areaAncientBasin.SetActive(value: false);
		areaCity.SetActive(value: false);
		areaCliffs.SetActive(value: false);
		areaCrossroads.SetActive(value: false);
		areaCrystalPeak.SetActive(value: false);
		areaDeepnest.SetActive(value: false);
		areaFogCanyon.SetActive(value: false);
		areaFungalWastes.SetActive(value: false);
		areaGreenpath.SetActive(value: false);
		areaKingdomsEdge.SetActive(value: false);
		areaQueensGardens.SetActive(value: true);
		areaRestingGrounds.SetActive(value: true);
		areaDirtmouth.SetActive(value: false);
		areaWaterways.SetActive(value: false);
		PositionCompass(posShade: false);
	}

	public void QuickMapDirtmouth()
	{
		shadeMarker.SetActive(value: true);
		flamePins.SetActive(value: true);
		dreamGateMarker.SetActive(value: true);
		dreamerPins.SetActive(value: true);
		displayNextArea = true;
		SetupMapMarkers();
		areaAncientBasin.SetActive(value: false);
		areaCity.SetActive(value: false);
		areaCliffs.SetActive(value: false);
		areaCrossroads.SetActive(value: false);
		areaCrystalPeak.SetActive(value: false);
		areaDeepnest.SetActive(value: false);
		areaFogCanyon.SetActive(value: false);
		areaFungalWastes.SetActive(value: false);
		areaGreenpath.SetActive(value: false);
		areaKingdomsEdge.SetActive(value: false);
		areaQueensGardens.SetActive(value: false);
		areaRestingGrounds.SetActive(value: false);
		areaDirtmouth.SetActive(value: true);
		areaWaterways.SetActive(value: false);
		PositionCompass(posShade: false);
	}

	public void QuickMapWaterways()
	{
		shadeMarker.SetActive(value: true);
		flamePins.SetActive(value: true);
		dreamGateMarker.SetActive(value: true);
		dreamerPins.SetActive(value: true);
		displayNextArea = true;
		SetupMapMarkers();
		areaAncientBasin.SetActive(value: false);
		areaCity.SetActive(value: false);
		areaCliffs.SetActive(value: false);
		areaCrossroads.SetActive(value: false);
		areaCrystalPeak.SetActive(value: false);
		areaDeepnest.SetActive(value: false);
		areaFogCanyon.SetActive(value: false);
		areaFungalWastes.SetActive(value: false);
		areaGreenpath.SetActive(value: false);
		areaKingdomsEdge.SetActive(value: false);
		areaQueensGardens.SetActive(value: false);
		areaRestingGrounds.SetActive(value: false);
		areaDirtmouth.SetActive(value: false);
		areaWaterways.SetActive(value: true);
		areaDirtmouth.SetActive(value: false);
		PositionCompass(posShade: false);
	}

	public void CloseQuickMap()
	{
		shadeMarker.SetActive(value: false);
		dreamGateMarker.SetActive(value: false);
		dreamerPins.SetActive(value: false);
		DisableMarkers();
		areaAncientBasin.SetActive(value: false);
		areaCity.SetActive(value: false);
		areaCliffs.SetActive(value: false);
		areaCrossroads.SetActive(value: false);
		areaCrystalPeak.SetActive(value: false);
		areaDeepnest.SetActive(value: false);
		areaFogCanyon.SetActive(value: false);
		areaFungalWastes.SetActive(value: false);
		areaGreenpath.SetActive(value: false);
		areaKingdomsEdge.SetActive(value: false);
		areaQueensGardens.SetActive(value: false);
		areaRestingGrounds.SetActive(value: false);
		areaDirtmouth.SetActive(value: false);
		flamePins.SetActive(value: false);
		areaWaterways.SetActive(value: false);
		compassIcon.SetActive(value: false);
		displayNextArea = false;
		displayingCompass = false;
	}

	public void PositionDreamGateMarker()
	{
		posGate = true;
		PositionCompass(posShade: false);
		posGate = false;
	}

	public void PositionCompass(bool posShade)
	{
		GameObject gameObject = null;
		string currentMapZone = gm.GetCurrentMapZone();
		switch (currentMapZone)
		{
		case "DREAM_WORLD":
		case "WHITE_PALACE":
		case "GODS_GLORY":
			compassIcon.SetActive(value: false);
			displayingCompass = false;
			return;
		}
		string sceneName;
		if (!inRoom)
		{
			sceneName = gm.sceneName;
		}
		else
		{
			currentMapZone = doorMapZone;
			sceneName = doorScene;
		}
		switch (currentMapZone)
		{
		case "ABYSS":
		{
			gameObject = areaAncientBasin;
			for (int num6 = 0; num6 < areaAncientBasin.transform.childCount; num6++)
			{
				GameObject gameObject13 = areaAncientBasin.transform.GetChild(num6).gameObject;
				if (gameObject13.name == sceneName)
				{
					currentScene = gameObject13;
					break;
				}
			}
			break;
		}
		case "CITY":
		case "KINGS_STATION":
		case "SOUL_SOCIETY":
		case "LURIENS_TOWER":
		{
			gameObject = areaCity;
			for (int l = 0; l < areaCity.transform.childCount; l++)
			{
				GameObject gameObject5 = areaCity.transform.GetChild(l).gameObject;
				if (gameObject5.name == sceneName)
				{
					currentScene = gameObject5;
					break;
				}
			}
			break;
		}
		case "CLIFFS":
		{
			gameObject = areaCliffs;
			for (int num2 = 0; num2 < areaCliffs.transform.childCount; num2++)
			{
				GameObject gameObject9 = areaCliffs.transform.GetChild(num2).gameObject;
				if (gameObject9.name == sceneName)
				{
					currentScene = gameObject9;
					break;
				}
			}
			break;
		}
		case "CROSSROADS":
		case "SHAMAN_TEMPLE":
		{
			gameObject = areaCrossroads;
			for (int num8 = 0; num8 < areaCrossroads.transform.childCount; num8++)
			{
				GameObject gameObject15 = areaCrossroads.transform.GetChild(num8).gameObject;
				if (gameObject15.name == sceneName)
				{
					currentScene = gameObject15;
					break;
				}
			}
			break;
		}
		case "MINES":
		{
			gameObject = areaCrystalPeak;
			for (int num4 = 0; num4 < areaCrystalPeak.transform.childCount; num4++)
			{
				GameObject gameObject11 = areaCrystalPeak.transform.GetChild(num4).gameObject;
				if (gameObject11.name == sceneName)
				{
					currentScene = gameObject11;
					break;
				}
			}
			break;
		}
		case "DEEPNEST":
		case "BEASTS_DEN":
		{
			gameObject = areaDeepnest;
			for (int n = 0; n < areaDeepnest.transform.childCount; n++)
			{
				GameObject gameObject7 = areaDeepnest.transform.GetChild(n).gameObject;
				if (gameObject7.name == sceneName)
				{
					currentScene = gameObject7;
					break;
				}
			}
			break;
		}
		case "FOG_CANYON":
		case "MONOMON_ARCHIVE":
		{
			gameObject = areaFogCanyon;
			for (int j = 0; j < areaFogCanyon.transform.childCount; j++)
			{
				GameObject gameObject3 = areaFogCanyon.transform.GetChild(j).gameObject;
				if (gameObject3.name == sceneName)
				{
					currentScene = gameObject3;
					break;
				}
			}
			break;
		}
		case "WASTES":
		case "QUEENS_STATION":
		{
			gameObject = areaFungalWastes;
			for (int num7 = 0; num7 < areaFungalWastes.transform.childCount; num7++)
			{
				GameObject gameObject14 = areaFungalWastes.transform.GetChild(num7).gameObject;
				if (gameObject14.name == sceneName)
				{
					currentScene = gameObject14;
					break;
				}
			}
			break;
		}
		case "GREEN_PATH":
		{
			gameObject = areaGreenpath;
			for (int num5 = 0; num5 < areaGreenpath.transform.childCount; num5++)
			{
				GameObject gameObject12 = areaGreenpath.transform.GetChild(num5).gameObject;
				if (gameObject12.name == sceneName)
				{
					currentScene = gameObject12;
					break;
				}
			}
			break;
		}
		case "OUTSKIRTS":
		case "HIVE":
		case "COLOSSEUM":
		{
			gameObject = areaKingdomsEdge;
			for (int num3 = 0; num3 < areaKingdomsEdge.transform.childCount; num3++)
			{
				GameObject gameObject10 = areaKingdomsEdge.transform.GetChild(num3).gameObject;
				if (gameObject10.name == sceneName)
				{
					currentScene = gameObject10;
					break;
				}
			}
			break;
		}
		case "ROYAL_GARDENS":
		{
			gameObject = areaQueensGardens;
			for (int num = 0; num < areaQueensGardens.transform.childCount; num++)
			{
				GameObject gameObject8 = areaQueensGardens.transform.GetChild(num).gameObject;
				if (gameObject8.name == sceneName)
				{
					currentScene = gameObject8;
					break;
				}
			}
			break;
		}
		case "RESTING_GROUNDS":
		{
			gameObject = areaRestingGrounds;
			for (int m = 0; m < areaRestingGrounds.transform.childCount; m++)
			{
				GameObject gameObject6 = areaRestingGrounds.transform.GetChild(m).gameObject;
				if (gameObject6.name == sceneName)
				{
					currentScene = gameObject6;
					break;
				}
			}
			break;
		}
		case "TOWN":
		case "KINGS_PASS":
		{
			gameObject = areaDirtmouth;
			for (int k = 0; k < areaDirtmouth.transform.childCount; k++)
			{
				GameObject gameObject4 = areaDirtmouth.transform.GetChild(k).gameObject;
				if (gameObject4.name == sceneName)
				{
					currentScene = gameObject4;
					break;
				}
			}
			break;
		}
		case "WATERWAYS":
		case "GODSEEKER_WASTE":
		{
			gameObject = areaWaterways;
			for (int i = 0; i < areaWaterways.transform.childCount; i++)
			{
				GameObject gameObject2 = areaWaterways.transform.GetChild(i).gameObject;
				if (gameObject2.name == sceneName)
				{
					currentScene = gameObject2;
					break;
				}
			}
			break;
		}
		}
		if (currentScene != null)
		{
			currentScenePos = new Vector3(currentScene.transform.localPosition.x + gameObject.transform.localPosition.x, currentScene.transform.localPosition.y + gameObject.transform.localPosition.y, 0f);
			if (!posShade && !posGate)
			{
				if (pd.GetBool("equippedCharm_2"))
				{
					displayingCompass = true;
					compassIcon.SetActive(value: true);
				}
				else
				{
					compassIcon.SetActive(value: false);
					displayingCompass = false;
				}
			}
			if (posShade)
			{
				if (!inRoom)
				{
					shadeMarker.transform.localPosition = new Vector3(currentScenePos.x, currentScenePos.y, 0f);
				}
				else
				{
					float x = currentScenePos.x - currentScene.GetComponent<SpriteRenderer>().sprite.rect.size.x / 100f / 2f + (doorX + doorOriginOffsetX) / doorSceneWidth * (currentScene.GetComponent<SpriteRenderer>().sprite.rect.size.x / 100f * base.transform.localScale.x) / base.transform.localScale.x;
					float y = currentScenePos.y - currentScene.GetComponent<SpriteRenderer>().sprite.rect.size.y / 100f / 2f + (doorY + doorOriginOffsetY) / doorSceneHeight * (currentScene.GetComponent<SpriteRenderer>().sprite.rect.size.y / 100f * base.transform.localScale.y) / base.transform.localScale.y;
					shadeMarker.transform.localPosition = new Vector3(x, y, 0f);
				}
				pd.SetVector3SwappedArgs(new Vector3(currentScenePos.x, currentScenePos.y, 0f), "shadeMapPos");
			}
			if (posGate)
			{
				dreamGateMarker.transform.localPosition = new Vector3(currentScenePos.x, currentScenePos.y, 0f);
				pd.SetVector3SwappedArgs(new Vector3(currentScenePos.x, currentScenePos.y, 0f), "dreamgateMapPos");
			}
		}
		else
		{
			Debug.Log("Couldn't find current scene object!");
			if (posShade)
			{
				pd.SetVector3SwappedArgs(new Vector3(-10000f, -10000f, 0f), "shadeMapPos");
				shadeMarker.transform.localPosition = pd.GetVector3("shadeMapPos");
			}
		}
	}

	private void Update()
	{
		if (displayingCompass)
		{
			Vector2 vector = currentScene.GetComponent<SpriteRenderer>().sprite.bounds.size;
			if (!inRoom)
			{
				float x = currentScenePos.x - vector.x / 2f + (hero.transform.position.x + originOffsetX) / sceneWidth * (vector.x * base.transform.localScale.x) / base.transform.localScale.x;
				float y = currentScenePos.y - vector.y / 2f + (hero.transform.position.y + originOffsetY) / sceneHeight * (vector.y * base.transform.localScale.y) / base.transform.localScale.y;
				compassIcon.transform.localPosition = new Vector3(x, y, -1f);
			}
			else
			{
				float x = currentScenePos.x - vector.x / 2f + (doorX + doorOriginOffsetX) / doorSceneWidth * (vector.x * base.transform.localScale.x) / base.transform.localScale.x;
				float y = currentScenePos.y - vector.y / 2f + (doorY + doorOriginOffsetY) / doorSceneHeight * (vector.y * base.transform.localScale.y) / base.transform.localScale.y;
				compassIcon.transform.localPosition = new Vector3(x, y, -1f);
				displayingCompass = false;
			}
			if (!compassIcon.activeSelf)
			{
				compassIcon.SetActive(value: true);
			}
		}
		if (canPan)
		{
			if (inputHandler.inputActions.rs_down.IsPressed)
			{
				base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + panSpeed * 2f * Time.deltaTime, base.transform.position.z);
			}
			else if (inputHandler.inputActions.down.IsPressed)
			{
				base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + panSpeed * Time.deltaTime, base.transform.position.z);
			}
			if (inputHandler.inputActions.rs_up.IsPressed)
			{
				base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y - panSpeed * 2f * Time.deltaTime, base.transform.position.z);
			}
			else if (inputHandler.inputActions.up.IsPressed)
			{
				base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y - panSpeed * Time.deltaTime, base.transform.position.z);
			}
			if (inputHandler.inputActions.rs_left.IsPressed)
			{
				base.transform.position = new Vector3(base.transform.position.x + panSpeed * 2f * Time.deltaTime, base.transform.position.y, base.transform.position.z);
			}
			else if (inputHandler.inputActions.left.IsPressed)
			{
				base.transform.position = new Vector3(base.transform.position.x + panSpeed * Time.deltaTime, base.transform.position.y, base.transform.position.z);
			}
			if (inputHandler.inputActions.rs_right.IsPressed)
			{
				base.transform.position = new Vector3(base.transform.position.x - panSpeed * 2f * Time.deltaTime, base.transform.position.y, base.transform.position.z);
			}
			else if (inputHandler.inputActions.right.IsPressed)
			{
				base.transform.position = new Vector3(base.transform.position.x - panSpeed * Time.deltaTime, base.transform.position.y, base.transform.position.z);
			}
			KeepWithinBounds();
			if (base.transform.position.x == panMinX && panArrowR.activeSelf)
			{
				panArrowR.SetActive(value: false);
			}
			else if (base.transform.position.x > panMinX && !panArrowR.activeSelf)
			{
				panArrowR.SetActive(value: true);
			}
			if (base.transform.position.x == panMaxX && panArrowL.activeSelf)
			{
				panArrowL.SetActive(value: false);
			}
			else if (base.transform.position.x < panMaxX && !panArrowL.activeSelf)
			{
				panArrowL.SetActive(value: true);
			}
			if (base.transform.position.y == panMinY && panArrowU.activeSelf)
			{
				panArrowU.SetActive(value: false);
			}
			else if (base.transform.position.y > panMinY && !panArrowU.activeSelf)
			{
				panArrowU.SetActive(value: true);
			}
			if (base.transform.position.y == panMaxY && panArrowD.activeSelf)
			{
				panArrowD.SetActive(value: false);
			}
			else if (base.transform.position.y < panMaxY && !panArrowD.activeSelf)
			{
				panArrowD.SetActive(value: true);
			}
		}
	}

	private void DisableMarkers()
	{
		for (int i = 0; i < mapMarkersBlue.Length; i++)
		{
			mapMarkersBlue[i].SetActive(value: false);
		}
		for (int j = 0; j < mapMarkersRed.Length; j++)
		{
			mapMarkersRed[j].SetActive(value: false);
		}
		for (int k = 0; k < mapMarkersYellow.Length; k++)
		{
			mapMarkersYellow[k].SetActive(value: false);
		}
		for (int l = 0; l < mapMarkersWhite.Length; l++)
		{
			mapMarkersWhite[l].SetActive(value: false);
		}
	}

	public void SetManualTilemap(float offsetX, float offsetY, float width, float height)
	{
		originOffsetX = offsetX;
		originOffsetY = offsetY;
		sceneWidth = width;
		sceneHeight = height;
	}

	public void SetDoorValues(float x, float y, string scene, string mapZone)
	{
		doorX = x;
		doorY = y;
		doorScene = scene;
		doorMapZone = mapZone;
		doorOriginOffsetX = originOffsetX;
		doorOriginOffsetY = originOffsetY;
		doorSceneWidth = sceneWidth;
		doorSceneHeight = sceneHeight;
	}

	public void SetCustomCompassPos(float x, float y, string scene, string mapZone, float offsetX, float offsetY, float width, float height)
	{
		inRoom = true;
		doorX = x;
		doorY = y;
		doorScene = scene;
		doorMapZone = mapZone;
		doorOriginOffsetX = offsetX;
		doorOriginOffsetY = offsetY;
		doorSceneWidth = width;
		doorSceneHeight = height;
	}

	public void SetInRoom(bool room)
	{
		inRoom = room;
	}

	public void SetCanPan(bool pan)
	{
		canPan = pan;
	}

	public string GetDoorMapZone()
	{
		return doorMapZone;
	}

	public bool GetInRoom()
	{
		return inRoom;
	}

	public void SetPanArrows(GameObject arrowU, GameObject arrowD, GameObject arrowL, GameObject arrowR)
	{
		panArrowU = arrowU;
		panArrowD = arrowD;
		panArrowL = arrowL;
		panArrowR = arrowR;
	}

	public void KeepWithinBounds()
	{
		if (base.transform.position.x < panMinX)
		{
			base.transform.position = new Vector3(panMinX, base.transform.position.y, base.transform.position.z);
		}
		if (base.transform.position.x > panMaxX)
		{
			base.transform.position = new Vector3(panMaxX, base.transform.position.y, base.transform.position.z);
		}
		if (base.transform.position.y < panMinY)
		{
			base.transform.position = new Vector3(base.transform.position.x, panMinY, base.transform.position.z);
		}
		if (base.transform.position.y > panMaxY)
		{
			base.transform.position = new Vector3(base.transform.position.x, panMaxY, base.transform.position.z);
		}
	}

	public void StopPan()
	{
		canPan = false;
		panArrowU.SetActive(value: false);
		panArrowL.SetActive(value: false);
		panArrowR.SetActive(value: false);
		panArrowD.SetActive(value: false);
	}

	public void StartPan()
	{
		canPan = true;
	}

	public void SetupMapMarkers()
	{
		DisableMarkers();
		for (int i = 0; i < pd.GetVariable<List<Vector3>>("placedMarkers_b").Count; i++)
		{
			mapMarkersBlue[i].SetActive(value: true);
			mapMarkersBlue[i].transform.localPosition = pd.GetVariable<List<Vector3>>("placedMarkers_b")[i];
		}
		for (int j = 0; j < pd.GetVariable<List<Vector3>>("placedMarkers_r").Count; j++)
		{
			mapMarkersRed[j].SetActive(value: true);
			mapMarkersRed[j].transform.localPosition = pd.GetVariable<List<Vector3>>("placedMarkers_r")[j];
		}
		for (int k = 0; k < pd.GetVariable<List<Vector3>>("placedMarkers_y").Count; k++)
		{
			mapMarkersYellow[k].SetActive(value: true);
			mapMarkersYellow[k].transform.localPosition = pd.GetVariable<List<Vector3>>("placedMarkers_y")[k];
		}
		for (int l = 0; l < pd.GetVariable<List<Vector3>>("placedMarkers_w").Count; l++)
		{
			mapMarkersWhite[l].SetActive(value: true);
			mapMarkersWhite[l].transform.localPosition = pd.GetVariable<List<Vector3>>("placedMarkers_w")[l];
		}
	}
}
