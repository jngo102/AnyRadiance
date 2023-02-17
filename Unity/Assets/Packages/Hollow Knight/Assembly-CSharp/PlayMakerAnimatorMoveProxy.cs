using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayMakerFSM))]
public class PlayMakerAnimatorMoveProxy : MonoBehaviour
{
	public bool applyRootMotion;

	public event Action OnAnimatorMoveEvent;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnAnimatorMove()
	{
		if (this.OnAnimatorMoveEvent != null)
		{
			this.OnAnimatorMoveEvent();
		}
	}
}
