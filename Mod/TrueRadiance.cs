using System.Linq;
using System.Reflection;
using Modding;
using UnityEngine;

namespace AnyRadiance
{
    internal class TrueRadiance : MonoBehaviour
    {
        #region Components

        private tk2dSpriteAnimator _animator;
        private BoxCollider2D _collider;
        private EnemyDeathEffectsUninfected _deathEffects;
        private EnemyDreamnailReaction _dreamReaction;
        private HealthManager _healthManager;
        private EnemyHitEffectsGhost _hitEffects;
        private MeshRenderer _renderer;
        #endregion

        private void Awake()
        {
            _animator = GetComponent<tk2dSpriteAnimator>();
            _collider = GetComponent<BoxCollider2D>();
            _deathEffects = GetComponent<EnemyDeathEffectsUninfected>();
            _dreamReaction = GetComponent<EnemyDreamnailReaction>();
            _healthManager = GetComponent<HealthManager>();
            _hitEffects = GetComponent<EnemyHitEffectsGhost>();
            _renderer = GetComponent<MeshRenderer>();

            AssignFields();
        }

        private void AssignFields()
        {
            GameObject @ref = AnyRadiance.Instance.GameObjects["Ref"];

            var deathEffects = @ref.GetComponent<EnemyDeathEffectsUninfected>();
            deathEffects.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public).ToList()
                .ForEach(fi => fi.SetValue(_deathEffects, fi.GetValue(deathEffects)));

            var dreamReaction = @ref.GetComponent<EnemyDreamnailReaction>();
            ReflectionHelper.SetField(_dreamReaction, "dreamImpactPrefab",
                ReflectionHelper.GetField<EnemyDreamnailReaction, GameObject>(dreamReaction, "dreamImpactPrefab"));

            var hitEffects = @ref.GetComponent<EnemyHitEffectsGhost>();
            hitEffects.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public).ToList()
                .ForEach(fi => fi.SetValue(_hitEffects, fi.GetValue(hitEffects)));

            var healthManager = @ref.GetComponent<HealthManager>();
            healthManager.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public)
                .Where(fi => fi.Name.Contains("Prefab") || fi.Name.Contains("Sound")).ToList()
                .ForEach(fi => fi.SetValue(_healthManager, fi.GetValue(healthManager)));
        }
    }
}
