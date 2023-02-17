using UnityEngine;

public class JournalListOld : MonoBehaviour
{
	public GameObject[] list;

	public GameObject[] listInv;

	public PlayerData pd;

	public float yDistance = -1.5f;

	private Vector3 selfPos;

	public int itemCount = -1;

	public int firstNewItem;

	private void Start()
	{
	}

	private void BuildItemList()
	{
		Debug.Log("build item list");
		firstNewItem = -1;
		itemCount = -1;
		pd = PlayerData.instance;
		float num = 0f;
		listInv = new GameObject[list.Length];
		for (int i = 0; i < list.Length; i++)
		{
			if (pd.GetBool(list[i].GetComponent<JournalEntryStats>().GetPlayerDataBoolName()) || pd.GetBool("fillJournal"))
			{
				itemCount++;
				GameObject gameObject = Object.Instantiate(list[i]);
				gameObject.transform.position = new Vector3(0f, num, 0f);
				gameObject.transform.SetParent(base.transform, worldPositionStays: false);
				gameObject.GetComponent<JournalEntryStats>().itemNumber = itemCount;
				listInv[itemCount] = gameObject;
				num += yDistance;
				if (pd.GetBool(list[i].GetComponent<JournalEntryStats>().GetPlayerDataNewDataName()) && firstNewItem == -1)
				{
					firstNewItem = itemCount;
				}
			}
		}
	}

	public int GetItemCount()
	{
		return itemCount;
	}

	public string GetNameConvo(int itemNum)
	{
		return listInv[itemNum].GetComponent<JournalEntryStats>().GetNameConvo();
	}

	public string GetDescConvo(int itemNum)
	{
		return listInv[itemNum].GetComponent<JournalEntryStats>().GetDescConvo();
	}

	public bool GetWarriorGhost(int itemNum)
	{
		return listInv[itemNum].GetComponent<JournalEntryStats>().GetWarriorGhost();
	}

	public string GetNotesConvo(int itemNum)
	{
		return listInv[itemNum].GetComponent<JournalEntryStats>().GetNotesConvo();
	}

	public string GetPlayerDataBoolName(int itemNum)
	{
		return listInv[itemNum].GetComponent<JournalEntryStats>().GetPlayerDataBoolName();
	}

	public string GetPlayerDataKillsName(int itemNum)
	{
		return listInv[itemNum].GetComponent<JournalEntryStats>().GetPlayerDataKillsName();
	}

	public string GetPlayerDataNewDataName(int itemNum)
	{
		return listInv[itemNum].GetComponent<JournalEntryStats>().GetPlayerDataNewDataName();
	}

	public Sprite GetSprite(int itemNum)
	{
		return listInv[itemNum].GetComponent<JournalEntryStats>().GetSprite();
	}

	public float GetYDistance()
	{
		return yDistance;
	}

	public int GetFirstNewItem()
	{
		return firstNewItem;
	}
}
