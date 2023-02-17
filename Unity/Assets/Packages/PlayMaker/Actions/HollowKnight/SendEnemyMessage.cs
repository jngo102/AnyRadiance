using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("Hollow Knight")]
	[Tooltip("Hook for enemy messages for scripts. Translates messages into appropriate method calls.")]
	public class SendEnemyMessage : FsmStateAction
	{
		public FsmGameObject Target;
	
		public FsmString EventString;
	
		public override void Reset()
		{
			Target = new FsmGameObject
			{
				UseVariable = true
			};
			EventString = new FsmString
			{
				UseVariable = true
			};
		}
	
		public override void OnEnter()
		{
			GameObject value = Target.Value;
			string value2 = EventString.Value;
			if (value != null && !string.IsNullOrEmpty(value2))
			{
				switch (value2)
				{
				case "GO LEFT":
					SendWalkerGoInDirection(value, -1);
					break;
				case "GO RIGHT":
					SendWalkerGoInDirection(value, 1);
					break;
				}
			}
			Finish();
		}
	
		private static void SendWalkerGoInDirection(GameObject target, int facing)
		{
			Walker component = target.GetComponent<Walker>();
			if (component != null)
			{
				component.RecieveGoMessage(facing);
			}
		}
	}
}