using System.Collections;
using GlobalEnums;
using UnityEngine;

namespace AnyRadiance.Radiance
{
    internal class Bouncer : MonoBehaviour, IHitResponder
    {
        public float Force = 1;

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            gameObject.layer = (int)PhysLayers.ENEMIES;

            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Hit(HitInstance damageInstance)
        {
            HeroController.instance.SoulGain();
            var direction = Vector2.right;
            switch (damageInstance.Direction)
            {
                case 90:
                    direction = Vector2.up;
                    break;
                case 180:
                    direction = Vector2.left;
                    break;
                case 270:
                    direction = Vector2.down;
                    break;
            }
            
            _rigidbody.velocity += direction * Force * damageInstance.DamageDealt;

            if (GetComponent<BeamOrb>())
            {
                IEnumerator TempDisableTracking(float duration)
                {
                    var beamOrb = GetComponent<BeamOrb>();
                    var damager = GetComponent<EnemyDamager>();
                    beamOrb.Tracking = false;
                    damager?.StartDamager();
                    yield return new WaitForSeconds(duration);
                    beamOrb.Tracking = true;
                    damager?.StopDamager();
                }

                StartCoroutine(TempDisableTracking(0.5f));
            }
        }
    }
}
