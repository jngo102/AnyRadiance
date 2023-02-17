using UnityEngine;
using InControl;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory( "InControl" )]
	[Tooltip( "Gets a vector and/or magnitude from a two-axis control for a given device mapped to a specified plane." )]
	public class InControlGetControlVectorMappedToPlane : FsmStateAction
	{
		public enum TwoAxisControlType
		{
			LeftStickAndDPad,
			LeftStick,
			RightStick,
			DPad,
		}

		public enum MappingPlane
		{
			XZ,
			XY,
			YZ
		}


		[Tooltip( "The index of the device (set to 0 to query the current active device)." )]
		public FsmInt deviceIndex;
		
		public TwoAxisControlType control;
		
		[Tooltip( "Values are typically in the range -1.0 to +1.0. Use scale to get a larger range." )]
		public FsmFloat scale;
		
		[RequiredField]
		[Tooltip( "The world plane to map the 2D vector onto." )]
		public MappingPlane mapToPlane;
		
		[Tooltip( "Make the result relative to a game object, typically the main camera." )]
		public FsmGameObject relativeTo;
		
		[UIHint( UIHint.Variable )]
		public FsmVector3 storeVector;
		
		[UIHint( UIHint.Variable )]
		public FsmFloat storeMagnitude;

		[Tooltip( "Repeat every frame." )]
		public bool everyFrame;

		Vector3 xMappingVector;
		Vector3 yMappingVector;


		public override void Reset()
		{
			deviceIndex = 0;
			scale = 1.0f;
			mapToPlane = MappingPlane.XZ;
			storeVector = null;
			storeMagnitude = null;
		}


		public override void OnEnter()
		{	
			if (scale.IsNone)
			{
				scale = 1.0f;
			}

			SetupMappingVectors();
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


		void SetupMappingVectors()
		{
			if (relativeTo.Value == null)
			{
				switch (mapToPlane)
				{
					case MappingPlane.XZ:
						xMappingVector = Vector3.forward;
						yMappingVector = Vector3.right;
						break;

					case MappingPlane.XY:
						xMappingVector = Vector3.up;
						yMappingVector = Vector3.right;
						break;

					case MappingPlane.YZ:
						xMappingVector = Vector3.up;
						yMappingVector = Vector3.forward;
						break;
				}
			}
			else
			{
				var transform = relativeTo.Value.transform;

				switch (mapToPlane)
				{
					case MappingPlane.XZ:
						xMappingVector = transform.TransformDirection( Vector3.forward );
						xMappingVector.y = 0;
						xMappingVector = xMappingVector.normalized;
						yMappingVector = new Vector3( xMappingVector.z, 0, -xMappingVector.x );
						break;

					case MappingPlane.XY:
					case MappingPlane.YZ:
						xMappingVector = Vector3.up;
						xMappingVector.z = 0;
						xMappingVector = xMappingVector.normalized;
						yMappingVector = transform.TransformDirection( Vector3.right );
						break;
				}
			}
		}


		void GetControlVector()
		{
			var x = Control.X;
			var y = Control.Y;
			var v = (x * yMappingVector + y * xMappingVector).normalized * scale.Value;

			if (!storeVector.IsNone)
			{
				storeVector.Value = v;
			}

			if (!storeMagnitude.IsNone)
			{
				storeMagnitude.Value = v.magnitude;
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

