using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Gets the explicit navigation properties of a Selectable Ugui component. ")]
	public class uGuiNavigationExplicitGetProperties : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Selectable))]
		[Tooltip("The GameObject with the Selectable ui component.")]
		public FsmOwnerDefault gameObject;
	
		[Tooltip("The down Selectable.")]
		[UIHint(UIHint.Variable)]
		public FsmGameObject selectOnDown;
	
		[Tooltip("The up Selectable.")]
		[UIHint(UIHint.Variable)]
		public FsmGameObject selectOnUp;
	
		[Tooltip("The left Selectable.")]
		[UIHint(UIHint.Variable)]
		public FsmGameObject selectOnLeft;
	
		[Tooltip("The right Selectable.")]
		[UIHint(UIHint.Variable)]
		public FsmGameObject selectOnRight;
	
		private Selectable _selectable;
	
		public override void Reset()
		{
			gameObject = null;
			selectOnDown = null;
			selectOnUp = null;
			selectOnLeft = null;
			selectOnRight = null;
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
			if (_selectable != null)
			{
				if (!selectOnUp.IsNone)
				{
					selectOnUp.Value = ((_selectable.navigation.selectOnUp == null) ? null : _selectable.navigation.selectOnUp.gameObject);
				}
				if (!selectOnDown.IsNone)
				{
					selectOnDown.Value = ((_selectable.navigation.selectOnDown == null) ? null : _selectable.navigation.selectOnDown.gameObject);
				}
				if (!selectOnLeft.IsNone)
				{
					selectOnLeft.Value = ((_selectable.navigation.selectOnLeft == null) ? null : _selectable.navigation.selectOnLeft.gameObject);
				}
				if (!selectOnRight.IsNone)
				{
					selectOnRight.Value = ((_selectable.navigation.selectOnRight == null) ? null : _selectable.navigation.selectOnRight.gameObject);
				}
			}
		}
	}
}