using UnityEngine;

public class SetParticleScale : MonoBehaviour
{
	public bool grandParent;

	public bool greatGrandParent;

	private float parentXScale;

	private float selfXScale;

	private Vector3 scaleVector;

	private bool unparented;

	private GameObject parent;

	private void Start()
	{
		if (grandParent)
		{
			if (base.transform.parent != null && base.transform.parent.parent != null)
			{
				parent = base.transform.parent.gameObject.transform.parent.gameObject;
			}
		}
		else if (greatGrandParent)
		{
			if (base.transform.parent != null && base.transform.parent.parent != null && base.transform.parent.parent.parent != null)
			{
				parent = base.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject;
			}
		}
		else if (base.transform.parent != null)
		{
			parent = base.transform.parent.gameObject;
		}
	}

	private void Update()
	{
		if (parent != null && !unparented)
		{
			parentXScale = parent.transform.localScale.x;
			selfXScale = base.transform.localScale.x;
			if ((parentXScale < 0f && selfXScale > 0f) || (parentXScale > 0f && selfXScale < 0f))
			{
				scaleVector.Set(0f - base.transform.localScale.x, base.transform.localScale.y, base.transform.localScale.z);
				base.transform.localScale = scaleVector;
			}
		}
		else
		{
			unparented = true;
			selfXScale = base.transform.localScale.x;
			if (selfXScale < 0f)
			{
				scaleVector.Set(0f - base.transform.localScale.x, base.transform.localScale.y, base.transform.localScale.z);
				base.transform.localScale = scaleVector;
			}
		}
	}
}
