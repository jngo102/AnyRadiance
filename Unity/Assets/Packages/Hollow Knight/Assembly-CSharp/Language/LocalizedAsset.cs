using UnityEngine;

namespace Language
{
	
	public class LocalizedAsset : MonoBehaviour
	{
		public Object localizeTarget;
	
		public void Awake()
		{
			LocalizeAsset(localizeTarget);
		}
	
		public void LocalizeAsset()
		{
			LocalizeAsset(localizeTarget);
		}
	
		public static void LocalizeAsset(Object target)
		{
			if (target == null)
			{
				Debug.LogError("LocalizedAsset target is null");
			}
			else if (target.GetType() == typeof(Material))
			{
				Material material = (Material)target;
				if (material.mainTexture != null)
				{
					Texture texture = (Texture)Language.GetAsset(material.mainTexture.name);
					if (texture != null)
					{
						material.mainTexture = texture;
					}
				}
			}
			else if (target.GetType() == typeof(MeshRenderer))
			{
				MeshRenderer meshRenderer = (MeshRenderer)target;
				if (meshRenderer.material.mainTexture != null)
				{
					Texture texture2 = (Texture)Language.GetAsset(meshRenderer.material.mainTexture.name);
					if (texture2 != null)
					{
						meshRenderer.material.mainTexture = texture2;
					}
				}
			}
			else
			{
				Debug.LogError("Could not localize this object type: " + target.GetType());
			}
		}
	}
}