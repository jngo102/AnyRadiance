using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Sets the explicit navigation properties of a Selectable Ugui component. Note that it will have no effect until Navigation mode is set to 'Explicit'.")]
	public class uGuiNavigationExplicitSetProperties : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Selectable))]
		[Tooltip("The GameObject with the Selectable ui component.")]
		public FsmOwnerDefault gameObject;
	
		[Tooltip("The down Selectable. Leave to none for no effect")]
		[CheckForComponent(typeof(Selectable))]
		public FsmGameObject selectOnDown;
	
		[Tooltip("The up Selectable.  Leave to none for no effect")]
		[CheckForComponent(typeof(Selectable))]
		public FsmGameObject selectOnUp;
	
		[Tooltip("The left Selectable.  Leave to none for no effect")]
		[CheckForComponent(typeof(Selectable))]
		public FsmGameObject selectOnLeft;
	
		[Tooltip("The right Selectable.  Leave to none for no effect")]
		[CheckForComponent(typeof(Selectable))]
		public FsmGameObject selectOnRight;
	
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;
	
		private Selectable _selectable;
	
		private Navigation _navigation;
	
		private Navigation _originalState;
	
		public override void Reset()
		{
			gameObject = null;
			selectOnDown = new FsmGameObject
			{
				UseVariable = true
			};
			selectOnUp = new FsmGameObject
			{
				UseVariable = true
			};
			selectOnLeft = new FsmGameObject
			{
				UseVariable = true
			};
			selectOnRight = new FsmGameObject
			{
				UseVariable = true
			};
			resetOnExit = false;
		}
	
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_selectable = ownerDefaultTarget.GetComponent<Selectable>();
			}
			if (_selectable != null && resetOnExit.Value)
			{
				_originalState = _selectable.navigation;
			}
			DoSetValue();
			Finish();
		}
	
		private void DoSetValue()
		{
			if (_selectable != null)
			{
				_navigation = _selectable.navigation;
				if (!selectOnDown.IsNone)
				{
					_navigation.selectOnDown = GetComponentFromFsmGameObject<Selectable>(selectOnDown);
				}
				if (!selectOnUp.IsNone)
				{
					_navigation.selectOnUp = GetComponentFromFsmGameObject<Selectable>(selectOnUp);
				}
				if (!selectOnLeft.IsNone)
				{
					_navigation.selectOnLeft = GetComponentFromFsmGameObject<Selectable>(selectOnLeft);
				}
				if (!selectOnRight.IsNone)
				{
					_navigation.selectOnRight = GetComponentFromFsmGameObject<Selectable>(selectOnRight);
				}
				_selectable.navigation = _navigation;
			}
		}
	
		public override void OnExit()
		{
			if (!(_selectable == null) && resetOnExit.Value)
			{
				_navigation = _selectable.navigation;
				_navigation.selectOnDown = _originalState.selectOnDown;
				_navigation.selectOnLeft = _originalState.selectOnLeft;
				_navigation.selectOnRight = _originalState.selectOnRight;
				_navigation.selectOnUp = _originalState.selectOnUp;
				_selectable.navigation = _navigation;
			}
		}
	
		private T GetComponentFromFsmGameObject<T>(FsmGameObject variable) where T : Component
		{
			if (variable.Value != null)
			{
				return variable.Value.GetComponent<T>();
			}
			return null;
		}
	}
}