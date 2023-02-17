using InControl;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	
	public class MenuScroller : UIBehaviour, IMoveHandler, IEventSystemHandler, ISelectHandler, IDeselectHandler, ISubmitHandler, IPointerClickHandler, ICancelHandler
	{
		public ScrollRect scrollRect;
	
		public Scrollbar scrollbar;
	
		public HollowKnightInputModule inputModule;
	
		public float scrollRate = 0.1f;
	
		public float scrollRepeatRate = 0.02f;
	
		public float scrollFirstDelay = 0.4f;
	
		private EventSystem eventSystem;
	
		private float prevRepeatRate;
	
		private float prevInitialDelay;
	
		public void OnSelect(BaseEventData eventData)
		{
			prevRepeatRate = inputModule.moveRepeatDelayDuration;
			prevInitialDelay = inputModule.moveRepeatFirstDuration;
			inputModule.moveRepeatDelayDuration = scrollRepeatRate;
			inputModule.moveRepeatFirstDuration = scrollFirstDelay;
		}
	
		public void OnDeselect(BaseEventData eventData)
		{
			inputModule.moveRepeatDelayDuration = prevRepeatRate;
			inputModule.moveRepeatFirstDuration = prevInitialDelay;
		}
	
		public void OnSubmit(BaseEventData eventData)
		{
			OnDeselect(eventData);
		}
	
		public void OnPointerClick(PointerEventData eventData)
		{
			OnDeselect(eventData);
		}
	
		public void OnCancel(BaseEventData eventData)
		{
			OnDeselect(eventData);
		}
	
		public void OnMove(AxisEventData move)
		{
			if (move.moveDir == MoveDirection.Up)
			{
				scrollbar.value += scrollRate;
			}
			else if (move.moveDir == MoveDirection.Down)
			{
				scrollbar.value -= scrollRate;
			}
		}
	}
}