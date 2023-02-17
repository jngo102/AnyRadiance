using GlobalEnums;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	
	public class PauseMenuButton : MenuSelectable, ISubmitHandler, IEventSystemHandler, IPointerClickHandler, ICancelHandler
	{
		public enum PauseButtonType
		{
			Continue,
			Options,
			Quit
		}
	
		public Animator flashEffect;
	
		private GameManager gm;
	
		private UIManager ui;
	
		private InputHandler ih;
	
		public PauseButtonType pauseButtonType;
	
		public new CancelAction cancelAction { get; private set; }
	
		private new void Start()
		{
			gm = GameManager.instance;
			ih = gm.inputHandler;
			ui = UIManager.instance;
			HookUpAudioPlayer();
		}
	
		public void OnSubmit(BaseEventData eventData)
		{
			if (pauseButtonType == PauseButtonType.Continue)
			{
				if (ih.pauseAllowed)
				{
					ui.TogglePauseGame();
					flashEffect.ResetTrigger("Flash");
					flashEffect.SetTrigger("Flash");
					ForceDeselect();
					PlaySubmitSound();
				}
			}
			else if (pauseButtonType == PauseButtonType.Options)
			{
				ui.UIGoToOptionsMenu();
				flashEffect.ResetTrigger("Flash");
				flashEffect.SetTrigger("Flash");
				ForceDeselect();
				PlaySubmitSound();
			}
			else if (pauseButtonType == PauseButtonType.Quit)
			{
				ui.UIShowReturnMenuPrompt();
				flashEffect.ResetTrigger("Flash");
				flashEffect.SetTrigger("Flash");
				ForceDeselect();
				PlaySubmitSound();
			}
		}
	
		public new void OnCancel(BaseEventData eventData)
		{
			if (ih.pauseAllowed)
			{
				ui.TogglePauseGame();
				flashEffect.ResetTrigger("Flash");
				flashEffect.SetTrigger("Flash");
				ForceDeselect();
				PlaySubmitSound();
			}
		}
	
		public void OnPointerClick(PointerEventData eventData)
		{
			OnSubmit(eventData);
		}
	}
}