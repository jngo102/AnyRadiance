using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("Animator")]
	[Tooltip("Follow a target")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1033")]
	public class AnimatorFollow : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The GameObject. An Animator component and a PlayMakerAnimatorProxy component are required")]
		public FsmOwnerDefault gameObject;
	
		[RequiredField]
		[Tooltip("The Game Object to target.")]
		public FsmGameObject target;
	
		[Tooltip("The minimum distance to follow.")]
		public FsmFloat minimumDistance;
	
		[Tooltip("The damping for following up.")]
		public FsmFloat speedDampTime;
	
		[Tooltip("The damping for turning.")]
		public FsmFloat directionDampTime;
	
		private GameObject _go;
	
		private PlayMakerAnimatorMoveProxy _animatorProxy;
	
		private Animator avatar;
	
		private CharacterController controller;
	
		public override void Reset()
		{
			gameObject = null;
			target = null;
			speedDampTime = 0.25f;
			directionDampTime = 0.25f;
			minimumDistance = 1f;
		}
	
		public override void OnEnter()
		{
			_go = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go == null)
			{
				Finish();
				return;
			}
			_animatorProxy = _go.GetComponent<PlayMakerAnimatorMoveProxy>();
			if (_animatorProxy != null)
			{
				_animatorProxy.OnAnimatorMoveEvent += OnAnimatorMoveEvent;
			}
			avatar = _go.GetComponent<Animator>();
			controller = _go.GetComponent<CharacterController>();
			avatar.speed = 1f + Random.Range(-0.4f, 0.4f);
		}
	
		public override void OnUpdate()
		{
			GameObject value = target.Value;
			float value2 = speedDampTime.Value;
			float value3 = directionDampTime.Value;
			float value4 = minimumDistance.Value;
			if (!avatar || !value)
			{
				return;
			}
			if (Vector3.Distance(value.transform.position, avatar.rootPosition) > value4)
			{
				avatar.SetFloat("Speed", 1f, value2, Time.deltaTime);
				Vector3 lhs = avatar.rootRotation * Vector3.forward;
				Vector3 normalized = (value.transform.position - avatar.rootPosition).normalized;
				if (Vector3.Dot(lhs, normalized) > 0f)
				{
					avatar.SetFloat("Direction", Vector3.Cross(lhs, normalized).y, value3, Time.deltaTime);
				}
				else
				{
					avatar.SetFloat("Direction", (Vector3.Cross(lhs, normalized).y > 0f) ? 1 : (-1), value3, Time.deltaTime);
				}
			}
			else
			{
				avatar.SetFloat("Speed", 0f, value2, Time.deltaTime);
			}
			if (_animatorProxy == null)
			{
				OnAnimatorMoveEvent();
			}
		}
	
		public override void OnExit()
		{
			if (_animatorProxy != null)
			{
				_animatorProxy.OnAnimatorMoveEvent -= OnAnimatorMoveEvent;
			}
		}
	
		public void OnAnimatorMoveEvent()
		{
			controller.Move(avatar.deltaPosition);
			_go.transform.rotation = avatar.rootRotation;
		}
	}
}