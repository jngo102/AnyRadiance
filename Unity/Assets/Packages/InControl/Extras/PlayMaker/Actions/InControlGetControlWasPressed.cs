using UnityEngine;
using InControl;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory( "InControl" )]
	[Tooltip( "Sends an event when control state for a given device is pressed. Optionally store the control state in a bool variable." )]
	public class InControlGetControlWasPressed : FsmStateAction
	{
		[Tooltip( "The index of the device (set to 0 to query the current active device)." )]
		public FsmInt deviceIndex;
		
		public InputControlType control;

		[Tooltip( "The absolute value at which a control is considered pressed." )]
		public FsmFloat stateThreshold;
		
		public FsmEvent sendEvent;

		[UIHint( UIHint.Variable )]
		public FsmBool storeResult;
		

		public override void Reset()
		{
			deviceIndex = null;
			control = InputControlType.Action1;
			stateThreshold = 0.5f;
			sendEvent = null;
			storeResult = null;
		}


		public override void OnEnter()
		{
		}


		public override void OnUpdate()
		{			
			var inputControl = Device.GetControl( control );
			inputControl.StateThreshold = stateThreshold.Value;

			if (!storeResult.IsNone)
			{
				storeResult.Value = inputControl.WasPressed;
			}

			if (inputControl.WasPressed)
			{
				Fsm.Event( sendEvent );				
			}
		}


		InputDevice Device
		{
			get
			{
				return deviceIndex.Value > 0 ? InputManager.Devices[deviceIndex.Value - 1] : InputManager.ActiveDevice;
			}
		}
	}
}