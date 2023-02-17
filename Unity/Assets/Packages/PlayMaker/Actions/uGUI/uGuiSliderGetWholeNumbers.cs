using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Gets the wholeNumbers property of a UGui Slider component. If true, the slider is constrained to integer values")]
	public class uGuiSliderGetWholeNumbers : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Slider))]
		[Tooltip("The GameObject with the slider UGui component.")]
		public FsmOwnerDefault gameObject;
	
		[UIHint(UIHint.Variable)]
		[Tooltip("Is the slider constrained to integer values?")]
		public FsmBool wholeNumbers;
	
		[Tooltip("Event sent if slider is showing integers")]
		public FsmEvent isShowingWholeNumbersEvent;
	
		[Tooltip("Event sent if slider is showing floats")]
		public FsmEvent isNotShowingWholeNumbersEvent;
	
		private Slider _slider;
	
		public override void Reset()
		{
			gameObject = null;
			isShowingWholeNumbersEvent = null;
			isNotShowingWholeNumbersEvent = null;
			wholeNumbers = null;
		}
	
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_slider = ownerDefaultTarget.GetComponent<Slider>();
			}
			DoGetValue();
			Finish();
		}
	
		private void DoGetValue()
		{
			bool flag = false;
			if (_slider != null)
			{
				flag = _slider.wholeNumbers;
			}
			wholeNumbers.Value = flag;
			if (flag)
			{
				base.Fsm.Event(isShowingWholeNumbersEvent);
			}
			else
			{
				base.Fsm.Event(isNotShowingWholeNumbersEvent);
			}
		}
	}
}