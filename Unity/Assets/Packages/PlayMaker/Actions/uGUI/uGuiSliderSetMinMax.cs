using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Sets the minimum and maximum limits for the value of a UGui Slider component. Optionally resets on exit")]
	public class uGuiSliderSetMinMax : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Slider))]
		[Tooltip("The GameObject with the slider UGui component.")]
		public FsmOwnerDefault gameObject;
	
		[Tooltip("The minimum value of the UGui slider component. Leave to none for no effect")]
		public FsmFloat minValue;
	
		[Tooltip("The maximum value of the UGui slider component. Leave to none for no effect")]
		public FsmFloat maxValue;
	
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;
	
		[Tooltip("Repeats every frame")]
		public bool everyFrame;
	
		private Slider _slider;
	
		private float _originalMinValue;
	
		private float _originalMaxValue;
	
		public override void Reset()
		{
			gameObject = null;
			minValue = new FsmFloat
			{
				UseVariable = true
			};
			maxValue = new FsmFloat
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
				_slider = ownerDefaultTarget.GetComponent<Slider>();
			}
			if (resetOnExit.Value)
			{
				_originalMinValue = _slider.minValue;
				_originalMaxValue = _slider.maxValue;
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
				if (!minValue.IsNone)
				{
					_slider.minValue = minValue.Value;
				}
				if (!maxValue.IsNone)
				{
					_slider.maxValue = maxValue.Value;
				}
			}
		}
	
		public override void OnExit()
		{
			if (!(_slider == null) && resetOnExit.Value)
			{
				_slider.minValue = _originalMinValue;
				_slider.maxValue = _originalMaxValue;
			}
		}
	}
}