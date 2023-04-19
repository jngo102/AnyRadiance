using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GlobalEnums;
using UnityEngine;

namespace AnyRadiance.Radiance
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    internal class EnemyDamager : MonoBehaviour
    {
        public int DamageAmount = 1;
        private Rigidbody2D _rigidbody;

        private int _origLayer;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();

            _origLayer = gameObject.layer;
        }

        public void StartDamager()
        {
            gameObject.layer = (int)PhysLayers.ENEMY_DETECTOR;
        }

        public void StopDamager()
        {
            gameObject.layer = _origLayer;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<HealthManager>())
            {
                float attackDirection = 0;
                if (Mathf.Abs(_rigidbody.velocity.x) > Mathf.Abs(_rigidbody.velocity.y))
                {
                    attackDirection = _rigidbody.velocity.x > 0 ? 0 : 180;
                }
                else
                {
                    attackDirection = _rigidbody.velocity.y > 0 ? 90 : 270;
                }

                var takeDamage = typeof(HealthManager).GetMethod("TakeDamage", BindingFlags.NonPublic | BindingFlags.Instance);
                takeDamage?.Invoke(other.GetComponent<HealthManager>(), new[] { new HitInstance
                {
                    AttackType = AttackTypes.Generic,
                    DamageDealt = DamageAmount,
                    Direction = attackDirection,
                    IgnoreInvulnerable = true,
                    Multiplier = 1,
                    Source = gameObject,
                } as object });
            }
        }
    }
}
