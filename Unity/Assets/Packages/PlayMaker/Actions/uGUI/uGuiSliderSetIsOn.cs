using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Sets the isOn property of a UGui Toggle component.")]
	public class uGuiSliderSetIsOn : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Toggle))]
		[Tooltip("The GameObject with the Toggle UGui component.")]
		public FsmOwnerDefault gameObject;
	
		[RequiredField]
		[Tooltip("Should the toggle be on?")]
		public FsmBool isOn;
	
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;
	
		private Toggle _toggle;
	
		private bool _originalValue;
	
		public override void Reset()
		{
			gameObject = null;
			isOn = null;
			resetOnExit = null;
		}
	
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_toggle = ownerDefaultTarget.GetComponent<Toggle>();
			}
			if (resetOnExit.Value)
			{
				_originalValue = _toggle.isOn;
			}
			DoSetValue();
			Finish();
		}
	
		private void DoSetValue()
		{
			if (_toggle != null)
			{
				_toggle.isOn = isOn.Value;
			}
		}
	
		public override void OnExit()
		{
			if (!(_toggle == null) && resetOnExit.Value)
			{
				_toggle.isOn = _originalValue;
			}
		}
	}
}