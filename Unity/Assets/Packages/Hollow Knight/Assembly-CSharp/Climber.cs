using System;
using System.Collections;
using UnityEngine;

public class Climber : MonoBehaviour
{
	private enum Direction
	{
		Right,
		Down,
		Left,
		Up
	}

	public bool startRight = true;

	private bool clockwise = true;

	public float speed = 2f;

	public float spinTime = 0.25f;

	[Space]
	public float wallRayPadding = 0.1f;

	[Space]
	public Vector2 constrain = new Vector2(0.1f, 0.1f);

	private Vector2 previousPos;

	public float minTurnDistance = 0.25f;

	private Vector2 previousTurnPos;

	private Direction currentDirection;

	private Coroutine turnRoutine;

	private Rigidbody2D body;

	private BoxCollider2D col;

	private tk2dSpriteAnimator anim;

	private void Awake()
	{
		body = GetComponent<Rigidbody2D>();
		col = GetComponent<BoxCollider2D>();
		anim = GetComponent<tk2dSpriteAnimator>();
	}

	private void Start()
	{
		StickToGround();
		float num = Mathf.Sign(base.transform.localScale.x);
		if (!startRight)
		{
			num *= -1f;
		}
		clockwise = num > 0f;
		float num2 = base.transform.eulerAngles.z % 360f;
		if (num2 >= 45f && num2 <= 135f)
		{
			currentDirection = ((!clockwise) ? Direction.Down : Direction.Up);
		}
		else if (num2 >= 135f && num2 <= 225f)
		{
			currentDirection = (clockwise ? Direction.Left : Direction.Right);
		}
		else if (num2 >= 225f && num2 <= 315f)
		{
			currentDirection = (clockwise ? Direction.Down : Direction.Up);
		}
		else
		{
			currentDirection = ((!clockwise) ? Direction.Left : Direction.Right);
		}
		Recoil component = GetComponent<Recoil>();
		if ((bool)component)
		{
			component.SkipFreezingByController = true;
			component.OnHandleFreeze += Stun;
		}
		previousPos = base.transform.position;
		StartCoroutine(Walk());
	}

	private IEnumerator Walk()
	{
		anim.Play("Walk");
		body.velocity = GetVelocity(currentDirection);
		while (true)
		{
			Vector2 vector = transform.position;
			bool flag = false;
			if (Mathf.Abs(vector.x - previousPos.x) > constrain.x)
			{
				vector.x = previousPos.x;
				flag = true;
			}
			if (Mathf.Abs(vector.y - previousPos.y) > constrain.y)
			{
				vector.y = previousPos.y;
				flag = true;
			}
			if (flag)
			{
				transform.position = vector;
			}
			else
			{
				previousPos = transform.position;
			}
			if (Vector3.Distance(previousTurnPos, transform.position) >= minTurnDistance)
			{
				if (!CheckGround())
				{
					turnRoutine = StartCoroutine(Turn(clockwise));
					yield return turnRoutine;
				}
				else if (CheckWall())
				{
					turnRoutine = StartCoroutine(Turn(!clockwise, tweenPos: true));
					yield return turnRoutine;
				}
			}
			yield return null;
		}
	}

	private IEnumerator Turn(bool turnClockwise, bool tweenPos = false)
	{
		body.velocity = Vector2.zero;
		float currentRotation = transform.eulerAngles.z;
		float targetRotation = currentRotation + (float)(turnClockwise ? (-90) : 90);
		Vector3 currentPos = transform.position;
		Vector3 targetPos = currentPos + (Vector3)GetTweenPos(currentDirection);
		for (float elapsed = 0f; elapsed < spinTime; elapsed += Time.deltaTime)
		{
			float t = elapsed / spinTime;
			transform.SetRotation2D(Mathf.Lerp(currentRotation, targetRotation, t));
			if (tweenPos)
			{
				transform.position = Vector3.Lerp(currentPos, targetPos, t);
			}
			yield return null;
		}
		transform.SetRotation2D(targetRotation);
		int num = (int)currentDirection;
		num += (turnClockwise ? 1 : (-1));
		int num2 = Enum.GetNames(typeof(Direction)).Length;
		if (num < 0)
		{
			num = num2 - 1;
		}
		else if (num >= num2)
		{
			num = 0;
		}
		currentDirection = (Direction)num;
		body.velocity = GetVelocity(currentDirection);
		previousPos = transform.position;
		previousTurnPos = previousPos;
		turnRoutine = null;
	}

	private Vector2 GetVelocity(Direction direction)
	{
		Vector2 result = Vector2.zero;
		switch (direction)
		{
		case Direction.Right:
			result = new Vector2(speed, 0f);
			break;
		case Direction.Down:
			result = new Vector2(0f, 0f - speed);
			break;
		case Direction.Left:
			result = new Vector2(0f - speed, 0f);
			break;
		case Direction.Up:
			result = new Vector2(0f, speed);
			break;
		}
		return result;
	}

	private bool CheckGround()
	{
		return FireRayLocal(Vector2.down, 1f).collider != null;
	}

	private bool CheckWall()
	{
		return FireRayLocal(clockwise ? Vector2.right : Vector2.left, col.size.x / 2f + wallRayPadding).collider != null;
	}

	public void Stun()
	{
		if (turnRoutine == null)
		{
			StopAllCoroutines();
			StartCoroutine(DoStun());
		}
	}

	private IEnumerator DoStun()
	{
		body.velocity = Vector2.zero;
		yield return StartCoroutine(anim.PlayAnimWait("Stun"));
		StartCoroutine(Walk());
	}

	private RaycastHit2D FireRayLocal(Vector2 direction, float length)
	{
		Vector2 vector = base.transform.TransformPoint(col.offset);
		Vector2 vector2 = base.transform.TransformDirection(direction);
		RaycastHit2D result = Physics2D.Raycast(vector, vector2, length, 256);
		Debug.DrawRay(vector, vector2);
		return result;
	}

	private Vector2 GetTweenPos(Direction direction)
	{
		Vector2 result = Vector2.zero;
		switch (direction)
		{
		case Direction.Right:
			result = (clockwise ? new Vector2(col.size.x / 2f, col.size.y / 2f) : new Vector2(col.size.x / 2f, 0f - col.size.y / 2f));
			result.x += wallRayPadding;
			break;
		case Direction.Up:
			result = (clockwise ? new Vector2(0f - col.size.x / 2f, col.size.y / 2f) : new Vector2(col.size.x / 2f, col.size.y / 2f));
			result.y += wallRayPadding;
			break;
		case Direction.Down:
			result = (clockwise ? new Vector2(col.size.x / 2f, 0f - col.size.y / 2f) : new Vector2(0f - col.size.x / 2f, 0f - col.size.y / 2f));
			result.y -= wallRayPadding;
			break;
		case Direction.Left:
			result = (clockwise ? new Vector2(0f - col.size.x / 2f, 0f - col.size.y / 2f) : new Vector2(0f - col.size.x / 2f, col.size.y / 2f));
			result.x -= wallRayPadding;
			break;
		}
		return result;
	}

	private void StickToGround()
	{
		RaycastHit2D raycastHit2D = FireRayLocal(Vector2.down, 2f);
		if (raycastHit2D.collider != null)
		{
			base.transform.position = raycastHit2D.point;
		}
	}
}
