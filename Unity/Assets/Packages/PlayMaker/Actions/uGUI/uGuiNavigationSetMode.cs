using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Sets the navigation mode of a Selectable Ugui component.")]
	public class uGuiNavigationSetMode : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Selectable))]
		[Tooltip("The GameObject with the Selectable ui component.")]
		public FsmOwnerDefault gameObject;
	
		[Tooltip("The navigation mode value")]
		public Navigation.Mode navigationMode;
	
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;
	
		private Selectable _selectable;
	
		private Navigation _navigation;
	
		private Navigation.Mode _originalValue;
	
		public override void Reset()
		{
			gameObject = null;
			navigationMode = Navigation.Mode.Automatic;
			resetOnExit = false;
		}
	
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_selectable = ownerDefaultTarget.GetComponent<Selectable>();
			}
			if (_selectable != null && resetOnExit.Value)
			{
				_originalValue = _selectable.navigation.mode;
			}
			DoSetValue();
			Finish();
		}
	
		private void DoSetValue()
		{
			if (_selectable != null)
			{
				_navigation = _selectable.navigation;
				_navigation.mode = navigationMode;
				_selectable.navigation = _navigation;
			}
		}
	
		public override void OnExit()
		{
			if (!(_selectable == null) && resetOnExit.Value)
			{
				_navigation = _selectable.navigation;
				_navigation.mode = _originalValue;
				_selectable.navigation = _navigation;
			}
		}
	}
}