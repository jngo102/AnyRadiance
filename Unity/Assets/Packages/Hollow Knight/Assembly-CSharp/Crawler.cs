using System.Collections;
using UnityEngine;

public class Crawler : MonoBehaviour
{
	private enum CrawlerType
	{
		Floor,
		Roof,
		Wall
	}

	public float speed = 2f;

	[Space]
	public Transform wallCheck;

	public Transform groundCheck;

	private Vector2 velocity;

	private CrawlerType type;

	private Rigidbody2D body;

	private Recoil recoil;

	private tk2dSpriteAnimator anim;

	private void Awake()
	{
		body = GetComponent<Rigidbody2D>();
		recoil = GetComponent<Recoil>();
		anim = GetComponent<tk2dSpriteAnimator>();
	}

	private void Start()
	{
		float z = base.transform.eulerAngles.z;
		if (z >= 45f && z <= 135f)
		{
			type = CrawlerType.Wall;
			velocity = new Vector2(0f, Mathf.Sign(0f - base.transform.localScale.x) * speed);
		}
		else if (z >= 135f && z <= 225f)
		{
			type = ((base.transform.localScale.y > 0f) ? CrawlerType.Roof : CrawlerType.Floor);
			velocity = new Vector2(Mathf.Sign(base.transform.localScale.x) * speed, 0f);
		}
		else if (z >= 225f && z <= 315f)
		{
			type = CrawlerType.Wall;
			velocity = new Vector2(0f, Mathf.Sign(base.transform.localScale.x) * speed);
		}
		else
		{
			type = ((!(base.transform.localScale.y > 0f)) ? CrawlerType.Roof : CrawlerType.Floor);
			velocity = new Vector2(Mathf.Sign(0f - base.transform.localScale.x) * speed, 0f);
		}
		recoil.SetRecoilSpeed(0f);
		recoil.OnCancelRecoil += delegate
		{
			body.velocity = velocity;
		};
		switch (type)
		{
		case CrawlerType.Floor:
			body.gravityScale = 1f;
			recoil.freezeInPlace = false;
			break;
		case CrawlerType.Roof:
		case CrawlerType.Wall:
			body.gravityScale = 0f;
			recoil.freezeInPlace = true;
			break;
		}
		StartCoroutine(Walk());
	}

	private IEnumerator Walk()
	{
		while (true)
		{
			anim.Play("Walk");
			body.velocity = velocity;
			bool hit = false;
			while (!hit && !CheckRayLocal(wallCheck.localPosition, (transform.localScale.x > 0f) ? Vector2.left : Vector2.right, 1f) && CheckRayLocal(groundCheck.localPosition, (transform.localScale.y > 0f) ? Vector2.down : Vector2.up, 1f))
			{
				yield return null;
			}
			yield return StartCoroutine(Turn());
			yield return null;
		}
	}

	private IEnumerator Turn()
	{
		body.velocity = Vector2.zero;
		yield return StartCoroutine(anim.PlayAnimWait("Turn"));
		transform.SetScaleX(transform.localScale.x * -1f);
		velocity.x *= -1f;
		velocity.y *= -1f;
	}

	public bool CheckRayLocal(Vector2 originLocal, Vector2 directionLocal, float length)
	{
		Vector2 vector = base.transform.TransformPoint(originLocal);
		Vector2 vector2 = base.transform.TransformDirection(directionLocal);
		RaycastHit2D raycastHit2D = Physics2D.Raycast(vector, vector2, length, 256);
		Debug.DrawLine(vector, vector + vector2 * length);
		return raycastHit2D.collider != null;
	}
}
