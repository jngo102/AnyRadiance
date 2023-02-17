using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Sets the direction of a UGui Slider component.")]
	public class uGuiSliderSetDirection : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Slider))]
		[Tooltip("The GameObject with the slider UGui component.")]
		public FsmOwnerDefault gameObject;
	
		[RequiredField]
		[Tooltip("The direction of the UGui slider component.")]
		public Slider.Direction direction;
	
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;
	
		private Slider _slider;
	
		private Slider.Direction _originalValue;
	
		public override void Reset()
		{
			gameObject = null;
			direction = Slider.Direction.LeftToRight;
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
				_originalValue = _slider.direction;
			}
			DoSetValue();
		}
	
		private void DoSetValue()
		{
			if (_slider != null)
			{
				_slider.direction = direction;
			}
		}
	
		public override void OnExit()
		{
			if (!(_slider == null) && resetOnExit.Value)
			{
				_slider.direction = _originalValue;
			}
		}
	}
}