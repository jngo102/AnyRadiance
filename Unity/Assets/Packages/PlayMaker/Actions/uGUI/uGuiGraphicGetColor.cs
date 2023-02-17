using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Gets the color of a UGui graphic component.")]
	public class uGuiGraphicGetColor : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Graphic))]
		[Tooltip("The GameObject with the ui component.")]
		public FsmOwnerDefault gameObject;
	
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Color of the UI component")]
		public FsmColor color;
	
		[Tooltip("Repeats every frame")]
		public bool everyFrame;
	
		private Graphic _component;
	
		public override void Reset()
		{
			gameObject = null;
			color = null;
		}
	
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_component = ownerDefaultTarget.GetComponent<Graphic>();
			}
			DoGetColorValue();
			if (!everyFrame)
			{
				Finish();
			}
		}
	
		public override void OnUpdate()
		{
			DoGetColorValue();
		}
	
		private void DoGetColorValue()
		{
			if (_component != null)
			{
				color.Value = _component.color;
			}
		}
	}
}