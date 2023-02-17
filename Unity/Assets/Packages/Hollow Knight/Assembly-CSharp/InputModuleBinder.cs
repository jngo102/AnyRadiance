using InControl;
using UnityEngine;

[RequireComponent(typeof(InControlInputModule))]
public class InputModuleBinder : MonoBehaviour
{
	public class MyActionSet : PlayerActionSet
	{
		public PlayerAction Submit;

		public PlayerAction Cancel;

		public PlayerAction Left;

		public PlayerAction Right;

		public PlayerAction Up;

		public PlayerAction Down;

		public PlayerTwoAxisAction Move;

		public MyActionSet()
		{
			Submit = CreatePlayerAction("Submit");
			Cancel = CreatePlayerAction("Cancel");
			Left = CreatePlayerAction("Move Left");
			Right = CreatePlayerAction("Move Right");
			Up = CreatePlayerAction("Move Up");
			Down = CreatePlayerAction("Move Down");
			Move = CreateTwoAxisPlayerAction(Left, Right, Down, Up);
		}
	}

	private MyActionSet actions;

	private void OnEnable()
	{
		actions = new MyActionSet();
		InControlInputModule component = GetComponent<InControlInputModule>();
		component.SubmitAction = actions.Submit;
		component.CancelAction = actions.Cancel;
		component.MoveAction = actions.Move;
		BindAndApplyActions();
		Platform.AcceptRejectInputStyleChanged += OnAcceptRejectInputStyleChanged;
	}

	private void OnDisable()
	{
		Platform.AcceptRejectInputStyleChanged -= OnAcceptRejectInputStyleChanged;
		actions.Destroy();
	}

	private void OnAcceptRejectInputStyleChanged()
	{
		BindAndApplyActions();
	}

	private void BindAndApplyActions()
	{
		actions.Submit.ClearBindings();
		actions.Cancel.ClearBindings();
		Platform.AcceptRejectInputStyles acceptRejectInputStyle = Platform.Current.AcceptRejectInputStyle;
		if (acceptRejectInputStyle == Platform.AcceptRejectInputStyles.NonJapaneseStyle || acceptRejectInputStyle != Platform.AcceptRejectInputStyles.JapaneseStyle)
		{
			actions.Submit.AddDefaultBinding(InputControlType.Action1);
			actions.Submit.AddDefaultBinding(Key.Space);
			actions.Submit.AddDefaultBinding(Key.Return);
			actions.Cancel.AddDefaultBinding(InputControlType.Action2);
			actions.Cancel.AddDefaultBinding(Key.Escape);
		}
		else
		{
			actions.Cancel.AddDefaultBinding(InputControlType.Action1);
			actions.Cancel.AddDefaultBinding(Key.Escape);
			actions.Submit.AddDefaultBinding(InputControlType.Action2);
			actions.Submit.AddDefaultBinding(Key.Space);
			actions.Submit.AddDefaultBinding(Key.Return);
		}
		actions.Up.ClearBindings();
		actions.Up.AddDefaultBinding(InputControlType.LeftStickUp);
		actions.Up.AddDefaultBinding(InputControlType.DPadUp);
		actions.Up.AddDefaultBinding(Key.UpArrow);
		actions.Down.ClearBindings();
		actions.Down.AddDefaultBinding(InputControlType.LeftStickDown);
		actions.Down.AddDefaultBinding(InputControlType.DPadDown);
		actions.Down.AddDefaultBinding(Key.DownArrow);
		actions.Left.ClearBindings();
		actions.Left.AddDefaultBinding(InputControlType.LeftStickLeft);
		actions.Left.AddDefaultBinding(InputControlType.DPadLeft);
		actions.Left.AddDefaultBinding(Key.LeftArrow);
		actions.Right.ClearBindings();
		actions.Right.AddDefaultBinding(InputControlType.LeftStickRight);
		actions.Right.AddDefaultBinding(InputControlType.DPadRight);
		actions.Right.AddDefaultBinding(Key.RightArrow);
	}
}
