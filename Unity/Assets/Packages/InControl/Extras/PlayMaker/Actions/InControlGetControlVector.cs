using UnityEngine;
using InControl;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory( "InControl" )]
	[Tooltip( "Gets a vector, magnitude and/or angle from a two-axis input control for a given device." )]
	public class InControlGetControlVector : FsmStateAction
	{
		public enum TwoAxisControlType
		{
			LeftStickAndDPad,
			LeftStick,
			RightStick,
			DPad,
		}


		[Tooltip( "The index of the device (set to 0 to query the current active device)." )]
		public FsmInt deviceIndex;

		public TwoAxisControlType control;

		[Tooltip( "Values are typically in the range -1.0 to +1.0. Use scale to get a larger range." )]
		public FsmFloat scale;

		[UIHint( UIHint.Variable )]
		public FsmVector2 storeVector;

		[UIHint( UIHint.Variable )]
		public FsmFloat storeMagnitude;

		[UIHint( UIHint.Variable )]
		public FsmFloat storeAngle;

		[Tooltip( "Repeat every frame." )]
		public bool everyFrame;


		public override void Reset()
		{
			deviceIndex = 0;
			control = TwoAxisControlType.LeftStickAndDPad;
			scale = 1.0f;
			storeVector = null;
			storeMagnitude = null;
		}


		public override void OnEnter()
		{	
			if (scale.IsNone)
			{
				scale = 1.0f;
			}

			GetControlVector();

			if (!everyFrame)
			{
				Finish();
			}
		}


		public override void OnUpdate()
		{
			GetControlVector();
		}


		void GetControlVector()
		{
			if (!storeVector.IsNone)
			{
				storeVector.Value = Control.Vector * scale.Value;
			}

			if (!storeMagnitude.IsNone)
			{
				storeMagnitude.Value = Control.Vector.magnitude * scale.Value;
			}

			if (!storeAngle.IsNone)
			{
				storeAngle.Value = Control.Angle;
			}
		}


		TwoAxisInputControl Control
		{
			get
			{
				switch (control)
				{
					case TwoAxisControlType.LeftStickAndDPad:
						return Device.Direction;

					case TwoAxisControlType.LeftStick:
						return Device.LeftStick;

					case TwoAxisControlType.RightStick:
						return Device.RightStick;

					case TwoAxisControlType.DPad:
						return Device.DPad;
				}

				return Device.Direction; 
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

