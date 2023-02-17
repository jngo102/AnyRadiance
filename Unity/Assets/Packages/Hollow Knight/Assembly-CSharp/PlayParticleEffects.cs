using UnityEngine;

public class PlayParticleEffects : MonoBehaviour
{
	public ParticleSystem[] particleEffects;

	public void PlayParticleSystems()
	{
		for (int i = 0; i < particleEffects.Length; i++)
		{
			particleEffects[i].Play();
		}
	}
}
