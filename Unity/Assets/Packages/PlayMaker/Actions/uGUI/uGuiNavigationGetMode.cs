using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Gets the navigation mode of a Selectable Ugui component.")]
	public class uGuiNavigationGetMode : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Selectable))]
		[Tooltip("The GameObject with the Selectable ui component.")]
		public FsmOwnerDefault gameObject;
	
		[Tooltip("The navigation mode value")]
		public FsmString navigationMode;
	
		[Tooltip("Event sent if transition is ColorTint")]
		public FsmEvent automaticEvent;
	
		[Tooltip("Event sent if transition is ColorTint")]
		public FsmEvent horizontalEvent;
	
		[Tooltip("Event sent if transition is SpriteSwap")]
		public FsmEvent verticalEvent;
	
		[Tooltip("Event sent if transition is Animation")]
		public FsmEvent explicitEvent;
	
		[Tooltip("Event sent if transition is none")]
		public FsmEvent noNavigationEvent;
	
		private Selectable _selectable;
	
		private Selectable.Transition _originalTransition;
	
		public override void Reset()
		{
			gameObject = null;
		}
	
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_selectable = ownerDefaultTarget.GetComponent<Selectable>();
			}
			DoGetValue();
			Finish();
		}
	
		private void DoGetValue()
		{
			if (!(_selectable == null))
			{
				navigationMode.Value = _selectable.navigation.mode.ToString();
				if (_selectable.navigation.mode == Navigation.Mode.None)
				{
					base.Fsm.Event(noNavigationEvent);
				}
				else if (_selectable.navigation.mode == Navigation.Mode.Automatic)
				{
					base.Fsm.Event(automaticEvent);
				}
				else if (_selectable.navigation.mode == Navigation.Mode.Vertical)
				{
					base.Fsm.Event(verticalEvent);
				}
				else if (_selectable.navigation.mode == Navigation.Mode.Horizontal)
				{
					base.Fsm.Event(horizontalEvent);
				}
				else if (_selectable.navigation.mode == Navigation.Mode.Explicit)
				{
					base.Fsm.Event(explicitEvent);
				}
			}
		}
	}
}