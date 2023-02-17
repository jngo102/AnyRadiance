using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Sets the wholeNumbers property of a UGui Slider component. This defines if the slider will be constrained to integer values ")]
	public class uGuiSliderSetWholeNumbers : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Slider))]
		[Tooltip("The GameObject with the slider UGui component.")]
		public FsmOwnerDefault gameObject;
	
		[RequiredField]
		[Tooltip("Should the slider be constrained to integer values?")]
		public FsmBool wholeNumbers;
	
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;
	
		private Slider _slider;
	
		private bool _originalValue;
	
		public override void Reset()
		{
			gameObject = null;
			wholeNumbers = null;
			resetOnExit = null;
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
				_originalValue = _slider.wholeNumbers;
			}
			DoSetValue();
			Finish();
		}
	
		private void DoSetValue()
		{
			if (_slider != null)
			{
				_slider.wholeNumbers = wholeNumbers.Value;
			}
		}
	
		public override void OnExit()
		{
			if (!(_slider == null) && resetOnExit.Value)
			{
				_slider.wholeNumbers = _originalValue;
			}
		}
	}
}