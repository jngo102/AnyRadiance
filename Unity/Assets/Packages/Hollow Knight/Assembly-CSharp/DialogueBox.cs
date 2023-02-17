using System.Collections;
using Language;
using TMPro;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
	[Header("Conversation Info")]
	public string currentConversation;

	public int currentPage;

	[Header("Typewriter")]
	[Tooltip("Enables the typewriter effect.")]
	public bool useTypeWriter;

	[Range(1f, 100f)]
	public float revealSpeed = 20f;

	private float normalRevealSpeed;

	private TextMeshPro textMesh;

	private PlayMakerFSM proxyFSM;

	private bool typing;

	private bool fastTyping;

	private bool hidden;

	private TMP_PageInfo[] pageInfo;

	private void Start()
	{
		textMesh = base.gameObject.GetComponent<TextMeshPro>();
		normalRevealSpeed = revealSpeed;
		HideText();
		proxyFSM = FSMUtility.LocateFSM(base.gameObject, "Dialogue Page Control");
		if (proxyFSM == null)
		{
			Debug.LogWarning("DialogueBox: Couldn't find an FSM on this GameObject to use as a proxy, events will not be fired from this dialogue box.");
		}
	}

	public void SetConversation(string convName, string sheetName)
	{
		currentConversation = convName;
		currentPage = 1;
		textMesh.text = global::Language.Language.Get(convName, sheetName);
		textMesh.ForceMeshUpdate();
	}

	public void ShowPage(int pageNum)
	{
		if (pageNum < 1 || pageNum > textMesh.textInfo.pageCount)
		{
			SendConvEndEvent();
			return;
		}
		if (hidden)
		{
			hidden = false;
		}
		if (useTypeWriter)
		{
			if (typing)
			{
				StopTypewriter();
			}
			textMesh.pageToDisplay = pageNum;
			currentPage = pageNum;
			textMesh.maxVisibleCharacters = GetFirstCharIndexOnPage() - 1;
			string text = textMesh.text;
			text = text.Replace("<br>", "\n");
			textMesh.text = text;
			StartCoroutine("TypewriteCurrentPage");
		}
		else
		{
			textMesh.pageToDisplay = pageNum;
			currentPage = pageNum;
			textMesh.maxVisibleCharacters = GetLastCharIndexOnPage();
			SendEndEvent();
		}
	}

	public void ShowNextPage()
	{
		if (textMesh.pageToDisplay < textMesh.textInfo.pageCount)
		{
			ShowPage(currentPage + 1);
		}
	}

	public void ShowPrevPage()
	{
		if (textMesh.pageToDisplay > 1)
		{
			ShowPage(currentPage - 1);
		}
	}

	public void HideText()
	{
		if (typing)
		{
			StopTypewriter();
		}
		textMesh.maxVisibleCharacters = 0;
		hidden = true;
	}

	public void StartConversation(string convName, string sheetName)
	{
		SetConversation(convName, sheetName);
		ShowPage(1);
	}

	private IEnumerator TypewriteCurrentPage()
	{
		if (!typing)
		{
			InvokeRepeating("ShowNextChar", 0f, 1f / revealSpeed);
			typing = true;
		}
		while (typing)
		{
			if (textMesh.maxVisibleCharacters >= GetLastCharIndexOnPage())
			{
				StopTypewriter();
				SendEndEvent();
			}
			else
			{
				yield return null;
			}
		}
	}

	private void ShowNextChar()
	{
		textMesh.maxVisibleCharacters++;
	}

	private void StopTypewriter()
	{
		CancelInvoke("ShowNextChar");
		typing = false;
		fastTyping = false;
		revealSpeed = normalRevealSpeed;
	}

	public void SpeedupTypewriter()
	{
		if (typing && !fastTyping)
		{
			StopTypewriter();
			normalRevealSpeed = revealSpeed;
			revealSpeed = 200f;
			fastTyping = true;
			StartCoroutine(TypewriteCurrentPage());
		}
	}

	private void RestoreTypewriter()
	{
		revealSpeed = normalRevealSpeed;
	}

	private void SendEndEvent()
	{
		if (currentPage == textMesh.textInfo.pageCount)
		{
			SendConvEndEvent();
		}
		else
		{
			SendPageEndEvent();
		}
	}

	private void SendPageEndEvent()
	{
		if (proxyFSM != null)
		{
			proxyFSM.SendEvent("PAGE_END");
		}
	}

	private void SendConvEndEvent()
	{
		if (proxyFSM != null)
		{
			proxyFSM.SendEvent("CONVERSATION_END");
		}
	}

	private int GetFirstCharIndexOnPage()
	{
		return textMesh.textInfo.pageInfo[currentPage - 1].firstCharacterIndex + 1;
	}

	private int GetLastCharIndexOnPage()
	{
		return textMesh.textInfo.pageInfo[currentPage - 1].lastCharacterIndex + 1;
	}

	public void PrintPageInfo()
	{
		Debug.LogFormat("PageInfo: Current Page: {0} Start: {1} End {2}", currentPage, GetFirstCharIndexOnPage(), GetLastCharIndexOnPage());
	}

	public void PrintPageInfoAll()
	{
		Debug.LogFormat("PageInfo: Current conversation {0} contains {1} pages.\n", currentConversation, textMesh.textInfo.pageCount);
		for (int i = 0; i < textMesh.textInfo.pageCount; i++)
		{
			Debug.LogFormat("[Page {0}] Start/End: {1}/{2}\n", i + 1, textMesh.textInfo.pageInfo[i].firstCharacterIndex, textMesh.textInfo.pageInfo[i].lastCharacterIndex);
		}
	}

	public void PrintCurrentConversation()
	{
		Debug.LogFormat("Current conversation set to {0}\nClick this message to see the dialogue for this conversation.\n{1}", currentConversation, textMesh.text);
	}
}
