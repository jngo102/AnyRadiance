using System;
using System.Collections;
using GlobalEnums;
using Modding.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AnyRadiance
{
    internal partial class Radiance
    {
        private IEnumerator SummonMegaOrb(float followTime, float minScale, float trackingAcceleration, float maxTrackingSpeed, float scale, float maxSpeedIncrease, float trackAccelIncrease, float scaleDivider)
        {
            var spawnPos = new Vector3(
                Random.Range(ArenaInfo.CurrentLeft, ArenaInfo.CurrentRight),
                Random.Range(ArenaInfo.CurrentBottom + 2, ArenaInfo.CurrentTop - 21),
                0);

            AnyRadiance.Instance.GameObjects["Shot Charge"].transform.position = spawnPos;
            AnyRadiance.Instance.GameObjects["Shot Charge"].GetComponent<ParticleSystem>().Play();
            AnyRadiance.Instance.AudioClips["Ghost"].PlayOneShot(transform.position);

            GameObject orb = AnyRadiance.Instance.GameObjects["Orb"].Spawn(spawnPos);
            var orbComponent = orb.GetOrAddComponent<MegaOrb>();
            orbComponent.SetParams(followTime, minScale, trackingAcceleration, maxTrackingSpeed, scale, maxSpeedIncrease, trackAccelIncrease, scaleDivider);
            yield return new WaitUntil(() => orbComponent.Following);
            AnyRadiance.Instance.AudioClips["Projectile"].PlayOneShot(transform.position);
        }

        private IEnumerator SummonBeamOrb(bool bouncer, float bounceForce, float spinSpeed, float trackingAcceleration, float maxTrackingSpeed, int numBeams, float beamInterval, float duration, bool bounce = false)
        {
            var spawnPos = new Vector3(
                Random.Range(ArenaInfo.CurrentLeft, ArenaInfo.CurrentRight),
                Random.Range(ArenaInfo.CurrentBottom + 2, ArenaInfo.CurrentTop - 21),
                0);

            AnyRadiance.Instance.GameObjects["Shot Charge"].transform.position = spawnPos;
            AnyRadiance.Instance.GameObjects["Shot Charge"].GetComponent<ParticleSystem>().Play();
            AnyRadiance.Instance.AudioClips["Ghost"].PlayOneShot(transform.position);

            yield return null;

            GameObject orb = AnyRadiance.Instance.GameObjects["Orb"].Spawn(spawnPos);
            var orbComponent = orb.AddComponent<BeamOrb>();
            orbComponent.SetParams(bouncer, bounceForce, spinSpeed, trackingAcceleration, maxTrackingSpeed, numBeams, beamInterval, duration);
            if (bounce)
            {
                orb.GetComponent<Collider2D>().isTrigger = false;
                orb.AddComponent<Bouncer>();
            }
        }

        private IEnumerator SummonUltraOrb(float killScale, float growRate)
        {
            _animator.Play("Cast");

            Vector2 spawnPos = transform.position + Vector3.up * 10;
            AnyRadiance.Instance.GameObjects["Shot Charge"].transform.position = spawnPos;
            AnyRadiance.Instance.GameObjects["Shot Charge"].GetComponent<ParticleSystem>().Play();
            AnyRadiance.Instance.AudioClips["Ghost"].PlayOneShot(transform.position);

            GameObject ultraOrb = AnyRadiance.Instance.GameObjects["Orb"].Spawn(spawnPos);
            Destroy(ultraOrb.GetComponentInChildren<CircleCollider2D>());
            yield return new WaitUntil(() =>
            {
                ultraOrb.transform.localScale += Vector3.one * growRate * Time.deltaTime;
                return ultraOrb.transform.localScale.x >= killScale;
            });

            AnyRadiance.Instance.AudioClips["Final Hit"].PlayOneShot(HeroController.instance.transform.position);
            iTween.FadeTo(AnyRadiance.Instance.GameObjects["White Fader"], 1, 0.25f);
            var heroController = HeroController.instance;
            heroController.RelinquishControl();
            heroController.IgnoreInput();

            var killer = new GameObject("Killer");
            killer.layer = (int)PhysLayers.ENEMIES;
            var damager = killer.AddComponent<DamageHero>();
            damager.damageDealt = int.MaxValue;
            damager.hazardType = (int)HazardType.SPIKES;
            var collider = killer.AddComponent<CircleCollider2D>();
            collider.isTrigger = true;
            collider.radius = 1;
            killer.transform.SetPosition2D(HeroController.instance.transform.position);

        }
    }
}
