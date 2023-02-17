using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Fires an event on value changed for a UGui Scrollbar component. Event float data will feature the slider value")]
	public class uGuiScrollbarOnClickEvent : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Scrollbar))]
		[Tooltip("The GameObject with the Scrollbar ui component.")]
		public FsmOwnerDefault gameObject;
	
		[Tooltip("Send this event when Clicked.")]
		public FsmEvent sendEvent;
	
		private Scrollbar _scrollbar;
	
		public override void Reset()
		{
			gameObject = null;
			sendEvent = null;
		}
	
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_scrollbar = ownerDefaultTarget.GetComponent<Scrollbar>();
				if (_scrollbar != null)
				{
					_scrollbar.onValueChanged.AddListener(DoOnValueChanged);
				}
				else
				{
					LogError("Missing UI.Scrollbar on " + ownerDefaultTarget.name);
				}
			}
			else
			{
				LogError("Missing GameObject");
			}
		}
	
		public override void OnExit()
		{
			if (_scrollbar != null)
			{
				_scrollbar.onValueChanged.RemoveListener(DoOnValueChanged);
			}
		}
	
		public void DoOnValueChanged(float value)
		{
			Fsm.EventData.FloatData = value;
			base.Fsm.Event(sendEvent);
		}
	}
}