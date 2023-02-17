using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Set Graphic Color.")]
	public class uGuiGraphicSetColor : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Graphic))]
		[Tooltip("The GameObject with an Unity UI component.")]
		public FsmOwnerDefault gameObject;
	
		[Tooltip("The Color of the UI component. Leave to none and set the individual color values, for example to affect just the alpha channel")]
		public FsmColor color;
	
		[Tooltip("The red channel Color of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat red;
	
		[Tooltip("The green channel Color of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat green;
	
		[Tooltip("The blue channel Color of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat blue;
	
		[Tooltip("The alpha channel Color of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat alpha;
	
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;
	
		[Tooltip("Repeats every frame, useful for animation")]
		public bool everyFrame;
	
		private Graphic _component;
	
		private Color _originalColor;
	
		public override void Reset()
		{
			gameObject = null;
			color = null;
			red = new FsmFloat
			{
				UseVariable = true
			};
			green = new FsmFloat
			{
				UseVariable = true
			};
			blue = new FsmFloat
			{
				UseVariable = true
			};
			alpha = new FsmFloat
			{
				UseVariable = true
			};
			resetOnExit = null;
			everyFrame = false;
		}
	
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_component = ownerDefaultTarget.GetComponent<Graphic>();
			}
			if (resetOnExit.Value)
			{
				_originalColor = _component.color;
			}
			DoSetColorValue();
			if (!everyFrame)
			{
				Finish();
			}
		}
	
		public override void OnUpdate()
		{
			DoSetColorValue();
		}
	
		private void DoSetColorValue()
		{
			if (_component != null)
			{
				Color value = _component.color;
				if (!color.IsNone)
				{
					value = color.Value;
				}
				if (!red.IsNone)
				{
					value.r = red.Value;
				}
				if (!green.IsNone)
				{
					value.g = green.Value;
				}
				if (!blue.IsNone)
				{
					value.b = blue.Value;
				}
				if (!alpha.IsNone)
				{
					value.a = alpha.Value;
				}
				_component.color = value;
			}
		}
	
		public override void OnExit()
		{
			if (!(_component == null) && resetOnExit.Value)
			{
				_component.color = _originalColor;
			}
		}
	}
}