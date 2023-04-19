using UnityEngine;

namespace AnyRadiance.Radiance
{
    [RequireComponent(typeof(Rigidbody2D))]
    internal class BackAndForthFollow : MonoBehaviour
    {
        private const float SwitchThresold = 1; 
        private const float Acceleration = 5;
        private const float MaxSpeed = 8;

        private Rigidbody2D _rigidbody;
        private bool _goingRight = true;

        private void Awake()
        {
            _rigidbody ??= GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            float distanceX = (_goingRight ? ArenaInfo.CurrentRight : ArenaInfo.CurrentLeft) - transform.position.x;
            if (Mathf.Abs(distanceX) < SwitchThresold)
            {
                _goingRight = !_goingRight;
            }
            var distanceY = HeroController.instance.transform.position.y - transform.position.y;
            var velX = distanceX * Acceleration * Time.fixedDeltaTime;
            var velY = distanceY * Acceleration * Time.fixedDeltaTime;
            _rigidbody.velocity += new Vector2(velX, velY);
            _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, MaxSpeed);
        }
    }
}
