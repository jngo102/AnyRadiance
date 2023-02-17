using System.Collections;
using UnityEngine;

public class MegaJellyZap : MonoBehaviour
{
	public enum Type
	{
		Zap,
		MultiZap
	}

	public Type type;

	public ParticleSystem ptAttack;

	public ParticleSystem ptAntic;

	public AudioSource audioSourcePrefab;

	public AudioEvent zapBugPt1;

	public AudioEvent zapBugPt2;

	public tk2dSpriteAnimator anim;

	private MeshRenderer animMesh;

	private CircleCollider2D col;

	private ColorFader fade;

	private Coroutine routine;

	private void Awake()
	{
		col = GetComponent<CircleCollider2D>();
		fade = GetComponentInChildren<ColorFader>();
		if ((bool)anim)
		{
			animMesh = anim.GetComponent<MeshRenderer>();
		}
	}

	private void OnEnable()
	{
		routine = StartCoroutine((type == Type.Zap) ? ZapSequence() : MultiZapSequence());
	}

	private void OnDisable()
	{
		if (routine != null)
		{
			StopCoroutine(routine);
		}
	}

	private IEnumerator ZapSequence()
	{
		col.enabled = false;
		ptAttack.Stop();
		ptAntic.Play();
		if ((bool)fade)
		{
			fade.Fade(up: true);
		}
		yield return new WaitForSeconds(0.5f);
		zapBugPt1.SpawnAndPlayOneShot(audioSourcePrefab, transform.position);
		col.enabled = true;
		ptAttack.Play();
		yield return new WaitForSeconds(1f);
		zapBugPt2.SpawnAndPlayOneShot(audioSourcePrefab, transform.position);
		if ((bool)fade)
		{
			fade.Fade(up: false);
		}
		ptAttack.Stop();
		ptAntic.Stop();
		col.enabled = false;
		yield return new WaitForSeconds(1f);
		gameObject.Recycle();
	}

	private IEnumerator MultiZapSequence()
	{
		animMesh.enabled = false;
		col.enabled = false;
		ptAttack.Stop();
		transform.SetScaleX((Random.Range(0, 2) == 0) ? 1 : (-1));
		transform.SetRotation2D(Random.Range(0f, 360f));
		yield return new WaitForSeconds(Random.Range(0f, 0.5f));
		anim.Play("Zap Antic");
		animMesh.enabled = true;
		yield return new WaitForSeconds(0.8f);
		col.enabled = true;
		ptAttack.Play();
		anim.Play("Zap");
		yield return new WaitForSeconds(1f);
		ptAttack.Stop();
		col.enabled = false;
		yield return StartCoroutine(anim.PlayAnimWait("Zap End"));
		animMesh.enabled = false;
		yield return new WaitForSeconds(0.5f);
		gameObject.SetActive(value: false);
	}
}
