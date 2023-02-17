namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("Controls")]
	[Tooltip("Listens for an action button press (using HeroActions InControl mappings).")]
	public class ListenForRight : FsmStateAction
	{
		[Tooltip("Where to send the event.")]
		public FsmEventTarget eventTarget;
	
		public FsmEvent wasPressed;
	
		public FsmEvent wasReleased;
	
		public FsmEvent isPressed;
	
		public FsmEvent isNotPressed;
	
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
		}
	
		public override void OnUpdate()
		{
			if (!gm.isPaused)
			{
				if (inputHandler.inputActions.right.WasPressed)
				{
					base.Fsm.Event(wasPressed);
				}
				if (inputHandler.inputActions.right.WasReleased)
				{
					base.Fsm.Event(wasReleased);
				}
				if (inputHandler.inputActions.right.IsPressed)
				{
					base.Fsm.Event(isPressed);
				}
				if (!inputHandler.inputActions.right.IsPressed)
				{
					base.Fsm.Event(isNotPressed);
				}
			}
		}
	}
}