using System;
using System.Collections;
using System.Collections.Generic;
using Game.Tween;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyProfile _profile;

        [Header("Dependencies")]
        [SerializeField] private EnemyAttack _attack;
        [SerializeField] private EnemyHealth _health;

        [SerializeField] private DoScale _scale;

        public Animator Animator;
        
        [FoldoutGroup("Art")] [SerializeField] private SpriteRenderer _renderer;

        private void Awake()
        {
            Animator = GetComponent<Animator>();
        }

        private bool InitializeDependenciesIsSucess()
        {
            if (_attack == null)
                _attack = GetComponent<EnemyAttack>();

            if (_health == null)
                _health = GetComponent<EnemyHealth>();

            return _health != null && _attack != null;
        }

        public void SetupEnemyAttributes()
        {
            _attack.SetDamage(_profile.Damage);
            _attack.SetCooldown(_profile.Cooldown);
            _attack.SetMinimunAndMaximumCooldown(_profile.MinCooldown, _profile.MaxCooldown);
            _attack.SetPossibleElements(_profile.PossibleElements);
            _attack.SetSecondsInChargeAttackState(_profile.SecondsInChargeAttackState);
            
            _health.SetHealthBars(_profile.HealthBars);
            _health.InitializeBars();
            _health.SelectBar(0);

            Animator.runtimeAnimatorController = _profile.RuntimeAnimator;
            
            _scale.SetScaleProperties(_profile.TargetScaleSize, _profile.Duration, _profile.Vibration, _profile.Elasticity);
            _renderer.sprite = _profile.EnemySprite;
            _renderer.transform.localPosition = _profile.BodyOffset;
        }

        public void SetProfile(EnemyProfile newProfile) => _profile = newProfile;
    }
}