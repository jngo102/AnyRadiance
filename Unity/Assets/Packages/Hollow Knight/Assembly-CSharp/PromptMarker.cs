using System.Collections;
using UnityEngine;

public class PromptMarker : MonoBehaviour
{
	public GameObject labels;

	private FadeGroup fadeGroup;

	private tk2dSpriteAnimator anim;

	private GameObject owner;

	private bool isVisible;

	private void Awake()
	{
		anim = GetComponent<tk2dSpriteAnimator>();
		if ((bool)labels)
		{
			fadeGroup = labels.GetComponent<FadeGroup>();
		}
	}

	private void Start()
	{
		if ((bool)GameManager.instance)
		{
			GameManager.instance.UnloadingLevel += RecycleOnLevelLoad;
		}
	}

	private void OnDestroy()
	{
		if ((bool)GameManager.instance)
		{
			GameManager.instance.UnloadingLevel -= RecycleOnLevelLoad;
		}
	}

	private void RecycleOnLevelLoad()
	{
		if (base.gameObject.activeSelf)
		{
			base.gameObject.Recycle();
		}
	}

	private void OnEnable()
	{
		anim.Play("Blank");
	}

	private void Update()
	{
		if (isVisible && (!owner || !owner.activeInHierarchy))
		{
			Hide();
		}
	}

	public void SetLabel(string labelName)
	{
		if (!labels)
		{
			return;
		}
		foreach (Transform item in labels.transform)
		{
			item.gameObject.SetActive(item.name == labelName);
		}
	}

	public void Show()
	{
		anim.Play("Up");
		base.transform.SetPositionZ(0f);
		fadeGroup.FadeUp();
		isVisible = true;
	}

	public void Hide()
	{
		anim.Play("Down");
		fadeGroup.FadeDown();
		owner = null;
		StartCoroutine(RecycleDelayed(fadeGroup.fadeOutTime));
		isVisible = false;
	}

	private IEnumerator RecycleDelayed(float delay)
	{
		yield return new WaitForSeconds(delay);
		gameObject.Recycle();
	}

	public void SetOwner(GameObject obj)
	{
		owner = obj;
	}
}
