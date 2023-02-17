using UnityEngine;
using InControl;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory( "InControl" )]
	[Tooltip( "Gets the value of the specified InControl control for a given device and stores it in a float variable." )]
	public class InControlGetControlValue : FsmStateAction
	{
		[Tooltip( "The index of the device. Use to 0 to query the current active device." )]
		public FsmInt deviceIndex;
		
		public InputControlType control;
		
		[Tooltip( "Values are typically in the range -1.0 to +1.0. Use scale to get a larger range." )]
		public FsmFloat scale;
		
		[RequiredField]
		[UIHint( UIHint.Variable )]
		[Tooltip( "Store the result in a float variable." )]
		public FsmFloat storeResult;
		
		[Tooltip( "Repeat every frame." )]
		public bool everyFrame;
		

		public override void Reset()
		{
			deviceIndex = 0;
			control = InputControlType.LeftStickX;
			scale = 1.0f;
			storeResult = null;
			everyFrame = true;
		}


		public override void OnEnter()
		{	
			if (scale.IsNone)
			{
				scale = 1.0f;
			}

			GetControlValue();
			
			if (!everyFrame)
			{
				Finish();
			}
		}


		public override void OnUpdate()
		{
			GetControlValue();
		}


		void GetControlValue()
		{
			storeResult.Value = Device.GetControl( control ).Value * scale.Value;
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

