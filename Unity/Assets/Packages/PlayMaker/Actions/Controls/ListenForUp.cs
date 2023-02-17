namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("Controls")]
	[Tooltip("Listens for an action button press (using HeroActions InControl mappings).")]
	public class ListenForUp : FsmStateAction
	{
		[Tooltip("Where to send the event.")]
		public FsmEventTarget eventTarget;
	
		public FsmEvent wasPressed;
	
		public FsmEvent wasReleased;
	
		public FsmEvent isPressed;
	
		public FsmEvent isNotPressed;
	
		[UIHint(UIHint.Variable)]
		public FsmBool isPressedBool;
	
		public bool stateEntryOnly;
	
		private GameManager gm;
	
		private InputHandler inputHandler;
	
		public override void Reset()
		{
			eventTarget = null;
		}
	
		public override void OnEnter()
		{
			gm = GameManager.instance;
			inputHandler = gm.GetComponent<InputHandler>();
			CheckForInput();
			if (stateEntryOnly)
			{
				Finish();
			}
		}
	
		public override void OnUpdate()
		{
			CheckForInput();
		}
	
		private void CheckForInput()
		{
			if (gm.isPaused)
			{
				return;
			}
			if (inputHandler.inputActions.up.WasPressed)
			{
				base.Fsm.Event(wasPressed);
			}
			if (inputHandler.inputActions.up.WasReleased)
			{
				base.Fsm.Event(wasReleased);
			}
			if (inputHandler.inputActions.up.IsPressed)
			{
				base.Fsm.Event(isPressed);
				if (!isPressedBool.IsNone)
				{
					isPressedBool.Value = true;
				}
			}
			if (!inputHandler.inputActions.up.IsPressed)
			{
				base.Fsm.Event(isNotPressed);
				if (!isPressedBool.IsNone)
				{
					isPressedBool.Value = false;
				}
			}
		}
	}
}