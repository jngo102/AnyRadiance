using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("Hollow Knight")]
	[Tooltip("Change behaviour based on platform.")]
	public class SwitchOnPlatform : FsmStateAction
	{
		public FsmEvent Standalone;
	
		public FsmEvent Switch;
	
		public FsmEvent PS4;
	
		public FsmEvent XB1;
	
		public FsmEvent Other;
	
		public override void Reset()
		{
			Standalone = null;
			Switch = null;
			PS4 = null;
			XB1 = null;
			Other = null;
		}
	
		public override void OnEnter()
		{
			switch (Application.platform)
			{
			case RuntimePlatform.OSXPlayer:
			case RuntimePlatform.WindowsPlayer:
			case RuntimePlatform.LinuxPlayer:
				base.Fsm.Event(Standalone);
				break;
			case RuntimePlatform.Switch:
				base.Fsm.Event(Switch);
				break;
			case RuntimePlatform.PS4:
				base.Fsm.Event(PS4);
				break;
			case RuntimePlatform.XboxOne:
				base.Fsm.Event(XB1);
				break;
			default:
				base.Fsm.Event(Other);
				break;
			}
			Finish();
		}
	}
}