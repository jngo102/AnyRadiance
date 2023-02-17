using Language;
using TMPro;
using UnityEngine;

public class ShopItemStats : MonoBehaviour
{
	public string playerDataBoolName;

	public string nameConvo;

	public string descConvo;

	public string priceConvo;

	public string requiredPlayerDataBool;

	public string removalPlayerDataBool;

	[Tooltip("0 = None, 1 = Heart Piece, 2 = Charm, 3 = Soul Piece, 4 = Relic1,  5 = Relic2, 6 = Relic3, 7 = Relic4, 8 = Notch, 9 = Map, 10 = Simple Key, 11 = Rancid Egg, 12 = Repair Glass HP, 13 = Repair Glass Geo, 14 = Repair Glass Attack, 15 = Salubra's Blessing, 16 = Map Pin, 17 = Map Marker")]
	public int specialType;

	public int relicNumber;

	public int charmsRequired;

	public Color activeColour;

	public Color inactiveColour;

	public bool dungDiscount;

	public bool relic;

	public string relicPDInt;

	[Header("Charms Only")]
	public string notchCostBool;

	[Header("Don't need to enter below variables!")]
	public int cost;

	private int runningCost;

	public int itemNumber;

	public bool canBuy;

	private PlayerData playerData;

	private int notchCost;

	private float topY = 4.9f;

	private float botY = -5.25f;

	private bool hidden = true;

	private PlayerData pd;

	private GameObject geoSprite;

	private GameObject itemSprite;

	private GameObject itemCost;

	private void Awake()
	{
		if (pd == null)
		{
			pd = PlayerData.instance;
		}
		string text = global::Language.Language.Get(priceConvo, "Prices");
		try
		{
			cost = int.Parse(text);
		}
		catch
		{
			Debug.LogError("Input string \"" + text + "\"could not be parsed to int");
		}
		if (specialType == 2)
		{
			playerData = PlayerData.instance;
			notchCost = playerData.GetInt(notchCostBool);
		}
		geoSprite = base.transform.Find("Geo Sprite").gameObject;
		itemSprite = base.transform.Find("Item Sprite").gameObject;
		itemCost = base.transform.Find("Item cost").gameObject;
	}

	private void OnEnable()
	{
		runningCost = cost;
		if (dungDiscount && pd.GetBool("equippedCharm_10"))
		{
			runningCost = (int)((float)cost * 0.8f);
		}
		itemCost.GetComponent<TextMeshPro>().text = runningCost.ToString();
		foreach (Transform item in base.transform)
		{
			item.gameObject.SetActive(value: false);
		}
		hidden = true;
		if (relic)
		{
			SetCanBuy(can: true);
			string text = pd.GetInt(relicPDInt).ToString();
			base.transform.Find("Amount").gameObject.GetComponent<TextMeshPro>().text = text;
		}
		else if (pd.GetInt("geo") >= runningCost && pd.GetInt("charmsOwned") >= charmsRequired)
		{
			geoSprite.GetComponent<SpriteRenderer>().color = activeColour;
			itemSprite.GetComponent<SpriteRenderer>().color = activeColour;
			itemCost.GetComponent<TextMeshPro>().color = activeColour;
		}
		else
		{
			geoSprite.GetComponent<SpriteRenderer>().color = inactiveColour;
			itemSprite.GetComponent<SpriteRenderer>().color = inactiveColour;
			itemCost.GetComponent<TextMeshPro>().color = inactiveColour;
		}
	}

	private void Update()
	{
		float y = base.transform.position.y;
		if (y > topY || y < botY)
		{
			if (hidden)
			{
				return;
			}
			foreach (Transform item in base.transform)
			{
				item.gameObject.SetActive(value: false);
			}
			hidden = true;
		}
		else
		{
			if (!hidden)
			{
				return;
			}
			foreach (Transform item2 in base.transform)
			{
				item2.gameObject.SetActive(value: true);
			}
			hidden = false;
		}
	}

	public int GetCost()
	{
		return runningCost;
	}

	public int GetNotchCost()
	{
		if (notchCost == 0 && specialType == 2)
		{
			playerData = PlayerData.instance;
			notchCost = playerData.GetInt(notchCostBool);
		}
		return notchCost;
	}

	public int GetCharmsRequired()
	{
		return charmsRequired;
	}

	public int GetRelicNumber()
	{
		return relicNumber;
	}

	public string GetNameConvo()
	{
		return nameConvo;
	}

	public string GetDescConvo()
	{
		return descConvo;
	}

	public string GetPlayerDataBoolName()
	{
		return playerDataBoolName;
	}

	public string GetRequiredPlayerDataBoolName()
	{
		return requiredPlayerDataBool;
	}

	public string GetRemovalPlayerDataBoolName()
	{
		return removalPlayerDataBool;
	}

	public int GetItemNumber()
	{
		return itemNumber;
	}

	public int GetSpecialType()
	{
		return specialType;
	}

	public bool CanBuy()
	{
		return canBuy;
	}

	public void SetCanBuy(bool can)
	{
		canBuy = can;
	}

	public void SetDescConvo(string convo)
	{
		descConvo = convo;
	}

	public void SetCost(int newCost)
	{
		cost = newCost;
		runningCost = cost;
	}
}
