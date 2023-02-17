using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Sets the value of a UGui Slider component.")]
	public class uGuiSliderSetValue : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Slider))]
		[Tooltip("The GameObject with the slider UGui component.")]
		public FsmOwnerDefault gameObject;
	
		[RequiredField]
		[Tooltip("The value of the UGui slider component.")]
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
				_originalValue = _slider.value;
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
				_slider.value = value.Value;
			}
		}
	
		public override void OnExit()
		{
			if (!(_slider == null) && resetOnExit.Value)
			{
				_slider.value = _originalValue;
			}
		}
	}
}