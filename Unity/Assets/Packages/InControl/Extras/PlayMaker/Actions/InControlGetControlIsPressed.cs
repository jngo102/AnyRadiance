using UnityEngine;
using InControl;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory ("InControl")]
	[Tooltip ("Gets the current pressed state of a specified control for a given device and stores it in a bool variable.")]
	public class InControlGetControlIsPressed : FsmStateAction
	{
		[Tooltip ("The index of the device (set to 0 to query the current active device).")]
		public FsmInt deviceIndex;

		public InputControlType control;

		[Tooltip ("The absolute value at which a control is considered pressed.")]
		public FsmFloat stateThreshold;

		[RequiredField]
		[UIHint (UIHint.Variable)]
		[Tooltip ("Store the result in a bool variable.")]
		public FsmBool storeResult;
		
		[Tooltip ("Repeat every frame.")]
		public bool everyFrame;


		public override void Reset ()
		{
			deviceIndex = null;
			control = InputControlType.Action1;
			stateThreshold = 0.5f;
			storeResult = null;
			everyFrame = true;
		}


		public override void OnEnter ()
		{
			GetControlState ();
			
			if (!everyFrame) {
				Finish ();
			}
		}


		public override void OnUpdate ()
		{
			GetControlState ();
		}


		void GetControlState ()
		{
			var inputControl = Device.GetControl (control);
			inputControl.StateThreshold = stateThreshold.Value;
			storeResult.Value = inputControl.State;
		}


		InputDevice Device {
			get {
				return deviceIndex.Value > 0 ? InputManager.Devices [deviceIndex.Value - 1] : InputManager.ActiveDevice;
			}
		}
	}
}

