using System.Collections;
using UnityEngine;
using Action = HKAIFramework.Action;
using Random = UnityEngine.Random;

namespace AnyRadiance.Radiance.Actions
{
    internal class SummonBeamOrb : Action
    {
        private readonly bool _tracking;
        private readonly bool _bouncer;
        private readonly float _spinSpeed;
        private readonly float _trackingAcceleration;
        private readonly float _maxTrackingSpeed;
        private readonly int _numBeams;
        private readonly float _beamInterval;
        private readonly float _duration;
        private readonly bool _bounce;

        public SummonBeamOrb(bool tracking, bool bouncer, float spinSpeed, float trackingAcceleration,
            float maxTrackingSpeed, int numBeams, float beamInterval, float duration, bool bounce = false)
        {
            _tracking = tracking;
            _bouncer = bouncer;
            _spinSpeed = spinSpeed;
            _trackingAcceleration = trackingAcceleration;
            _maxTrackingSpeed = maxTrackingSpeed;
            _numBeams = numBeams;
            _beamInterval = beamInterval;
            _duration = duration;
            _bounce = bounce;
        }

        public override IEnumerator Execute()
        {
            Transform transform = Radiance.Self.transform;

            var spawnPos = new Vector3(
                Random.Range(ArenaInfo.CurrentLeft, ArenaInfo.CurrentRight),
                Random.Range(ArenaInfo.CurrentBottom + 2, ArenaInfo.CurrentTop - 21),
                0);

            AnyRadiance.Instance.GameObjects["Shot Charge"].transform.position = spawnPos;
            AnyRadiance.Instance.GameObjects["Shot Charge"].GetComponent<ParticleSystem>().Play();
            AnyRadiance.Instance.AudioClips["Ghost"].PlayOneShot(transform.position);

            yield return null;

            GameObject orb = AnyRadiance.Instance.GameObjects["Orb"].Spawn(spawnPos);
            orb.name = "Beam Orb";
            var orbComponent = orb.AddComponent<BeamOrb>();
            orbComponent.SetParams(_tracking, _spinSpeed, _trackingAcceleration, _maxTrackingSpeed, _numBeams, _beamInterval, _duration);
            if (_bounce)
            {
                orb.name += " Bouncer";
                orb.GetComponent<Collider2D>().isTrigger = false;
                orb.AddComponent<Bouncer>().Force = 1;
                orb.AddComponent<EnemyDamager>().DamageAmount = 50;
            }
        }
    }
}
