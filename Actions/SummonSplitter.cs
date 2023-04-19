using System.Collections;
using AnyRadiance.Radiance;
using Modding.Utils;
using UnityEngine;
using Action = HKAIFramework.Action;
using Random = UnityEngine.Random;

namespace AnyRadiance.Radiance.Actions
{
    internal class SummonSplitter : Action
    {
        private readonly float _followTime;
        private readonly float _minScale;
        private readonly float _trackingAcceleration;
        private readonly float _maxTrackingSpeed;
        private readonly float _scale;
        private readonly float _maxSpeedIncrease;
        private readonly float _trackAccelIncrease;
        private readonly float _scaleDivider;

        public SummonSplitter(float followTime, float minScale, float trackingAcceleration, float maxTrackingSpeed,
            float scale, float maxSpeedIncrease, float trackAccelIncrease, float scaleDivider)
        {
            _followTime = followTime;
            _minScale = minScale;
            _trackingAcceleration = trackingAcceleration;
            _maxTrackingSpeed = maxTrackingSpeed;
            _scale = scale;
            _maxSpeedIncrease = maxSpeedIncrease;
            _trackAccelIncrease = trackAccelIncrease;
            _scaleDivider = scaleDivider;
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

            GameObject orb = AnyRadiance.Instance.GameObjects["Orb"].Spawn(spawnPos);
            var orbComponent = orb.GetOrAddComponent<Splitter>();
            orbComponent.SetParams(_followTime, _minScale, _trackingAcceleration, _maxTrackingSpeed, _scale,
                _maxSpeedIncrease, _trackAccelIncrease, _scaleDivider);
            yield return new WaitUntil(() => orbComponent.Following);
            AnyRadiance.Instance.AudioClips["Projectile"].PlayOneShot(transform.position);
        }
    }
}
