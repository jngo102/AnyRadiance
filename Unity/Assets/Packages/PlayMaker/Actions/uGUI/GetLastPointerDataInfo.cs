using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGUI")]
	[Tooltip("Gets pointer data on the last System event.")]
	public class GetLastPointerDataInfo : FsmStateAction
	{
		public static PointerEventData lastPointeEventData;
	
		[UIHint(UIHint.Variable)]
		public FsmInt clickCount;
	
		[UIHint(UIHint.Variable)]
		public FsmFloat clickTime;
	
		[UIHint(UIHint.Variable)]
		public FsmVector2 delta;
	
		[UIHint(UIHint.Variable)]
		public FsmBool dragging;
	
		[UIHint(UIHint.Variable)]
		public FsmBool eligibleForClick;
	
		[UIHint(UIHint.Variable)]
		public FsmGameObject enterEventCamera;
	
		[UIHint(UIHint.Variable)]
		public FsmGameObject pressEventCamera;
	
		[UIHint(UIHint.Variable)]
		public FsmBool isPointerMoving;
	
		[UIHint(UIHint.Variable)]
		public FsmBool isScrolling;
	
		[UIHint(UIHint.Variable)]
		public FsmGameObject lastPress;
	
		[UIHint(UIHint.Variable)]
		public FsmGameObject pointerDrag;
	
		[UIHint(UIHint.Variable)]
		public FsmGameObject pointerEnter;
	
		[UIHint(UIHint.Variable)]
		public FsmInt pointerId;
	
		[UIHint(UIHint.Variable)]
		public FsmGameObject pointerPress;
	
		[UIHint(UIHint.Variable)]
		public FsmVector2 position;
	
		[UIHint(UIHint.Variable)]
		public FsmVector2 pressPosition;
	
		[UIHint(UIHint.Variable)]
		public FsmGameObject rawPointerPress;
	
		[UIHint(UIHint.Variable)]
		public FsmVector2 scrollDelta;
	
		[UIHint(UIHint.Variable)]
		public FsmBool used;
	
		[UIHint(UIHint.Variable)]
		public FsmBool useDragThreshold;
	
		[UIHint(UIHint.Variable)]
		public FsmVector3 worldNormal;
	
		[UIHint(UIHint.Variable)]
		public FsmVector3 worldPosition;
	
		public override void Reset()
		{
			clickCount = null;
			clickTime = null;
			delta = null;
			dragging = null;
			eligibleForClick = null;
			enterEventCamera = null;
			pressEventCamera = null;
			isPointerMoving = null;
			isScrolling = null;
			lastPress = null;
			pointerDrag = null;
			pointerEnter = null;
			pointerId = null;
			pointerPress = null;
			position = null;
			pressPosition = null;
			rawPointerPress = null;
			scrollDelta = null;
			used = null;
			useDragThreshold = null;
			worldNormal = null;
			worldPosition = null;
		}
	
		public override void OnEnter()
		{
			if (lastPointeEventData == null)
			{
				Finish();
				return;
			}
			if (!clickCount.IsNone)
			{
				clickCount.Value = lastPointeEventData.clickCount;
			}
			if (!clickTime.IsNone)
			{
				clickTime.Value = lastPointeEventData.clickTime;
			}
			if (!delta.IsNone)
			{
				delta.Value = lastPointeEventData.delta;
			}
			if (!dragging.IsNone)
			{
				dragging.Value = lastPointeEventData.dragging;
			}
			if (!eligibleForClick.IsNone)
			{
				eligibleForClick.Value = lastPointeEventData.eligibleForClick;
			}
			if (!enterEventCamera.IsNone)
			{
				enterEventCamera.Value = lastPointeEventData.enterEventCamera.gameObject;
			}
			if (!isPointerMoving.IsNone)
			{
				isPointerMoving.Value = lastPointeEventData.IsPointerMoving();
			}
			if (!isScrolling.IsNone)
			{
				isScrolling.Value = lastPointeEventData.IsScrolling();
			}
			if (!lastPress.IsNone)
			{
				lastPress.Value = lastPointeEventData.lastPress;
			}
			if (!pointerDrag.IsNone)
			{
				pointerDrag.Value = lastPointeEventData.pointerDrag;
			}
			if (!pointerEnter.IsNone)
			{
				pointerEnter.Value = lastPointeEventData.pointerEnter;
			}
			if (!pointerId.IsNone)
			{
				pointerId.Value = lastPointeEventData.pointerId;
			}
			if (!pointerPress.IsNone)
			{
				pointerPress.Value = lastPointeEventData.pointerPress;
			}
			if (!position.IsNone)
			{
				position.Value = lastPointeEventData.position;
			}
			if (!pressEventCamera.IsNone)
			{
				pressEventCamera.Value = lastPointeEventData.pressEventCamera.gameObject;
			}
			if (!pressPosition.IsNone)
			{
				pressPosition.Value = lastPointeEventData.pressPosition;
			}
			if (!rawPointerPress.IsNone)
			{
				rawPointerPress.Value = lastPointeEventData.rawPointerPress;
			}
			if (!scrollDelta.IsNone)
			{
				scrollDelta.Value = lastPointeEventData.scrollDelta;
			}
			if (!used.IsNone)
			{
				used.Value = lastPointeEventData.used;
			}
			if (!useDragThreshold.IsNone)
			{
				useDragThreshold.Value = lastPointeEventData.useDragThreshold;
			}
			if (!worldNormal.IsNone)
			{
				worldNormal.Value = lastPointeEventData.worldNormal;
			}
			if (!worldPosition.IsNone)
			{
				worldPosition.Value = lastPointeEventData.worldPosition;
			}
			Finish();
		}
	}
}