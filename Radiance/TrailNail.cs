using System.Collections;
using GlobalEnums;
using UnityEngine;

namespace AnyRadiance.Radiance
{
    [RequireComponent(typeof(Rigidbody2D))]
    internal class TrailNail : MonoBehaviour
    {
        public float PreTravelPause = 0.5f;
        public float RotateTime = 1;
        public float TravelSpeed = 10;
        public float TravelTime = 2;

        private Rigidbody2D _rigidbody;
        private Coroutine _logic;
        private ParticleSystem _idlePt;

        private void Awake()
        {
            _rigidbody ??= GetComponent<Rigidbody2D>();
            _idlePt ??= GetComponentInChildren<ParticleSystem>();

            GetComponent<DamageHero>().damageDealt = int.MaxValue;
            
            var emissionModule = _idlePt.emission;
            emissionModule.rateOverTime = 160;

            ParticleSystem.CollisionModule collision = _idlePt.collision;
            collision.type = ParticleSystemCollisionType.World;
            collision.sendCollisionMessages = true;
            collision.mode = ParticleSystemCollisionMode.Collision2D;
            collision.enabled = true;
            collision.quality = ParticleSystemCollisionQuality.High;
            collision.maxCollisionShapes = 256;
            collision.dampenMultiplier = 0;
            collision.radiusScale = 1;
            collision.collidesWith = 1 << (int)PhysLayers.PLAYER;

            _idlePt.gameObject.AddComponent<DamageOnParticleCollision>().Damage = int.MaxValue;
        }

        public void StartFollow()
        {
            if (_logic != null) StopCoroutine(_logic);
            _logic = StartCoroutine(RotateToHero());
        }

        private IEnumerator RotateToHero()
        {
            _rigidbody.velocity = Vector2.zero;
            _idlePt.Stop();
            var heroPos = HeroController.instance.transform.position;
            var selfPos = transform.position;
            var angle = Mathf.Atan2(heroPos.y - selfPos.y, heroPos.x - selfPos.x) * Mathf.Rad2Deg;
            iTween.RotateTo(gameObject, iTween.Hash("z", angle - 90, "time", RotateTime, "easetype", iTween.EaseType.easeOutQuad));
            yield return new WaitForSeconds(RotateTime + PreTravelPause);
            _logic = StartCoroutine(Travel());
        }

        private IEnumerator Travel()
        {
            _idlePt.Play();
            var velX = Mathf.Cos((transform.rotation.eulerAngles.z + 90) * Mathf.Deg2Rad) * TravelSpeed;
            var velY = Mathf.Sin((transform.rotation.eulerAngles.z + 90) * Mathf.Deg2Rad) * TravelSpeed;
            _rigidbody.velocity = new Vector2(velX, velY);
            yield return new WaitForSeconds(TravelTime);
            _logic = StartCoroutine(RotateToHero());
        }
    }
}
