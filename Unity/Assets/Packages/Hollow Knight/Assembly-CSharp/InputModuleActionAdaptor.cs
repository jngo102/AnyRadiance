using InControl;
using UnityEngine;

[RequireComponent(typeof(HollowKnightInputModule))]
public class InputModuleActionAdaptor : MonoBehaviour
{
	private InputHandler inputHandler;

	private HollowKnightInputModule inputModule;

	private void Start()
	{
		inputHandler = GameManager.instance.inputHandler;
		inputModule = GetComponent<HollowKnightInputModule>();
		if (inputHandler != null && inputModule != null)
		{
			inputModule.MoveAction = inputHandler.inputActions.moveVector;
			inputModule.SubmitAction = inputHandler.inputActions.menuSubmit;
			inputModule.CancelAction = inputHandler.inputActions.menuCancel;
			inputModule.JumpAction = inputHandler.inputActions.jump;
			inputModule.AttackAction = inputHandler.inputActions.attack;
			inputModule.CastAction = inputHandler.inputActions.cast;
			inputModule.MoveAction = inputHandler.inputActions.moveVector;
		}
		else
		{
			Debug.LogError("Unable to bind player action set to Input Module.");
		}
	}
}
