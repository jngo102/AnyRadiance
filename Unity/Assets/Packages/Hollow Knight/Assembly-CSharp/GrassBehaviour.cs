using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassBehaviour : MonoBehaviour
{
	[Header("Animation")]
	public float walkReactAmount = 1f;

	public AnimationCurve walkReactCurve;

	public float walkReactLength;

	[Space]
	public float attackReactAmount = 2f;

	public AnimationCurve attackReactCurve;

	public float attackReactLength;

	[Space]
	public string pushAnim = "Push";

	private Animator animator;

	[Header("Sound")]
	public AudioClip[] pushSounds;

	public AudioClip[] cutPushSounds;

	public AudioClip[] cutSounds;

	private AudioSource source;

	[Header("Extra")]
	public Color infectedColor = Color.white;

	public bool neverInfected;

	private static bool colorSet = false;

	private AnimationCurve curve;

	private float animLength = 2f;

	private float animElapsed;

	private float pushAmount = 1f;

	private float pushDirection;

	private bool returned = true;

	private bool cutFirstFrame;

	private bool isCut;

	private float pushAmountError;

	private Rigidbody2D player;

	private Vector3 oldPlayerPos;

	private SpriteRenderer[] renderers;

	private static Dictionary<string, Material> sharedMaterials = new Dictionary<string, Material>();

	private static int grassCount = 0;

	private Material sharedMaterial;

	private MaterialPropertyBlock propertyBlock;

	public Material SharedMaterial => sharedMaterial;

	private void Awake()
	{
		source = GetComponent<AudioSource>();
		animator = GetComponentInChildren<Animator>();
		propertyBlock = new MaterialPropertyBlock();
	}

	private void Start()
	{
		if (Mathf.Abs(base.transform.position.z - 0.004f) > 1.8f)
		{
			GrassCut component = GetComponent<GrassCut>();
			if ((bool)component)
			{
				Object.Destroy(component);
			}
			Collider2D[] componentsInChildren = GetComponentsInChildren<Collider2D>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Object.Destroy(componentsInChildren[i]);
			}
		}
		renderers = GetComponentsInChildren<SpriteRenderer>(includeInactive: true);
		if (renderers.Length != 0)
		{
			string key = renderers[0].material.name + (neverInfected ? "_neverInfected" : "");
			if (sharedMaterials.ContainsKey(key))
			{
				sharedMaterial = sharedMaterials[key];
			}
			if (!sharedMaterial)
			{
				sharedMaterial = new Material(renderers[0].material);
				sharedMaterials[key] = sharedMaterial;
			}
		}
		SpriteRenderer[] array;
		if ((bool)sharedMaterial)
		{
			array = renderers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].sharedMaterial = sharedMaterial;
			}
		}
		if (!colorSet && !neverInfected)
		{
			StartCoroutine(DelayedInfectedCheck());
		}
		pushAmountError = Random.Range(-0.01f, 0.01f);
		array = renderers;
		foreach (Renderer rend in array)
		{
			SetPushAmount(rend, pushAmountError);
		}
		base.transform.SetPositionZ(base.transform.position.z + Random.Range(-0.0001f, 0.0001f));
	}

	private void OnEnable()
	{
		grassCount++;
	}

	private void OnDisable()
	{
		grassCount--;
		if (colorSet)
		{
			colorSet = false;
			sharedMaterial = null;
		}
		if (grassCount <= 0)
		{
			sharedMaterials.Clear();
		}
	}

	private IEnumerator DelayedInfectedCheck()
	{
		yield return null;
		if ((bool)sharedMaterial && (bool)GameObject.FindWithTag("Infected Flag"))
		{
			colorSet = true;
			sharedMaterial.color = infectedColor;
			SpriteRenderer[] array = renderers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].sharedMaterial = sharedMaterial;
			}
		}
	}

	private void LateUpdate()
	{
		if (returned)
		{
			return;
		}
		float value = curve.Evaluate(animElapsed / animLength) * pushAmount * pushDirection * Mathf.Sign(base.transform.localScale.x) + pushAmountError;
		SpriteRenderer[] array = renderers;
		foreach (SpriteRenderer rend in array)
		{
			SetPushAmount(rend, value);
		}
		if (animElapsed >= animLength)
		{
			returned = true;
			if ((bool)animator && animator.HasParameter(pushAnim, AnimatorControllerParameterType.Bool))
			{
				animator.SetBool(pushAnim, value: false);
			}
		}
		animElapsed += Time.deltaTime;
	}

	private void FixedUpdate()
	{
		if (!player || !returned || !(Mathf.Abs(player.velocity.x) >= 0.1f))
		{
			return;
		}
		pushDirection = Mathf.Sign(player.velocity.x);
		returned = false;
		animElapsed = 0f;
		pushAmount = walkReactAmount;
		curve = walkReactCurve;
		animLength = walkReactLength;
		PlayRandomSound(isCut ? cutPushSounds : pushSounds);
		if ((bool)animator)
		{
			if (animator.HasParameter(pushAnim, AnimatorControllerParameterType.Bool))
			{
				animator.SetBool(pushAnim, value: true);
			}
			else if (animator.HasParameter(pushAnim, AnimatorControllerParameterType.Trigger))
			{
				animator.SetTrigger(pushAnim);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!returned)
		{
			return;
		}
		pushDirection = Mathf.Sign(base.transform.position.x - collision.transform.position.x);
		returned = false;
		animElapsed = 0f;
		if (GrassCut.ShouldCut(collision))
		{
			pushAmount = attackReactAmount;
			curve = attackReactCurve;
			animLength = attackReactLength;
			PlayRandomSound(isCut ? cutPushSounds : pushSounds);
		}
		else
		{
			pushAmount = walkReactAmount;
			curve = walkReactCurve;
			animLength = walkReactLength;
			if (collision.tag == "Player")
			{
				player = collision.GetComponent<Rigidbody2D>();
			}
			PlayRandomSound(isCut ? cutPushSounds : pushSounds);
		}
		if ((bool)animator && animator.HasParameter(pushAnim))
		{
			animator.SetBool(pushAnim, value: true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			player = null;
		}
	}

	public void CutReact(Collider2D collision)
	{
		if (!isCut)
		{
			pushDirection = Mathf.Sign(base.transform.position.x - collision.transform.position.x);
			returned = false;
			animElapsed = 0f;
			cutFirstFrame = true;
			pushAmount = attackReactAmount;
			curve = attackReactCurve;
			animLength = attackReactLength;
		}
		PlayRandomSound(cutSounds);
		isCut = true;
	}

	public void WindReact(Collider2D collision)
	{
		if (returned)
		{
			pushDirection = Mathf.Sign(base.transform.position.x - collision.transform.position.x);
			returned = false;
			animElapsed = 0f;
			pushAmount = walkReactAmount;
			curve = walkReactCurve;
			animLength = walkReactLength;
			PlayRandomSound(isCut ? cutPushSounds : pushSounds);
		}
	}

	private void PlayRandomSound(AudioClip[] clips)
	{
		if ((bool)source && clips.Length != 0)
		{
			AudioClip clip = clips[Random.Range(0, clips.Length)];
			source.PlayOneShot(clip);
		}
	}

	private void SetPushAmount(Renderer rend, float value)
	{
		rend.GetPropertyBlock(propertyBlock);
		propertyBlock.SetFloat("_PushAmount", value);
		rend.SetPropertyBlock(propertyBlock);
	}
}
