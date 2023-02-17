using UnityEngine;

public class CreditsHelper : MonoBehaviour
{
	public enum CreditsType
	{
		EndCredits,
		MenuCredits
	}

	public CreditsType creditsType;

	public Animator creditsAnimator;

	public CutsceneHelper cutSceneHelper;

	private void Start()
	{
		if (creditsType == CreditsType.MenuCredits)
		{
			Invoke("BeginCredits", 0f);
			return;
		}
		Invoke("BeginCredits", 4f);
		GameObject gameObject = GameObject.Find("Knight");
		if ((bool)gameObject)
		{
			Rigidbody2D component = gameObject.GetComponent<Rigidbody2D>();
			if ((bool)component)
			{
				component.gravityScale = 0f;
				component.velocity = Vector2.zero;
			}
		}
	}

	private void BeginCredits()
	{
		creditsAnimator.SetTrigger("BeginCredits");
	}

	public void ShouldStopHere()
	{
		if (creditsType == CreditsType.MenuCredits)
		{
			StartCoroutine(cutSceneHelper.SkipCutscene());
		}
		else
		{
			creditsAnimator.SetTrigger("ShowThanks");
		}
	}
}
