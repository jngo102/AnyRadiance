using GlobalEnums;
using UnityEngine;

namespace AnyRadiance
{
    internal class Bouncer : MonoBehaviour, IHitResponder
    {
        public float Force = 2500;

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            gameObject.layer = (int)PhysLayers.ENEMIES;
            
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody.isKinematic = false;
        }

        public void Hit(HitInstance damageInstance)
        {
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
            
            _rigidbody.AddForce(direction * Force, ForceMode2D.Force);
        }
    }
}
