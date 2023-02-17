using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PushableRubble : MonoBehaviour
{
	private Rigidbody2D body;

	protected void Awake()
	{
		body = GetComponent<Rigidbody2D>();
	}

	protected void OnTriggerEnter2D(Collider2D collider)
	{
		Push();
	}

	private void Push()
	{
		body.AddForce(new Vector2(Random.Range(-100, 100), Random.Range(0, 40)), ForceMode2D.Force);
		body.AddTorque(Random.Range(-50, 50), ForceMode2D.Force);
	}

	public void EndRubble()
	{
		Invoke("EndRubbleContinuation", 5f);
	}

	private void EndRubbleContinuation()
	{
		body.isKinematic = true;
		body.velocity = Vector2.zero;
		Collider2D component = GetComponent<Collider2D>();
		if (component != null)
		{
			component.enabled = false;
		}
	}
}
