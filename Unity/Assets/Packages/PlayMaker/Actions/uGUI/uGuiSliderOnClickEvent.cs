using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Fires an event on value changed for a UGui Slider component. Event float data will feature the slider value")]
	public class uGuiSliderOnClickEvent : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Slider))]
		[Tooltip("The GameObject with the Slider ui component.")]
		public FsmOwnerDefault gameObject;
	
		[Tooltip("Send this event when Clicked.")]
		public FsmEvent sendEvent;
	
		private Slider _slider;
	
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
				_slider = ownerDefaultTarget.GetComponent<Slider>();
				if (_slider != null)
				{
					_slider.onValueChanged.AddListener(DoOnValueChanged);
				}
				else
				{
					LogError("Missing UI.Slider on " + ownerDefaultTarget.name);
				}
			}
			else
			{
				LogError("Missing GameObject");
			}
		}
	
		public override void OnExit()
		{
			if (_slider != null)
			{
				_slider.onValueChanged.RemoveListener(DoOnValueChanged);
			}
		}
	
		public void DoOnValueChanged(float value)
		{
			Fsm.EventData.FloatData = value;
			base.Fsm.Event(sendEvent);
		}
	}
}