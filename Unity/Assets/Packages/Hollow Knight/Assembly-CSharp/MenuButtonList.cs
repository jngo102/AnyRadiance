// MenuButtonList
using System;
using System.Collections;
using System.Collections.Generic;
using GlobalEnums;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuButtonList : MonoBehaviour
{
	[Serializable]
	private class Entry
	{
		[SerializeField]
		[FormerlySerializedAs("button")]
		private Selectable selectable;

		[SerializeField]
		private MenuButtonListCondition condition;

		[SerializeField]
		private bool alsoAffectParent;

		public Selectable Selectable => selectable;

		public MenuButtonListCondition Condition => condition;

		public bool AlsoAffectParent => alsoAffectParent;
	}

	[SerializeField]
	private Entry[] entries;

	[SerializeField]
	private bool isTopLevelMenu;

	[SerializeField]
	private bool skipDisabled;

	private MenuSelectable lastSelected;

	private List<Selectable> activeSelectables;

	private static List<MenuButtonList> menuButtonLists = new List<MenuButtonList>();

	private bool started;

	private void Awake()
	{
		MenuScreen component = GetComponent<MenuScreen>();
		if ((bool)component)
		{
			component.defaultHighlight = null;
		}
	}

	protected void Start()
	{
		menuButtonLists.Add(this);
		activeSelectables = new List<Selectable>();
		for (int i = 0; i < entries.Length; i++)
		{
			Entry entry = entries[i];
			Selectable selectable = entry.Selectable;
			MenuButtonListCondition condition = entry.Condition;
			if (condition == null || condition.IsFulfilled())
			{
				if (!skipDisabled)
				{
					selectable.gameObject.SetActive(value: true);
					if (entry.AlsoAffectParent)
					{
						selectable.transform.parent.gameObject.SetActive(value: true);
						selectable.interactable = true;
					}
				}
				if (!skipDisabled || (selectable.gameObject.activeInHierarchy && selectable.interactable))
				{
					activeSelectables.Add(selectable);
				}
			}
			else
			{
				selectable.gameObject.SetActive(value: false);
				if (entry.AlsoAffectParent)
				{
					selectable.transform.parent.gameObject.SetActive(value: false);
					selectable.interactable = false;
				}
			}
		}
		for (int j = 0; j < activeSelectables.Count; j++)
		{
			Selectable selectable2 = activeSelectables[j];
			Selectable selectOnUp = activeSelectables[(j + activeSelectables.Count - 1) % activeSelectables.Count];
			Selectable selectOnDown = activeSelectables[(j + 1) % activeSelectables.Count];
			Navigation navigation = selectable2.navigation;
			if (navigation.mode == Navigation.Mode.Explicit)
			{
				navigation.selectOnUp = selectOnUp;
				navigation.selectOnDown = selectOnDown;
				selectable2.navigation = navigation;
			}
			if (isTopLevelMenu)
			{
				CancelAction cancelAction = (Platform.Current.WillDisplayQuitButton ? CancelAction.GoToExitPrompt : CancelAction.DoNothing);
				MenuButton menuButton = selectable2 as MenuButton;
				if (menuButton != null)
				{
					menuButton.cancelAction = cancelAction;
				}
			}
		}
		foreach (MenuSelectable menuSelectable in activeSelectables)
		{
			menuSelectable.OnSelected += delegate (MenuSelectable self)
			{
				if (isTopLevelMenu || menuSelectable != (MenuSelectable)activeSelectables[activeSelectables.Count - 1])
				{
					lastSelected = self;
				}
			};
		}
		DoSelect();
		started = true;
	}

	private void OnEnable()
	{
		if (started)
		{
			DoSelect();
		}
	}

	private void DoSelect()
	{
		if ((bool)lastSelected)
		{
			StartCoroutine(SelectDelayed(lastSelected));
		}
		else if (activeSelectables != null && activeSelectables.Count > 0)
		{
			StartCoroutine(SelectDelayed(activeSelectables[0].GetFirstInteractable()));
		}
	}

	private void OnDestroy()
	{
		menuButtonLists.Remove(this);
	}

	private IEnumerator SelectDelayed(Selectable selectable)
	{
		while (!selectable.gameObject.activeInHierarchy)
		{
			yield return null;
		}
		if (selectable is MenuSelectable)
		{
			((MenuSelectable)selectable).DontPlaySelectSound = true;
		}
		selectable.Select();
		if (selectable is MenuSelectable)
		{
			((MenuSelectable)selectable).DontPlaySelectSound = false;
		}
		Animator[] componentsInChildren = selectable.GetComponentsInChildren<Animator>();
		foreach (Animator animator in componentsInChildren)
		{
			if (animator.HasParameter("hide"))
			{
				animator.ResetTrigger("hide");
			}
			if (animator.HasParameter("show"))
			{
				animator.SetTrigger("show");
			}
		}
	}

	public void ClearLastSelected()
	{
		lastSelected = null;
	}

	public static void ClearAllLastSelected()
	{
		foreach (MenuButtonList menuButtonList in menuButtonLists)
		{
			menuButtonList.ClearLastSelected();
		}
	}
}
