using UnityEngine;

public class LookAnimNPC : MonoBehaviour
{
	private enum AnimState
	{
		Left,
		TurningLeft,
		Right,
		TurningRight
	}

	public string leftAnim;

	public string rightAnim;

	public bool defaultLeft = true;

	public float centreOffset;

	[Tooltip("Limit's behaviour if further than this from the hero's Z. Leave 0 for no limit.")]
	public float limitZ;

	public TriggerEnterEvent enterDetector;

	public TriggerEnterEvent exitDetector;

	private Transform target;

	private AnimState state;

	private float turnFinishTime;

	private bool isTurning;

	private tk2dSpriteAnimator anim;

	private void Awake()
	{
		anim = GetComponent<tk2dSpriteAnimator>();
		if (!anim)
		{
			Debug.LogError($"LookAnimNPC on {base.gameObject.name} could not find a tk2dSpriteAnimator!", this);
		}
	}

	private void Start()
	{
		if (limitZ > 0f && Mathf.Abs(base.transform.position.z - 0.004f) > limitZ)
		{
			Object.Destroy(this);
			return;
		}
		state = ((!defaultLeft) ? AnimState.Right : AnimState.Left);
		if ((bool)anim)
		{
			tk2dSpriteAnimationClip clipByName = anim.GetClipByName(defaultLeft ? leftAnim : rightAnim);
			anim.PlayFromFrame(clipByName, clipByName.frames.Length - 1);
		}
		if ((bool)enterDetector)
		{
			enterDetector.OnTriggerEntered += delegate(Collider2D collider, GameObject sender)
			{
				target = collider.transform;
			};
		}
		if ((bool)exitDetector)
		{
			exitDetector.OnTriggerExited += delegate
			{
				target = null;
			};
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(base.transform.position + new Vector3(centreOffset, 0f, 0f), 0.25f);
	}

	private void Update()
	{
		if (!anim)
		{
			return;
		}
		if (!isTurning)
		{
			bool flag = (target ? ((target.transform.position.x - (base.transform.position.x + centreOffset)) * base.transform.localScale.x < 0f) : defaultLeft);
			switch (state)
			{
			case AnimState.Left:
				if (!flag)
				{
					float num2 = anim.PlayAnimGetTime(rightAnim);
					state = AnimState.TurningRight;
					isTurning = true;
					turnFinishTime = Time.time + num2;
				}
				break;
			case AnimState.Right:
				if (flag)
				{
					float num = anim.PlayAnimGetTime(leftAnim);
					state = AnimState.TurningLeft;
					isTurning = true;
					turnFinishTime = Time.time + num;
				}
				break;
			}
		}
		if (isTurning && Time.time >= turnFinishTime)
		{
			isTurning = false;
			switch (state)
			{
			case AnimState.TurningLeft:
				state = AnimState.Left;
				break;
			case AnimState.TurningRight:
				state = AnimState.Right;
				break;
			}
		}
	}
}
