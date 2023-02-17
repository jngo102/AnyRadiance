using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Sets various values of a UGui Layout Element component.")]
	public class uGuiLayoutElementSetValues : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(LayoutElement))]
		[Tooltip("The GameObject with the Layout Element component.")]
		public FsmOwnerDefault gameObject;
	
		[ActionSection("Values")]
		[Tooltip("The minimum width this layout element should have.")]
		public FsmFloat minWidth;
	
		[Tooltip("The minimum height this layout element should have.")]
		public FsmFloat minHeight;
	
		[Tooltip("The preferred width this layout element should have before additional available width is allocated.")]
		public FsmFloat preferredWidth;
	
		[Tooltip("The preferred height this layout element should have before additional available height is allocated.")]
		public FsmFloat preferredHeight;
	
		[Tooltip("The relative amount of additional available width this layout element should fill out relative to its siblings.")]
		public FsmFloat flexibleWidth;
	
		[Tooltip("The relative amount of additional available height this layout element should fill out relative to its siblings.")]
		public FsmFloat flexibleHeight;
	
		[ActionSection("Options")]
		[Tooltip("Repeats every frame")]
		public bool everyFrame;
	
		private LayoutElement _layoutElement;
	
		public override void Reset()
		{
			gameObject = null;
			minWidth = new FsmFloat
			{
				UseVariable = true
			};
			minHeight = new FsmFloat
			{
				UseVariable = true
			};
			preferredWidth = new FsmFloat
			{
				UseVariable = true
			};
			preferredHeight = new FsmFloat
			{
				UseVariable = true
			};
			flexibleWidth = new FsmFloat
			{
				UseVariable = true
			};
			flexibleHeight = new FsmFloat
			{
				UseVariable = true
			};
		}
	
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_layoutElement = ownerDefaultTarget.GetComponent<LayoutElement>();
			}
			DoSetValues();
			if (!everyFrame)
			{
				Finish();
			}
		}
	
		public override void OnUpdate()
		{
			DoSetValues();
		}
	
		private void DoSetValues()
		{
			if (_layoutElement != null)
			{
				if (!minWidth.IsNone)
				{
					_layoutElement.minWidth = minWidth.Value;
				}
				if (!minHeight.IsNone)
				{
					_layoutElement.minHeight = minHeight.Value;
				}
				if (!preferredWidth.IsNone)
				{
					_layoutElement.preferredWidth = preferredWidth.Value;
				}
				if (!preferredHeight.IsNone)
				{
					_layoutElement.preferredHeight = preferredHeight.Value;
				}
				if (!flexibleWidth.IsNone)
				{
					_layoutElement.flexibleWidth = flexibleWidth.Value;
				}
				if (!flexibleHeight.IsNone)
				{
					_layoutElement.flexibleHeight = flexibleHeight.Value;
				}
			}
		}
	}
}