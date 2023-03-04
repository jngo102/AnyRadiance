using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AnyRadiance
{
    internal class MovingPlatform : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        
        private void Awake()
        {
            var renderer = GetComponent<MeshRenderer>();
            
            _rigidbody = gameObject.AddComponent<Rigidbody2D>();
            _rigidbody.isKinematic = true;
            _rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
            _rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            _rigidbody.sharedMaterial = new PhysicsMaterial2D { friction = 5 };

            GameObject spikePrefab = AnyRadiance.Instance.GameObjects["Spike"];
            Bounds bounds = renderer.bounds;
            for (float i = bounds.min.x; i < bounds.max.x; i += (bounds.max.x - bounds.min.x) / 4)
            {
                var spike = spikePrefab.Spawn(transform);
                spike.name = "Moving Plat Spike(" + i + ")";
                spike.transform.localPosition = new Vector2(i - bounds.center.x + 0.25f, bounds.max.y - bounds.center.y + 1);
                Destroy(spike.LocateMyFSM("Hero Saver"));
                spike.GetComponent<DamageHero>().damageDealt = 2;
            }
        }
        
        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.name == "Knight")
            {
                if (other.contacts.Any(contact => contact.normal.y < -1))
                {
                    return;
                }

                var heroController = other.gameObject.GetComponent<HeroController>();
                heroController.SetConveyorSpeed(_rigidbody.velocity.x);
                heroController.SetConveyorSpeedV(_rigidbody.velocity.y);
                heroController.cState.onConveyor = heroController.cState.onConveyorV = true;
                if (heroController.cState.hazardRespawning)
                {
                    other.rigidbody.velocity = Vector2.zero;
                }
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.name == "Knight")
            {
                var cState = other.gameObject.GetComponent<HeroController>().cState;
                cState.onConveyor = cState.onConveyorV = false;
            }
        }
    }
}
