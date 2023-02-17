using System.Collections.Generic;
using UnityEngine;

namespace CustomEnemyTest.Scripts
{
	[RequireComponent(typeof(MeshRenderer))]
	public class BlurPlane : MonoBehaviour
	{
		public delegate void BlurPlanesChangedDelegate();

		private MeshRenderer meshRenderer;

		private static List<BlurPlane> blurPlanes;

		public Material OriginalMaterial { get; private set; }

		public static int BlurPlaneCount => blurPlanes.Count;

		public static BlurPlane ClosestBlurPlane
		{
			get
			{
				if (blurPlanes.Count <= 0)
				{
					return null;
				}
				return blurPlanes[0];
			}
		}

		public float PlaneZ => base.transform.position.z;

		public static event BlurPlanesChangedDelegate BlurPlanesChanged;

		public static BlurPlane GetBlurPlane(int index)
		{
			return blurPlanes[index];
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Init()
		{
			blurPlanes = new List<BlurPlane>();
		}

		protected void Awake()
		{
			meshRenderer = GetComponent<MeshRenderer>();
			OriginalMaterial = meshRenderer.sharedMaterial;
		}

		protected void OnEnable()
		{
			int i;
			for (i = 0; i < blurPlanes.Count; i++)
			{
				BlurPlane blurPlane = blurPlanes[i];
				if (blurPlane.PlaneZ > blurPlane.PlaneZ)
				{
					break;
				}
			}
			blurPlanes.Insert(i, this);
			BlurPlane.BlurPlanesChanged?.Invoke();
		}

		protected void OnDisable()
		{
			blurPlanes.Remove(this);
			BlurPlane.BlurPlanesChanged?.Invoke();
		}

		public void SetPlaneVisibility(bool isVisible)
		{
			meshRenderer.enabled = isVisible;
		}

		public void SetPlaneMaterial(Material material)
		{
			meshRenderer.sharedMaterial = ((material == null) ? OriginalMaterial : material);
		}
	}
}