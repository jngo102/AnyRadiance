using UnityEngine;

[RequireComponent(typeof(GameCameras))]
public class LightBlurredBackground : MonoBehaviour
{
	[SerializeField]
	private float distantFarClipPlane;

	[SerializeField]
	private int renderTextureHeight;

	[SerializeField]
	private Material blitMaterial;

	[SerializeField]
	private float clipEpsilon;

	[SerializeField]
	private LayerMask blurPlaneLayer;

	private GameCameras gameCameras;

	private Camera sceneCamera;

	private Camera backgroundCamera;

	private RenderTexture renderTexture;

	private Material blurMaterialInstance;

	private Material blitMaterialInstance;

	private LightBlur lightBlur;

	private int passGroupCount;

	private static readonly int _vibrancyProp = Shader.PropertyToID("_Vibrancy");

	private static readonly int _blurPlaneVibranceProp = Shader.PropertyToID("_BlurPlaneVibrance");

	public int RenderTextureHeight
	{
		get
		{
			return renderTextureHeight;
		}
		set
		{
			renderTextureHeight = value;
		}
	}

	public int PassGroupCount
	{
		get
		{
			return passGroupCount;
		}
		set
		{
			passGroupCount = value;
			if (lightBlur != null)
			{
				lightBlur.PassGroupCount = passGroupCount;
			}
		}
	}

	protected void Awake()
	{
		gameCameras = GetComponent<GameCameras>();
		sceneCamera = gameCameras.tk2dCam.GetComponent<Camera>();
		passGroupCount = 2;
	}

	protected void OnEnable()
	{
		distantFarClipPlane = sceneCamera.farClipPlane;
		GameObject gameObject = new GameObject("BlurCamera");
		gameObject.transform.SetParent(sceneCamera.transform);
		backgroundCamera = gameObject.AddComponent<Camera>();
		backgroundCamera.CopyFrom(sceneCamera);
		backgroundCamera.farClipPlane = distantFarClipPlane;
		backgroundCamera.cullingMask &= ~blurPlaneLayer.value;
		backgroundCamera.depth -= 5f;
		backgroundCamera.rect = new Rect(0f, 0f, 1f, 1f);
		lightBlur = gameObject.AddComponent<LightBlur>();
		lightBlur.PassGroupCount = passGroupCount;
		UpdateCameraClipPlanes();
		blitMaterialInstance = new Material(blitMaterial);
		blitMaterialInstance.EnableKeyword("BLUR_PLANE");
		OnCameraAspectChanged(ForceCameraAspect.CurrentViewportAspect);
		ForceCameraAspect.ViewportAspectChanged += OnCameraAspectChanged;
		OnBlurPlanesChanged();
		BlurPlane.BlurPlanesChanged += OnBlurPlanesChanged;
	}

	private void OnCameraAspectChanged(float aspect)
	{
		if (aspect <= Mathf.Epsilon)
		{
			return;
		}
		int num = Mathf.RoundToInt((float)renderTextureHeight * aspect);
		if (num > 0)
		{
			if (renderTexture != null)
			{
				Object.Destroy(renderTexture);
			}
			renderTexture = new RenderTexture(num, renderTextureHeight, 16, RenderTextureFormat.Default);
			backgroundCamera.targetTexture = renderTexture;
			blitMaterialInstance.mainTexture = renderTexture;
		}
	}

	protected void OnDisable()
	{
		ForceCameraAspect.ViewportAspectChanged -= OnCameraAspectChanged;
		BlurPlane.BlurPlanesChanged -= OnBlurPlanesChanged;
		for (int i = 0; i < BlurPlane.BlurPlaneCount; i++)
		{
			BlurPlane blurPlane = BlurPlane.GetBlurPlane(i);
			blurPlane.SetPlaneMaterial(null);
			blurPlane.SetPlaneVisibility(isVisible: true);
		}
		Object.Destroy(blitMaterialInstance);
		blitMaterialInstance = null;
		lightBlur = null;
		backgroundCamera.targetTexture = null;
		Object.Destroy(renderTexture);
		renderTexture = null;
		sceneCamera.farClipPlane = distantFarClipPlane;
		Object.DestroyObject(backgroundCamera.gameObject);
		backgroundCamera = null;
	}

	private void OnBlurPlanesChanged()
	{
		for (int i = 0; i < BlurPlane.BlurPlaneCount; i++)
		{
			BlurPlane blurPlane = BlurPlane.GetBlurPlane(i);
			blurPlane.SetPlaneVisibility(isVisible: true);
			blurPlane.SetPlaneMaterial(blitMaterialInstance);
			float @float = blurPlane.OriginalMaterial.GetFloat(_vibrancyProp);
			blitMaterialInstance.SetFloat(_blurPlaneVibranceProp, @float);
		}
		UpdateCameraClipPlanes();
	}

	protected void LateUpdate()
	{
		UpdateCameraClipPlanes();
	}

	private void UpdateCameraClipPlanes()
	{
		BlurPlane closestBlurPlane = BlurPlane.ClosestBlurPlane;
		if (closestBlurPlane != null)
		{
			sceneCamera.farClipPlane = closestBlurPlane.PlaneZ - sceneCamera.transform.GetPositionZ() + clipEpsilon;
			backgroundCamera.nearClipPlane = closestBlurPlane.PlaneZ - backgroundCamera.transform.GetPositionZ() + clipEpsilon;
		}
		else
		{
			sceneCamera.farClipPlane = distantFarClipPlane;
			backgroundCamera.nearClipPlane = distantFarClipPlane;
		}
	}
}
