using InControl;

public class HeroActions : PlayerActionSet
{
	public PlayerAction left;

	public PlayerAction right;

	public PlayerAction up;

	public PlayerAction down;

	public PlayerAction menuSubmit;

	public PlayerAction menuCancel;

	public PlayerTwoAxisAction moveVector;

	public PlayerAction rs_up;

	public PlayerAction rs_down;

	public PlayerAction rs_left;

	public PlayerAction rs_right;

	public PlayerTwoAxisAction rightStick;

	public PlayerAction jump;

	public PlayerAction evade;

	public PlayerAction dash;

	public PlayerAction superDash;

	public PlayerAction dreamNail;

	public PlayerAction attack;

	public PlayerAction cast;

	public PlayerAction focus;

	public PlayerAction quickMap;

	public PlayerAction quickCast;

	public PlayerAction textSpeedup;

	public PlayerAction skipCutscene;

	public PlayerAction openInventory;

	public PlayerAction paneRight;

	public PlayerAction paneLeft;

	public PlayerAction pause;

	public HeroActions()
	{
		menuSubmit = CreatePlayerAction("Submit");
		menuCancel = CreatePlayerAction("Cancel");
		left = CreatePlayerAction("Left");
		left.StateThreshold = 0.3f;
		right = CreatePlayerAction("Right");
		right.StateThreshold = 0.3f;
		up = CreatePlayerAction("Up");
		up.StateThreshold = 0.5f;
		down = CreatePlayerAction("Down");
		down.StateThreshold = 0.5f;
		moveVector = CreateTwoAxisPlayerAction(left, right, down, up);
		moveVector.LowerDeadZone = 0.15f;
		moveVector.UpperDeadZone = 0.95f;
		rs_up = CreatePlayerAction("RS_Up");
		rs_up.StateThreshold = 0.5f;
		rs_down = CreatePlayerAction("RS_Down");
		rs_down.StateThreshold = 0.5f;
		rs_left = CreatePlayerAction("RS_Left");
		rs_left.StateThreshold = 0.3f;
		rs_right = CreatePlayerAction("RS_Right");
		rs_right.StateThreshold = 0.3f;
		rightStick = CreateTwoAxisPlayerAction(rs_left, rs_right, rs_down, rs_up);
		rightStick.LowerDeadZone = 0.15f;
		rightStick.UpperDeadZone = 0.95f;
		jump = CreatePlayerAction("Jump");
		attack = CreatePlayerAction("Attack");
		evade = CreatePlayerAction("Evade");
		dash = CreatePlayerAction("Dash");
		superDash = CreatePlayerAction("Super Dash");
		dreamNail = CreatePlayerAction("Dream Nail");
		cast = CreatePlayerAction("Cast");
		focus = CreatePlayerAction("Focus");
		quickMap = CreatePlayerAction("Quick Map");
		quickCast = CreatePlayerAction("Quick Cast");
		textSpeedup = CreatePlayerAction("TextSpeedup");
		skipCutscene = CreatePlayerAction("SkipCutscene");
		openInventory = CreatePlayerAction("openInventory");
		paneRight = CreatePlayerAction("Pane Right");
		paneLeft = CreatePlayerAction("Pane Left");
		pause = CreatePlayerAction("Pause");
	}
}
