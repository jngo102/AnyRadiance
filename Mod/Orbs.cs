using System.Collections;
using Modding.Utils;
using UnityEngine;

namespace AnyRadiance
{
    internal partial class Radiance
    {
        private IEnumerator SummonMegaOrb()
        {
            var spawnPos = new Vector3(
                Random.Range(ArenaInfo.A1Left, ArenaInfo.A1Right),
                Random.Range(ArenaInfo.A1Bottom + 2, ArenaInfo.A1Top - 21),
                0);

            AnyRadiance.Instance.GameObjects["Shot Charge"].transform.position = spawnPos;
            AnyRadiance.Instance.GameObjects["Shot Charge"].GetComponent<ParticleSystem>().Play();
            PlayOneShot(AnyRadiance.Instance.AudioClips["Ghost"], spawnPos);

            GameObject orb = AnyRadiance.Instance.GameObjects["Orb"].Spawn(spawnPos);
            var orbComponent = orb.GetOrAddComponent<MegaOrb>();
            yield return new WaitUntil(() => orbComponent.Following);
            PlayOneShot(AnyRadiance.Instance.AudioClips["Projectile"], spawnPos);
        }

        public IEnumerator SummonBeamOrb()
        {
            var spawnPos = new Vector3(
                Random.Range(ArenaInfo.A1Left, ArenaInfo.A1Right),
                Random.Range(ArenaInfo.A1Bottom + 2, ArenaInfo.A1Top - 21),
                0);

            AnyRadiance.Instance.GameObjects["Shot Charge"].transform.position = spawnPos;
            AnyRadiance.Instance.GameObjects["Shot Charge"].GetComponent<ParticleSystem>().Play();
            PlayOneShot(AnyRadiance.Instance.AudioClips["Ghost"], spawnPos);

            yield return null;

            GameObject orb = AnyRadiance.Instance.GameObjects["Orb"].Spawn(spawnPos);
            orb.AddComponent<BeamOrb>();
        }
    }
}
