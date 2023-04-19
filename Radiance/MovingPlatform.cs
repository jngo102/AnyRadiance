using System.Linq;
using UnityEngine;

namespace AnyRadiance.Radiance
{
    [RequireComponent(typeof(Renderer))]
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
                // Prevent bug where hero gets launched off platform upon hazard respawn
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

        /// <summary>
        /// Set the moving platform's velocity.
        /// </summary>
        /// <param name="velocity">The platform's velocity.</param>
        public void SetVelocity(Vector2 velocity) => _rigidbody.velocity = velocity;
    }
}
