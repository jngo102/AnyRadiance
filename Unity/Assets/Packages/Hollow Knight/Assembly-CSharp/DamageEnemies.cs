using System.Collections.Generic;
using UnityEngine;

public class DamageEnemies : MonoBehaviour
{
	public AttackTypes attackType = AttackTypes.Generic;

	public bool circleDirection;

	public int damageDealt;

	public float direction;

	public bool ignoreInvuln = true;

	public float magnitudeMult;

	public bool moveDirection;

	public SpecialTypes specialType;

	private List<Collider2D> enteredColliders = new List<Collider2D>();

	private void Reset()
	{
		PlayMakerFSM[] components = GetComponents<PlayMakerFSM>();
		foreach (PlayMakerFSM playMakerFSM in components)
		{
			if (playMakerFSM.FsmName == "damages_enemy")
			{
				attackType = (AttackTypes)playMakerFSM.FsmVariables.GetFsmInt("attackType").Value;
				circleDirection = playMakerFSM.FsmVariables.GetFsmBool("circleDirection").Value;
				damageDealt = playMakerFSM.FsmVariables.GetFsmInt("damageDealt").Value;
				direction = playMakerFSM.FsmVariables.GetFsmFloat("direction").Value;
				ignoreInvuln = playMakerFSM.FsmVariables.GetFsmBool("Ignore Invuln").Value;
				magnitudeMult = playMakerFSM.FsmVariables.GetFsmFloat("magnitudeMult").Value;
				moveDirection = playMakerFSM.FsmVariables.GetFsmBool("moveDirection").Value;
				specialType = (SpecialTypes)playMakerFSM.FsmVariables.GetFsmInt("Special Type").Value;
				break;
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		DoDamage(collision.gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (base.enabled)
		{
			int layer = collision.gameObject.layer;
			if (layer != 20 && layer != 9 && layer != 26 && layer != 31 && !collision.CompareTag("Geo") && !enteredColliders.Contains(collision))
			{
				enteredColliders.Add(collision);
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (enteredColliders.Contains(collision))
		{
			enteredColliders.Remove(collision);
		}
	}

	private void OnDisable()
	{
		enteredColliders.Clear();
	}

	private void FixedUpdate()
	{
		for (int num = enteredColliders.Count - 1; num >= 0; num--)
		{
			Collider2D collider2D = enteredColliders[num];
			if (collider2D == null || !collider2D.isActiveAndEnabled)
			{
				enteredColliders.RemoveAt(num);
			}
			else
			{
				DoDamage(collider2D.gameObject);
			}
		}
	}

	private void DoDamage(GameObject target)
	{
		if (damageDealt > 0)
		{
			FSMUtility.SendEventToGameObject(target, "TAKE DAMAGE");
			HitTaker.Hit(target, new HitInstance
			{
				Source = base.gameObject,
				AttackType = attackType,
				CircleDirection = circleDirection,
				DamageDealt = damageDealt,
				Direction = direction,
				IgnoreInvulnerable = ignoreInvuln,
				MagnitudeMultiplier = magnitudeMult,
				MoveAngle = 0f,
				MoveDirection = moveDirection,
				Multiplier = 1f,
				SpecialType = specialType,
				IsExtraDamage = false
			});
		}
	}
}
