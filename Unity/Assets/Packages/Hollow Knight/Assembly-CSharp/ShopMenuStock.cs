using System.Collections.Generic;
using UnityEngine;

public class ShopMenuStock : MonoBehaviour
{
	public GameObject masterList;

	public GameObject[] stock;

	public GameObject[] stockInv;

	public GameObject[] stockAlt;

	public string altPlayerDataBool;

	public string altPlayerDataBoolAlt;

	private Dictionary<GameObject, GameObject> spawnedStock;

	public float yDistance = -1.5f;

	private Vector3 selfPos;

	public int itemCount = -1;

	private void Start()
	{
		PlayerData instance = PlayerData.instance;
		if (altPlayerDataBool != "" && (instance.GetBool(altPlayerDataBool) || (altPlayerDataBoolAlt != "" && instance.GetBool(altPlayerDataBoolAlt))))
		{
			stock = stockAlt;
		}
		SpawnStock();
	}

	private void SpawnStock()
	{
		if ((bool)masterList)
		{
			ShopMenuStock component = masterList.GetComponent<ShopMenuStock>();
			if ((bool)component)
			{
				spawnedStock = component.spawnedStock;
			}
		}
		if (spawnedStock == null && stock.Length != 0)
		{
			spawnedStock = new Dictionary<GameObject, GameObject>(stock.Length);
			GameObject[] array = stock;
			foreach (GameObject gameObject in array)
			{
				GameObject gameObject2 = Object.Instantiate(gameObject);
				gameObject2.SetActive(value: false);
				spawnedStock.Add(gameObject, gameObject2);
			}
		}
	}

	public void UpdateStock()
	{
		PlayerData instance = PlayerData.instance;
		if (altPlayerDataBool != "" && instance.GetBool(altPlayerDataBool))
		{
			stock = stockAlt;
		}
	}

	private void BuildFromMasterList()
	{
		masterList = base.transform.parent.gameObject;
		stock = masterList.GetComponent<ShopMenuStock>().stock;
	}

	public bool StockLeft()
	{
		PlayerData instance = PlayerData.instance;
		bool result = false;
		if (instance != null)
		{
			for (int i = 0; i < stock.Length; i++)
			{
				if (stock[i].GetComponent<ShopItemStats>().requiredPlayerDataBool == "" || stock[i].GetComponent<ShopItemStats>().requiredPlayerDataBool == "Null" || stock[i].GetComponent<ShopItemStats>().requiredPlayerDataBool == null)
				{
					if (!instance.GetBool(stock[i].GetComponent<ShopItemStats>().GetPlayerDataBoolName()) && (stock[i].GetComponent<ShopItemStats>().removalPlayerDataBool == "" || !instance.GetBool(stock[i].GetComponent<ShopItemStats>().GetRemovalPlayerDataBoolName())))
					{
						result = true;
					}
				}
				else if (!instance.GetBool(stock[i].GetComponent<ShopItemStats>().GetPlayerDataBoolName()) && instance.GetBool(stock[i].GetComponent<ShopItemStats>().GetRequiredPlayerDataBoolName()) && (stock[i].GetComponent<ShopItemStats>().removalPlayerDataBool == "" || !instance.GetBool(stock[i].GetComponent<ShopItemStats>().GetRemovalPlayerDataBoolName())))
				{
					result = true;
				}
			}
		}
		return result;
	}

	private void BuildItemList()
	{
		PlayerData instance = PlayerData.instance;
		if (spawnedStock == null)
		{
			SpawnStock();
		}
		itemCount = -1;
		float num = 0f;
		stockInv = new GameObject[stock.Length];
		for (int i = 0; i < stock.Length; i++)
		{
			if (stock[i].GetComponent<ShopItemStats>().requiredPlayerDataBool == "" || stock[i].GetComponent<ShopItemStats>().requiredPlayerDataBool == "Null" || stock[i].GetComponent<ShopItemStats>().requiredPlayerDataBool == null)
			{
				if ((!instance.GetBool(stock[i].GetComponent<ShopItemStats>().GetPlayerDataBoolName()) || stock[i].GetComponent<ShopItemStats>().playerDataBoolName == "" || stock[i].GetComponent<ShopItemStats>().playerDataBoolName == "Null" || stock[i].GetComponent<ShopItemStats>().playerDataBoolName == null) && (stock[i].GetComponent<ShopItemStats>().removalPlayerDataBool == "" || !instance.GetBool(stock[i].GetComponent<ShopItemStats>().GetRemovalPlayerDataBoolName())))
				{
					itemCount++;
					GameObject gameObject = spawnedStock[stock[i]];
					gameObject.transform.SetParent(base.transform, worldPositionStays: false);
					gameObject.transform.localPosition = new Vector3(0f, num, 0f);
					gameObject.GetComponent<ShopItemStats>().itemNumber = itemCount;
					stockInv[itemCount] = gameObject;
					num += yDistance;
					gameObject.SetActive(value: true);
				}
			}
			else if ((!instance.GetBool(stock[i].GetComponent<ShopItemStats>().GetPlayerDataBoolName()) || stock[i].GetComponent<ShopItemStats>().playerDataBoolName == "" || stock[i].GetComponent<ShopItemStats>().playerDataBoolName == "Null" || stock[i].GetComponent<ShopItemStats>().playerDataBoolName == null) && instance.GetBool(stock[i].GetComponent<ShopItemStats>().GetRequiredPlayerDataBoolName()) && (stock[i].GetComponent<ShopItemStats>().removalPlayerDataBool == "" || !instance.GetBool(stock[i].GetComponent<ShopItemStats>().GetRemovalPlayerDataBoolName())))
			{
				itemCount++;
				GameObject gameObject = spawnedStock[stock[i]];
				gameObject.transform.SetParent(base.transform, worldPositionStays: false);
				gameObject.transform.localPosition = new Vector3(0f, num, 0f);
				gameObject.GetComponent<ShopItemStats>().itemNumber = itemCount;
				stockInv[itemCount] = gameObject;
				num += yDistance;
				gameObject.SetActive(value: true);
			}
		}
	}

	public int GetItemCount()
	{
		return itemCount;
	}

	public int GetCost(int itemNum)
	{
		return stockInv[itemNum].GetComponent<ShopItemStats>().GetCost();
	}

	public int GetNotchCost(int itemNum)
	{
		return stockInv[itemNum].GetComponent<ShopItemStats>().GetNotchCost();
	}

	public string GetNameConvo(int itemNum)
	{
		return stockInv[itemNum].GetComponent<ShopItemStats>().GetNameConvo();
	}

	public string GetDescConvo(int itemNum)
	{
		return stockInv[itemNum].GetComponent<ShopItemStats>().GetDescConvo();
	}

	public string GetPlayerDataBoolName(int itemNum)
	{
		return stockInv[itemNum].GetComponent<ShopItemStats>().GetPlayerDataBoolName();
	}

	public int GetSpecialType(int itemNum)
	{
		return stockInv[itemNum].GetComponent<ShopItemStats>().GetSpecialType();
	}

	public int GetRelicNumber(int itemNum)
	{
		return stockInv[itemNum].GetComponent<ShopItemStats>().GetRelicNumber();
	}

	public int GetCharmsRequired(int itemNum)
	{
		return stockInv[itemNum].GetComponent<ShopItemStats>().GetCharmsRequired();
	}

	public float GetYDistance()
	{
		return yDistance;
	}

	public Sprite GetItemSprite(int itemNum)
	{
		return stockInv[itemNum].transform.Find("Item Sprite").gameObject.GetComponent<SpriteRenderer>().sprite;
	}

	public Vector3 GetItemSpriteScale(int itemNum)
	{
		return stockInv[itemNum].transform.Find("Item Sprite").gameObject.transform.localScale;
	}

	public bool CanBuy(int itemNum)
	{
		PlayerData instance = PlayerData.instance;
		bool result = stockInv[itemNum].GetComponent<ShopItemStats>().relicNumber > 0 || ((instance.GetInt("geo") >= stockInv[itemNum].GetComponent<ShopItemStats>().GetCost()) ? true : false);
		if (instance.GetInt("charmsOwned") < stockInv[itemNum].GetComponent<ShopItemStats>().charmsRequired)
		{
			result = false;
		}
		return result;
	}

	public GameObject GetItemGameObject(int itemNum)
	{
		return stockInv[itemNum];
	}
}
