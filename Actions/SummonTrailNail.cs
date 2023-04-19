using AnyRadiance.Radiance;
using System.Collections;
using UnityEngine;
using Action = HKAIFramework.Action;
using UObject = UnityEngine.Object;

namespace AnyRadiance.Actions
{
    internal class SummonTrailNail : Action
    {
        private float _preTravelPause;
        private float _rotateTime;
        private float _travelSpeed;
        private float _travelTime;

        public SummonTrailNail(float preTravelPause, float rotateTime, float travelSpeed, float travelTime)
        {
            _preTravelPause = preTravelPause;
            _rotateTime = rotateTime;
            _travelSpeed = travelSpeed;
            _travelTime = travelTime;
        }

        public override IEnumerator Execute()
        {
            var nail = UObject.Instantiate(AnyRadiance.Instance.GameObjects["Nail"],
                Radiance.Radiance.Self.transform.position, Quaternion.identity);
            var trailNail = nail.AddComponent<TrailNail>();
            trailNail.PreTravelPause = _preTravelPause;
            trailNail.RotateTime = _rotateTime;
            trailNail.TravelSpeed = _travelSpeed;
            trailNail.TravelTime = _travelTime;
            trailNail.StartFollow();
            yield return null;
        }
    }
}
