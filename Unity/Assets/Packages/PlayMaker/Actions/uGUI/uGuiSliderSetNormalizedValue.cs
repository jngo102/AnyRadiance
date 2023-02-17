using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Sets the normalized value ( between 0 and 1) of a UGui Slider component.")]
	public class uGuiSliderSetNormalizedValue : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Slider))]
		[Tooltip("The GameObject with the slider UGui component.")]
		public FsmOwnerDefault gameObject;
	
		[RequiredField]
		[HasFloatSlider(0f, 1f)]
		[Tooltip("The normalized value ( between 0 and 1) of the UGui slider component.")]
		public FsmFloat value;
	
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;
	
		[Tooltip("Repeats every frame")]
		public bool everyFrame;
	
		private Slider _slider;
	
		private float _originalValue;
	
		public override void Reset()
		{
			gameObject = null;
			value = null;
			resetOnExit = null;
			everyFrame = false;
		}
	
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_slider = ownerDefaultTarget.GetComponent<Slider>();
			}
			if (resetOnExit.Value)
			{
				_originalValue = _slider.normalizedValue;
			}
			DoSetValue();
			if (!everyFrame)
			{
				Finish();
			}
		}
	
		public override void OnUpdate()
		{
			DoSetValue();
		}
	
		private void DoSetValue()
		{
			if (_slider != null)
			{
				_slider.normalizedValue = value.Value;
			}
		}
	
		public override void OnExit()
		{
			if (!(_slider == null) && resetOnExit.Value)
			{
				_slider.normalizedValue = _originalValue;
			}
		}
	}
}