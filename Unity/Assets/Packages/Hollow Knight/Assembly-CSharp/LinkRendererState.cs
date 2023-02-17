using UnityEngine;

public class LinkRendererState : MonoBehaviour
{
	public Renderer parent;

	public Renderer child;

	private void Start()
	{
		UpdateLink();
	}

	private void Update()
	{
		UpdateLink();
	}

	private void UpdateLink()
	{
		if ((bool)parent && (bool)child && child.enabled != parent.enabled)
		{
			child.enabled = parent.enabled;
		}
	}
}
