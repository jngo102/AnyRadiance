using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Gets the interactable flag of a Selectable Ugui component.")]
	public class uGuiGetIsInteractable : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Selectable))]
		[Tooltip("The GameObject with the Selectable ui component.")]
		public FsmOwnerDefault gameObject;
	
		[Tooltip("The Interactable value")]
		[UIHint(UIHint.Variable)]
		public FsmBool isInteractable;
	
		[Tooltip("Event sent if Component is Interactable")]
		public FsmEvent isInteractableEvent;
	
		[Tooltip("Event sent if Component is not Interactable")]
		public FsmEvent isNotInteractableEvent;
	
		private Selectable _selectable;
	
		private bool _originalState;
	
		public override void Reset()
		{
			gameObject = null;
			isInteractable = null;
			isInteractableEvent = null;
			isNotInteractableEvent = null;
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
				bool flag = _selectable.IsInteractable();
				isInteractable.Value = flag;
				if (flag)
				{
					base.Fsm.Event(isInteractableEvent);
				}
				else
				{
					base.Fsm.Event(isNotInteractableEvent);
				}
			}
		}
	}
}