using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Gets the isOn value of a UGui Toggle component. Optionally send events")]
	public class uGuiToggleGetIsOn : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Toggle))]
		[Tooltip("The GameObject with the Toggle UGui component.")]
		public FsmOwnerDefault gameObject;
	
		[UIHint(UIHint.Variable)]
		[Tooltip("The isOn Value of the UGui slider component.")]
		public FsmBool value;
	
		[Tooltip("Event sent when isOn Value is true.")]
		public FsmEvent isOnEvent;
	
		[Tooltip("Event sent when isOn Value is false.")]
		public FsmEvent isOffEvent;
	
		[Tooltip("Repeats every frame")]
		public bool everyFrame;
	
		private Toggle _toggle;
	
		public override void Reset()
		{
			gameObject = null;
			value = null;
			everyFrame = false;
		}
	
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_toggle = ownerDefaultTarget.GetComponent<Toggle>();
			}
			DoGetValue();
			if (!everyFrame)
			{
				Finish();
			}
		}
	
		public override void OnUpdate()
		{
			DoGetValue();
		}
	
		private void DoGetValue()
		{
			if (!(_toggle != null))
			{
				return;
			}
			value.Value = _toggle.isOn;
			if (_toggle.isOn)
			{
				if (isOnEvent != null)
				{
					base.Fsm.Event(isOnEvent);
				}
			}
			else if (isOnEvent != null)
			{
				base.Fsm.Event(isOffEvent);
			}
		}
	}
}