using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;
using Game.AbilitySystem;
using Game.DamageSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    public class PlayerShieldUsage : BaseAbility
    {
        [SerializeField] private Damageable _damageable;
        [SerializeField, Range(0, 100)] private float _shieldDamageOnBlock = 20f;

        [SerializeField] private float _shieldDuration = 1f;

        public float ShieldDamageOnBlock => _shieldDamageOnBlock;

        private void Start()
        {
            if (_damageable == null)
                _damageable = GetComponent<Damageable>();
            
            PlayerInput.Instance.Actions.Player.Shield.performed += ShieldOnperformed;
        }

        private void ShieldOnperformed(InputAction.CallbackContext obj)
        {
            this.TriggerAbility();
        }

        public override void TriggerAbility()
        {
            if(!CanUseAbility)
                return;

            EnergySource.UseEnergy(RequiredEnergy);
            StartCoroutine(HandleShieldDuration_CO());
        }

        private IEnumerator HandleShieldDuration_CO()
        {
            _damageable.Shield.IsActive = true;
            yield return new WaitForSeconds(_shieldDuration);
            _damageable.Shield.IsActive = false;
        }
    }
}