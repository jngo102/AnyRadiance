using GlobalEnums;
using System.Collections;
using UnityEngine;
using Action = HKAIFramework.Action;

namespace AnyRadiance.Radiance.Actions
{
    internal class SummonMegaOrb : Action
    {
        private float _growRate;
        private float _killScale;
        
        public SummonMegaOrb(float growRate, float killScale)
        {
            _growRate = growRate;
            _killScale = killScale;
        }

        public override IEnumerator Execute()
        {
            var animator = Radiance.Self.GetComponent<tk2dSpriteAnimator>();
            Transform transform = Radiance.Self.transform;
            
            animator.Play("Cast");

            Vector2 spawnPos = transform.position + Vector3.up * 10;
            AnyRadiance.Instance.GameObjects["Shot Charge"].transform.position = spawnPos;
            AnyRadiance.Instance.GameObjects["Shot Charge"].GetComponent<ParticleSystem>().Play();
            AnyRadiance.Instance.AudioClips["Ghost"].PlayOneShot(transform.position);

            GameObject ultraOrb = AnyRadiance.Instance.GameObjects["Orb"].Spawn(spawnPos);
            Object.Destroy(ultraOrb.GetComponentInChildren<CircleCollider2D>());
            yield return new WaitUntil(() =>
            {
                ultraOrb.transform.localScale += Vector3.one * _growRate * Time.deltaTime;
                return ultraOrb.transform.localScale.x >= _killScale;
            });

            AnyRadiance.Instance.AudioClips["Final Hit"].PlayOneShot(HeroController.instance.transform.position);
            iTween.FadeTo(AnyRadiance.Instance.GameObjects["White Fader"], 1, 0.25f);
            yield return new WaitForSeconds(1);
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
            collider.radius = 100;
            killer.transform.SetPosition2D(HeroController.instance.transform.position);
        }
    }
}
