using UnityEngine;

public class VisibilityControl : MonoBehaviour
{
	public enum ControlType
	{
		SHOW_AND_HIDE,
		HIDE_ONLY
	}

	private Animator myAnimator;

	public ControlType controlType;

	private void Awake()
	{
		myAnimator = GetComponent<Animator>();
		if (myAnimator == null)
		{
			Debug.Log("VisibilityControl: This UI object does not have an animator component attached. Attach an animator or remove the VisibilityControl component.");
		}
	}

	public void Reveal()
	{
		if (controlType == ControlType.SHOW_AND_HIDE)
		{
			myAnimator.ResetTrigger("hide");
			myAnimator.SetTrigger("show");
		}
	}

	public void Hide()
	{
		myAnimator.ResetTrigger("show");
		myAnimator.SetTrigger("hide");
	}
}
